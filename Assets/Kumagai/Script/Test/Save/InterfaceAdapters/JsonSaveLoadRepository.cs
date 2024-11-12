using System.IO;
using Kumagai.Entities;
using Kumagai.UseCase;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Kumagai.InterfaceAdapters
{
    public class JsonSaveLoadRepository:ISaveLoadRepository
    {
        private string filePath=Application.persistentDataPath+"/saveData.json";

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
        }

        public SaveData Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<SaveData>(json);
            }
            return new SaveData();
        }
    }
}