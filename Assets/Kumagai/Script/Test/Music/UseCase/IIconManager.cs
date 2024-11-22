using Cysharp.Threading.Tasks;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{
    /// <summary>
    /// アイコン管理のためのインターフェース
    /// </summary>
    public interface IIconManager
    {
        /// <summary>
        /// MusicDataKariオブジェクトに基づいてアイコンを作成するメソッド
        /// </summary>
        /// <param name="musicDataKari">アイコンを作成するためのデータ</param>
        void CreateIcon(MusicDataKari musicDataKari);
        /// <summary>
        /// 指定された時間が経過した後にアイコンを破棄する非同期処理を行うメソッド
        /// </summary>
        /// <param name="icon">破棄する対象のGameObjectアイコン</param>
        /// <param name="keepTime">アイコンを保持する時間（秒単位）</param>
        UniTask HandleIconDestruction(GameObject icon, float keepTime);
    }
}