using IdolGame.InGame.Interfaces;
using UnityEngine;

namespace IdolGame.InGame.Services;

public class PositionService : IPositionService
{
    public Vector2 GetPosition(Vector2 posNum)
    {
        // posNumに基づいてX,Y座標を計算
        float posX = (posNum.x - 1) % 3 - 1;
        float posY = ((posNum.y + 2) / 3) * -1 + 2;

        // 計算した座標をVector2として返す
        return new Vector2(posX, posY);
    }
}