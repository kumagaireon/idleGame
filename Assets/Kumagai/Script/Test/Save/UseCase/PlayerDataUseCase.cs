using Kumagai.Entities;

namespace Kumagai.UseCase
{
    /// <summary>
    /// プレイヤーデータの保存と読み込みを管理するためのインターフェース
    /// </summary>
    public interface IPlayerDataRepository
    {
        /// <summary>
        /// プレイヤーデータを保存するメソッド
        /// </summary>
        /// <param name="data">保存するPlayerDataオブジェクト</param>
        void Save(PlayerData data);
        /// <summary>
        /// プレイヤーデータを読み込むメソッド
        /// </summary>
        /// <param name="id">読み込むプレイヤーのID</param>
        /// <returns>読み込まれたPlayerDataオブジェクト</returns>
        PlayerData Load(string id);
    }

    /// <summary>
    /// プレイヤーデータの保存と読み込みを管理するユースケースクラス
    /// </summary>
    public class PlayerDataUseCase
    {
        // プレイヤーデータのリポジトリを保持するフィールド
        private readonly IPlayerDataRepository repository;

        /// <summary>
        /// PlayerDataUseCaseクラスのコンストラクタ
        /// </summary>
        /// <param name="repository">IPlayerDataRepositoryのインスタンス</param>
        public PlayerDataUseCase(IPlayerDataRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// プレイヤーデータを保存するメソッド
        /// </summary>
        /// <param name="data">保存するPlayerDataオブジェクト</param>
        public void Save(PlayerData data)
        {
            this.repository.Save(data);
        }

        /// <summary>
        /// プレイヤーデータを読み込むメソッド
        /// </summary>
        /// <param name="playrtId">読み込むプレイヤーのID</param>
        /// <returns>読み込まれたPlayerDataオブジェクト</returns>
        public PlayerData Load(string playrtId)
        {
            return repository.Load(playrtId);
        }
    }
}