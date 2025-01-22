using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IdolGame.InGame.Interfaces;

public interface IAlphaService
{
    // SpriteRendererをフェードインさせるメソッド
    UniTask FadeIn(SpriteRenderer targetSr);
}