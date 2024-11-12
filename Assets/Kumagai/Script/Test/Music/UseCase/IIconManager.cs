using Cysharp.Threading.Tasks;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{
    public interface IIconManager
    {
        void CreateIcon(MusicData musicData);
        UniTask HandleIconDestruction(GameObject icon, float keepTime);
    }
}