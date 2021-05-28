using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

//if can't find file replace the text in combine with Application.persistentDataPath

public static class FileManager
{
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = a_FileName;//Path.Combine("\\aiden\\Unity\\2D-platformer\\2D-platformer\\Assets\\Scripts\\Global", a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = a_FileName;//Path.Combine("\\aiden\\Unity\\2D-platformer\\2D-platformer", a_FileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}