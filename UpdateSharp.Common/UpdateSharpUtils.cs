using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateSharp.Common
{

    public static class UpdateSharpUtils
    {

        public static UpdatePatchChanges ParseChanges(UpdatePatch current, UpdatePatch latest)
        {
            var result = new UpdatePatchChanges();

            // Look for deletion first
            Action<UpdatePatchFile, UpdatePatchFile> scanFolder = null;
            scanFolder = (currentFile, latestFile) =>
            {
                if (currentFile == null && latestFile == null)
                {
                    // This should not be happening
                    return;
                }
                if (currentFile != null && latestFile == null)
                {
                    // Current file no longer exist
                    result.DeletedItems.Add(currentFile);
                }
                else if (currentFile == null && latestFile != null)
                {
                    // New file available
                    result.Modifications.Add(latestFile);
                }
                else
                {
                    // When both file are available
                    if (currentFile.IsFolder != latestFile.IsFolder)
                    {
                        // The folder/file is deleted and switched to a folder/file with same name
                        result.DeletedItems.Add(currentFile);
                        result.Modifications.Add(latestFile);
                    }
                    else if (currentFile.IsFolder)
                    {
                        // If both are folder, scan the subitems
                        var latestSubItems = new HashSet<UpdatePatchFile>(latestFile.SubItems.Values);

                        foreach (var currentSubItem in currentFile.SubItems)
                        {
                            if (latestFile.SubItems.TryGetValue(currentSubItem.Key, out var latestSubItem))
                            {
                                latestSubItems.Remove(latestSubItem);
                            }

                            scanFolder(currentSubItem.Value, latestSubItem);
                        }

                        // All the remaining items in the HashSet are files that are not scanned
                        // (did not appear in the current version folder)
                        foreach (var item in latestSubItems)
                        {
                            scanFolder(null, item);
                        }
                    }
                    else
                    {
                        // If both are files, scan if the hash code has changed
                        if (currentFile.HashCode != latestFile.HashCode)
                        {
                            result.Modifications.Add(latestFile);
                        }
                    }
                }

                scanFolder(current.Files, latest.Files);
            };

            return result;
        }

    }

}
