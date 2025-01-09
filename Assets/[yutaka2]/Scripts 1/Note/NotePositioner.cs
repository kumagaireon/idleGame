using UnityEngine;

public class NotePositioner : MonoBehaviour
{
    // Noteの位置を設定するメソッド
    public Vector2 SetPosition(int posNum)
    {        
        // posNumに基づいてX,Y座標を計算
        float posX = (posNum - 1) % 3 - 1;
        float posY = ((posNum + 2) / 3) * -1 + 2;
        // 計算した座標をVector2として返す
        return new Vector2(posX, posY);
    }
}
