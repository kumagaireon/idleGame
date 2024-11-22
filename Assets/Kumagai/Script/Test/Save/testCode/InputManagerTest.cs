using Kumagai.Entities;
using Kumagai.InterfaceAdapters;
using Kumagai.UseCase;
using UnityEngine;

namespace Kumagai.testCode
{
    // InputManagerTestクラスは、ゲームのテスト用にプレイヤーデータとセーブデータの管理を行う
    public class InputManagerTest : MonoBehaviour
    {
        // セーブ・ロード機能を提供するインターフェース
        private ISaveLoadUseCase saveLoadUseCase;
// プレイヤーデータ管理を提供するユースケース
        [SerializeField] private PlayerDataUseCase playerDataUseCase; 
        // 現在のプレイヤーID
       [SerializeField] private string currentPlayerId = "player123";
        
        private void Awake()
        {
            // 具体的なリポジトリのインスタンスを作成し、依存性注入を行う
            var repository = new JsonSaveLoadRepository();
            saveLoadUseCase = new SaveloadUseCase(repository);
            
            var playrtrRepository = new JsonPlayerDataRepository();
            playerDataUseCase = new PlayerDataUseCase(playrtrRepository);
          
            // プレイヤーデータをロード
            PlayerData playerData = playerDataUseCase.Load(currentPlayerId);
            if (playerData != null)
            {
                Debug.Log($"Player ID: {playerData.playerId}, Player Name: {playerData.playerName}");
            }
            else
            {
                Debug.Log("プレイヤーデータが見つかりませんでした。新しいデータを作成します。");
                playerData = new PlayerData { playerId = currentPlayerId, playerName = "Player One" };
                playerDataUseCase.Save(playerData);
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // 左クリック
            {
                PlayerLoadGame();
              //  LoadGame();
            }

            if (Input.GetMouseButtonDown(1)) // 右クリック
            {
                PlayerSave();
                //  SaveGame();
            }
        }

        /// <summary>
        /// プレイヤーデータをセーブするメソッド
        /// </summary>
        private void PlayerSave()
        {
            PlayerData data = new PlayerData
            {
                playerId = currentPlayerId, playerName = "Player One", // 実際のデータをここに設定
             
            };
            playerDataUseCase.Save(data);
            Debug.Log("ゲームデータをセーブしました。");
        }

        /// <summary>
        /// プレイヤーデータをロードするメソッド
        /// </summary>
        private void PlayerLoadGame()
        {
            PlayerData data = playerDataUseCase.Load(currentPlayerId);
            if (data != null)
            {
                Debug.Log($"ゲームデータをロードしました。Player ID: {data.playerId}, Player Name: {data.playerName}"); // ロードしたデータをゲーム内のプレイヤーやその他のオブジェクトに反映する処理を追加
            }
            else
            {
                Debug.Log("プレイヤーデータが見つかりませんでした。");
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        
        /// <summary>
        /// ゲームデータをセーブするメソッド
        /// </summary>
        private void SaveGame()
        {
            SaveData data = new SaveData
            {
                playerLevel = 1, // 実際のデータをここに設定
                playerHealth = 100f, playerPosition = Vector3.zero
            };
            saveLoadUseCase.Save(data);
            Debug.Log("ゲームデータをセーブしました。");
        }

        

        // ReSharper disable Unity.PerformanceAnalysis
        
        /// <summary>
        /// ゲームデータをロードするメソッド
        /// </summary>
        private void LoadGame()
        {
            SaveData data = saveLoadUseCase.Load();
            Debug.Log("ゲームデータをロードしました。"); 
            // ロードしたデータをゲーム内のプレイヤーやその他のオブジェクトに反映する処理を追加
        }
    }
}