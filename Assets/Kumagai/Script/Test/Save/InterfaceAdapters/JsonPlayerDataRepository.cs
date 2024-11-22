using System.IO;
using Kumagai.Entities;
using Kumagai.UseCase;
using Unity.VisualScripting;
using UnityEngine;

namespace Kumagai.InterfaceAdapters
{
    // JsonPlayerDataRepositoryクラスは、IPlayerDataRepositoryインターフェースを実装し、プレイヤーデータの保存と読み込みを行う
    public class JsonPlayerDataRepository : IPlayerDataRepository
    {
        // 指定されたプレイヤーIDに基づいてファイルパスを取得するメソッド
        private string GetFilePath(string playerId) => $"{Application.persistentDataPath}/{playerId}_playerData.json";

        /// <summary>
        /// プレイヤーデータをJSON形式で保存する
        /// </summary>
        /// <param name="data">保存するPlayerDataオブジェクト</param>
        public void Save(PlayerData data)
        {
            // プレイヤーIDに基づいてファイルパスを取得
            string filePath = GetFilePath(data.playerId);
            // PlayerDataオブジェクトをJSON形式に変換
            string json = JsonUtility.ToJson(data);
            // 変換したJSONデータをファイルに書き込む
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// プレイヤーデータをJSON形式で読み込む
        /// </summary>
        /// <param name="playrtId">読み込むプレイヤーのID</param>
        /// <returns>読み込まれたPlayerDataオブジェクト</returns>
        public PlayerData Load(string playrtId)
        {
            // プレイヤーIDに基づいてファイルパスを取得
            string filePath = GetFilePath(playrtId);
            // ファイルが存在する場合、内容を読み込みPlayerDataオブジェクトに変換
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<PlayerData>(json);
            }

            // ファイルが存在しない場合、nullを返す
            return null;
        }
    }
}