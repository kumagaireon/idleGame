using System.IO;
using Kumagai.Entities;
using Kumagai.UseCase;
using Unity.VisualScripting;
using UnityEngine;

namespace Kumagai.InterfaceAdapters
{
    public class JsonPlayerDataRepository : IPlayerDataRepository
    {
        private string GetFilePath(string playerId) => $"{Application.persistentDataPath}/{playerId}_playerData.json";

        public void Save(PlayerData data)
        {
            string filePath = GetFilePath(data.playerId);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
        }

        public PlayerData Load(string playrtId)
        {
            string filePath = GetFilePath(playrtId);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<PlayerData>(json);
            }

            return null;
        }
    }
}