using UnityEngine.Serialization;

namespace Kumagai.Entities
{
    [System.Serializable]
    public class PlayerData
    {
        [FormerlySerializedAs("playerId")] public string playerId;
        public string playerName;
    }
}