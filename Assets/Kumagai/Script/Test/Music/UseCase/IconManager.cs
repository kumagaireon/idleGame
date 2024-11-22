using Cysharp.Threading.Tasks;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{
    // IconManagerクラスは、アイコンの生成と破棄の処理を行う
    public class IconManager:IIconManager
    {
        // アイコンのプレハブを保持する配列
        private readonly GameObject[] iconPrefabs;

        /// <summary>
        /// コンストラクタはアイコンのプレハブ配列を引数に取り、それをフィールドに設定する
        /// </summary>
        /// <param name="iconPrefabs"></param>
        public IconManager(GameObject[] iconPrefabs)
        {
            this.iconPrefabs = iconPrefabs;
        }

        /// <summary>
        /// 指定されたMusicDataKariオブジェクトに基づいてアイコンを生成する
        /// </summary>
        /// <param name="musicDataKari"></param>
        public void CreateIcon(MusicDataKari musicDataKari)
        {
            // アイコンのプレハブをインスタンス化
            GameObject iconObject = UnityEngine.Object.Instantiate(iconPrefabs[musicDataKari.direction - 1]);
            // ここでiconObjectを使用して表示などの処理を行う
            HandleIconDestruction(iconObject, musicDataKari.keepTime).Forget();
        }

        /// <summary>
        /// 指定された時間が経過した後にアイコンを破棄する非同期処理を行う
        /// </summary>
        /// <param name="icon">破棄する対象のGameObjectアイコン</param>
        /// <param name="keepTime">アイコンを保持する時間（秒単位）</param>
        public async UniTask HandleIconDestruction(GameObject icon, float keepTime)
        {
            // 指定された時間（ミリ秒単位）待機
            await UniTask.Delay((int)(keepTime * 1000));
            // 待機後にアイコンを破棄
            UnityEngine.Object.Destroy(icon);
        }
    }
}