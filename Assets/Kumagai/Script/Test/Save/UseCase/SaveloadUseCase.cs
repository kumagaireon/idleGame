using Kumagai.Entities;

namespace Kumagai.UseCase
{
    public interface ISaveLoadUseCase
    {
        void Save(SaveData data);
        SaveData Load();
    }

    public class SaveloadUseCase : ISaveLoadUseCase
    {
        private readonly ISaveLoadRepository repository;

        public SaveloadUseCase(ISaveLoadRepository repository)
        {
            this.repository = repository;
        }

        public void Save(SaveData data)
        {
            repository.Save(data);
        }

        public SaveData Load()
        {
            return repository.Load();
        }
    }
}
