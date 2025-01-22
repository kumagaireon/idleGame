using UnityEngine;

namespace IdolGame.InGame.Interfaces;

public interface IPositionService
{
    // 指定された位置にオブジェクトの位置を設定するメソッド
    Vector2 GetPosition(Vector2 posNum);
}