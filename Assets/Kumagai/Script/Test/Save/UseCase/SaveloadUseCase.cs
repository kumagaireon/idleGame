using Kumagai.Entities;

namespace Kumagai.UseCase
{
    /// <summary>
    /// セーブデータの保存と読み込みを管理するためのユースケースインターフェース
    /// </summary>
    public interface ISaveLoadUseCase
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

    /// <summary>
    /// セーブデータの保存と読み込みを管理するユースケースクラス
    /// </summary>
    public class SaveloadUseCase : ISaveLoadUseCase
    {
        // セーブデータのリポジトリを保持するフィールド
        private readonly ISaveLoadRepository repository;

        /// <summary>
        /// SaveloadUseCaseクラスのコンストラクタ
        /// </summary>
        /// <param name="repository">ISaveLoadRepositoryのインスタンス</param>
        public SaveloadUseCase(ISaveLoadRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// セーブデータを保存するメソッド
        /// </summary>
        /// <param name="data">保存するSaveDataオブジェクト</param>
        public void Save(SaveData data)
        {
            repository.Save(data);
        }

        /// <summary>
        /// セーブデータを読み込むメソッド
        /// </summary>
        /// <returns>読み込まれたSaveDataオブジェクト</returns>
        public SaveData Load()
        {
            return repository.Load();
        }
    }
}
