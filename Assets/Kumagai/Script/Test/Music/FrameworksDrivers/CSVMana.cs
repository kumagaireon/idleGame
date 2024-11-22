using Cysharp.Threading.Tasks;
using Kumagai.InterfaceAdapters;
using Kumagai.UseCase;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Kumagai.FrameworksDrivers
{
    public class CSVMana : MonoBehaviour
    {
        // CSVLoaderAdapterインスタンスを保持するためのフィールド
        private CSVLoaderAdapter csvLoaderAdapter;

        /// <summary>
        /// CSVLoaderAdapter を初期化し、MusicDataLoader を設定する
        /// </summary>
        /// <param name="csvLoaderAdapter"></param>
        public void Initialize(CSVLoaderAdapter csvLoaderAdapter)
        {
            //   Debug.Log("Initialize method called for CSVMana");
            this.csvLoaderAdapter = csvLoaderAdapter;
            if (csvLoaderAdapter != null)
            {
                //Debug.Log("CSVLoaderAdapter is successfully injected in Initialize method");
                IMusicDataLoader musicDataLoader = new MusicDataLoader();
                csvLoaderAdapter.Initialize(musicDataLoader);
            }
            else
            {
                Debug.LogError("CSVLoaderAdapter is null in Initialize method");
            }
        }

        private void Start()
        {
            //  Debug.Log("Start method called for CSVMana");
            // Initialize メソッドで注入済みの csvLoaderAdapter を使用するため、ここでは何もしません
        }

        /// <summary>
        ///  スペースキーまたはタッチ入力があったときに CSV をロードし、シーンを切り替える
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // スペースキーが押されたときに非同期で CSV をロードしてシーンを切り替える
                LoadCSVAndSwitchScene().Forget();
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // タッチ入力があったときに非同期で CSV をロードしてシーンを切り替える
                LoadCSVAndSwitchScene().Forget();
            }
        }

        /// <summary>
        ///  CSV をロードし、指定のシーンに切り替える非同期処理
        /// </summary>
        private async UniTaskVoid LoadCSVAndSwitchScene()
        {
            if (csvLoaderAdapter != null)
            {
                // CSVデータを非同期でロード
                await csvLoaderAdapter.LoadMusicData();
                // ロード完了後にシーンを切り替える
                SceneManager.LoadScene("TESTSCENE 1");
            }
            else
            {
                Debug.LogError("CSVLoaderAdapter が初期化されていません。");
            }
        }
    }
}