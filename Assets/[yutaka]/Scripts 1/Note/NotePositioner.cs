using UnityEngine;

public class NotePositioner : MonoBehaviour
{
    //Note�̈ʒu���Ǘ�����
    public Vector2 SetPosition(int posNum)
    {        
        float posX = (posNum - 1) % 3 - 1;
        float posY = ((posNum + 2) / 3) * -1 + 2;

        return new Vector2(posX, posY);
    }
}
