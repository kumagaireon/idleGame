using Kumagai.Entities;

namespace Kumagai.UseCase
{
    public interface IPlayerDataRepository
    {
        void Save(PlayerData data);
        PlayerData Load(string id);
    }

    public class PlayerDataUseCase
    {
        private readonly IPlayerDataRepository repository;

        public PlayerDataUseCase(IPlayerDataRepository repository)
        {
            this.repository = repository;
        }

        public void Save(PlayerData data)
        {
            this.repository.Save(data);
        }

        public PlayerData Load(string playrtId)
        {
            return repository.Load(playrtId);
        }
    }
}