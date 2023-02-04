using UnityEngine;
using System.IO;

namespace Codes.Scripts.SaveSystem
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string SaveDirectory = "/SaveData/";
        public const string FileName = "SaveGame.sav";
        
        /// <summary>
        /// Save data in json format
        /// <param name="fileName">File to save data</param>
        /// </summary>
        public static bool SaveData()
        {
            var dir = Application.persistentDataPath + SaveDirectory;
            
            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string json = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(dir + FileName, json);

            GUIUtility.systemCopyBuffer = dir;
            return true;
        }
        
        /// <summary>
        /// Save data in json format
        /// <param name="fileName">File to load data</param>
        /// </summary>
        public static void LoadData(string fileName)
        {
            string fullPath = Application.persistentDataPath + SaveDirectory + fileName;
            SaveData tempData = new SaveData();
            
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                Debug.LogError("Save file does not exist");
            }

            CurrentSaveData = tempData;
        }
    }
}