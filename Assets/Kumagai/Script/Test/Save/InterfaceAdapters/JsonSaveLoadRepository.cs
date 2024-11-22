using System.IO;
using Kumagai.Entities;
using Kumagai.UseCase;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Kumagai.InterfaceAdapters
{
    // JsonSaveLoadRepositoryクラスは、ISaveLoadRepositoryインターフェースを実装し、セーブデータの保存と読み込みを行う
    public class JsonSaveLoadRepository:ISaveLoadRepository
    {
        // セーブデータのファイルパスを保持するフィールド
        private string filePath=Application.persistentDataPath+"/saveData.json";

        /// <summary>
        /// セーブデータをJSON形式で保存する
        /// </summary>
        /// <param name="data">保存するSaveDataオブジェクト</param>
        public void Save(SaveData data)
        {
            // SaveDataオブジェクトをJSON形式に変換
            string json = JsonUtility.ToJson(data);
            // 変換したJSONデータをファイルに書き込む
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// セーブデータをJSON形式で読み込む
        /// </summary>
        /// <returns>読み込まれたSaveDataオブジェクト</returns>
        public SaveData Load()
        {
            // ファイルが存在する場合、内容を読み込みSaveDataオブジェクトに変換
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<SaveData>(json);
            }
            // ファイルが存在しない場合、新しいSaveDataオブジェクトを返す
            return new SaveData();
        }
    }
}