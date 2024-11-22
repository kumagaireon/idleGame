using Kumagai.Entities;

namespace Kumagai.UseCase
{
    /// <summary>
    /// セーブデータの保存と読み込みを管理するためのインターフェース
    /// </summary>
    public interface ISaveLoadRepository
    {
        /// <summary>
        /// セーブデータを保存するメソッド
        /// </summary>
        /// <param name="data">保存するSaveDataオブジェクト</param>
        void Save(SaveData data);
        /// <summary>
        /// セーブデータを読み込むメソッド
        /// </summary>
        /// <returns>読み込まれたSaveDataオブジェクト</returns>
        SaveData Load();
    }
}