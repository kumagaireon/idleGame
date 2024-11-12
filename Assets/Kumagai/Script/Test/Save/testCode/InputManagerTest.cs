using Kumagai.Entities;
using Kumagai.InterfaceAdapters;
using Kumagai.UseCase;
using UnityEngine;

namespace Kumagai.testCode
{
    public class InputManagerTest : MonoBehaviour
    {
        private ISaveLoadUseCase saveLoadUseCase;

        [SerializeField] private PlayerDataUseCase playerDataUseCase; 
       [SerializeField] private string currentPlayerId = "player123";
        
        private void Awake()
        {
            // 具体的なリポジトリのインスタンスを作成
            var repository = new JsonSaveLoadRepository();
            saveLoadUseCase = new SaveloadUseCase(repository);
            
            var playrtrRepository = new JsonPlayerDataRepository();
            playerDataUseCase = new PlayerDataUseCase(playrtrRepository);
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

        private void PlayerSave()
        {
            PlayerData data = new PlayerData
            {
                playerId = currentPlayerId, playerName = "Player One", // 実際のデータをここに設定
             
            };
            playerDataUseCase.Save(data);
            Debug.Log("ゲームデータをセーブしました。");
        }

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
        private void LoadGame()
        {
            SaveData data = saveLoadUseCase.Load();
            Debug.Log("ゲームデータをロードしました。"); 
            // ロードしたデータをゲーム内のプレイヤーやその他のオブジェクトに反映する処理を追加
        }
    }
}