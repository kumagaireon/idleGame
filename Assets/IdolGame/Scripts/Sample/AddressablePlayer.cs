using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;

public class AddressablePlayer : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject cubePrefab;
    async UniTask Start()
    {
        // var handle = Addressables.LoadAssetAsync<GameObject>("Cube_Prefab");
        // var handle = Addressables.LoadAssetAsync<GameObject>(cubePrefab);
        var handle = cubePrefab.LoadAssetAsync<GameObject>();
        await handle;

        var instance = Object.Instantiate(handle.Result);

        instance.name = "Addressable Prefab ";

        await UniTask.WaitForSeconds(2.0f);

        Object.Destroy(instance);
        Addressables.Release((handle));
    }
}