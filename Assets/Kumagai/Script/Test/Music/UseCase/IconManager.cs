using Cysharp.Threading.Tasks;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{
    public class IconManager:IIconManager
    {
        private readonly GameObject[] iconPrefabs;

        public IconManager(GameObject[] iconPrefabs)
        {
            this.iconPrefabs = iconPrefabs;
        }

        public void CreateIcon(MusicData musicData)
        {
            GameObject iconObject = UnityEngine.Object.Instantiate(iconPrefabs[musicData.direction - 1]);
            // ここでiconObjectを使用して表示などの処理を行う
            HandleIconDestruction(iconObject, musicData.keepTime).Forget();
        }

        public async UniTask HandleIconDestruction(GameObject icon, float keepTime)
        {
            await UniTask.Delay((int)(keepTime * 1000));
            UnityEngine.Object.Destroy(icon);
        }
    }
}