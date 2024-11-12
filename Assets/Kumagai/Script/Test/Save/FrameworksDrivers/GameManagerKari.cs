using System;
using Kumagai.Entities;
using Kumagai.InterfaceAdapters;
using Kumagai.UseCase;
using UnityEngine;

namespace Kumagai.FrameworksDrivers
{
    public class GameManagerKari : MonoBehaviour
    {
        private ISaveLoadUseCase saveLoadUseCase;
        private PlayerDataUseCase playerDataUseCase;
        private void Awake()
        {
            // 具体的なリポジトリのインスタンスを作成
            var repository = new JsonSaveLoadRepository();
            saveLoadUseCase = new SaveloadUseCase(repository);
            
            //
            var playerRepository = new JsonPlayerDataRepository();
            playerDataUseCase = new PlayerDataUseCase(playerRepository);
        }

        private void Start()
        {
            // ゲーム開始時にセーブデータをロード
            SaveData data = saveLoadUseCase.Load();
            // ロードしたデータをゲーム内のプレイヤーやその他のオブジェクトに反映

            // 初回プレイヤー作成処理（実際にはUIでプレイヤーIDと名前を入力させる）
            string playerId = "player123";
            string playerName = "Player One";

            PlayerData playerData = playerDataUseCase.Load(playerId);
            if (playerData == null)
            {
                playerData = new PlayerData
                {
                    playerId = playerId,
                    playerName = playerName
                };
                playerDataUseCase.Save(playerData);
                Debug.Log($"新しいプレイヤーを作成しました: {playerData.playerName}");
            }
            else
            {
                Debug.Log($"既存のプレイヤーデータをロードしました: {playerData.playerName}");
            }
        }

        private void PlayerOnApplicationQuit()
        {
            // ゲーム終了時にプレイヤーデータを保存
            PlayerData playerData = new PlayerData
            {
                playerId = "player123", // 実際のデータをここに設定
                playerName = "Player One"
            };
            playerDataUseCase.Save(playerData);
        }

        private void OnApplicationQuit()
        {
            SaveData data = new SaveData
            {
                playerLevel = 1,
                playerHealth = 100f,
                playerPosition = Vector3.zero
            };
            saveLoadUseCase.Save(data);
        }
    }
}