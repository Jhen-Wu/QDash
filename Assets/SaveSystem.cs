using UnityEngine; 
using System.IO; 

public static class SaveSystem 
{
    // 存檔資料夾路徑
    public static readonly string SAVE_FOLDER =
        Application.persistentDataPath + "/saves/";

    // 檔案副檔名
    public static readonly string FILE_EXT = ".json";

    public static void Save(string fileName, string dataToSave) 
    {
        // 若資料夾不存在則建立
        if (!Directory.Exists(SAVE_FOLDER)) 
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        // 寫入檔案
        File.WriteAllText(
            SAVE_FOLDER + fileName + FILE_EXT,
            dataToSave
        );
    }

    public static string Load(string fileName) 
    {
        string fileloc = SAVE_FOLDER + fileName + FILE_EXT;

        // 若檔案存在則讀取
        if (File.Exists(fileloc)) 
        {
            return File.ReadAllText(fileloc);
        }
        else 
        {
            return null;
        }
    }
}

