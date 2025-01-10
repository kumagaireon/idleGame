using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IdolGame.InGame.Interfaces;

public interface IAlphaService
{
    UniTask FadeIn(SpriteRenderer targetSr);
}