using System;
using Kumagai.Entities;
using Kumagai.InterfaceAdapters;
using Kumagai.UseCase;
using UnityEngine;

namespace Kumagai.FrameworksDrivers
{
    // GameManagerKariクラスは、ゲームの状態管理とプレイヤーデータの保存・読み込みを担当する
    public class GameManagerKari : MonoBehaviour
    {
        private ISaveLoadUseCase saveLoadUseCase;// セーブ・ロード機能を提供するインターフェース
        private PlayerDataUseCase playerDataUseCase;// プレイヤーデータ管理を提供するユースケース
        private void Awake()
        {
            // 具体的なリポジトリのインスタンスを作成し、依存性注入を行う
            var repository = new JsonSaveLoadRepository();
            saveLoadUseCase = new SaveloadUseCase(repository);
            
            // プレイヤーデータ用のリポジトリを作成し、依存性注入を行う
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
            
// プレイヤーデータをロード
            PlayerData playerData = playerDataUseCase.Load(playerId);
            if (playerData == null)
            {
                // プレイヤーデータが存在しない場合、新しいプレイヤーデータを作成
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
                // 既存のプレイヤーデータをロードした場合
                Debug.Log($"既存のプレイヤーデータをロードしました: {playerData.playerName}");
            }
        }

        /// <summary>
        /// ゲーム終了時にプレイヤーデータを保存するメソッド
        /// </summary>
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

        /// <summary>
        /// アプリケーション終了時に呼び出されるメソッド
        /// </summary>
        private void OnApplicationQuit()
        {
            // セーブデータを作成し保存
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