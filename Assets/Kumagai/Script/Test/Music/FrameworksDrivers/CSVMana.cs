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
        private CSVLoaderAdapter csvLoaderAdapter;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadCSVAndSwitchScene().Forget();
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                LoadCSVAndSwitchScene().Forget();
            }
        }

        private async UniTaskVoid LoadCSVAndSwitchScene()
        {
            if (csvLoaderAdapter != null)
            {
                await csvLoaderAdapter.LoadMusicData();
                  SceneManager.LoadScene("TESTSCENE 1");
            }
            else
            {
                Debug.LogError("CSVLoaderAdapter が初期化されていません。");
            }
        }
    }
}