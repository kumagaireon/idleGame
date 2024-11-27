using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablePlayer : MonoBehaviour
{
    // シリアライズフィールドとしてアドレス可能なゲームオブジェクトの参照
    [SerializeField] private AssetReferenceGameObject _assetReference;

    async UniTask Start()
    {
        // アドレス可能なゲームオブジェクトを非同期にロード
        var handle = Addressables.LoadAssetAsync<GameObject>(_assetReference);
        await handle;
        // ロードしたゲームオブジェクトのインスタンスを作成
        var instance = Object.Instantiate(handle.Result);
        // インスタンスの名前を設定
        instance.name = "Addressable Prefab ";
        // 3秒間待機
        await UniTask.WaitForSeconds(3.0f);
        // インスタンスを破棄
        Object.Destroy(instance.gameObject);
        // アドレス可能なハンドルを解放
        Addressables.Release(handle);
    }
}