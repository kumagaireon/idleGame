using Kumagai.Entities;

namespace Kumagai.UseCase
{
    public interface ISaveLoadRepository
    {
        void Save(SaveData data);
        SaveData Load();
    }
}