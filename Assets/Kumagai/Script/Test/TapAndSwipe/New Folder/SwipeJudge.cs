using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SwipeJudge : MonoBehaviour
{  
    // �X���C�v�̕������t���ǂ����𔻒肷��֐�
    public bool AreVectorsReversed(Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 vectorBA = a - b;
        Vector2 vectorBC = b - c;
        return Vector2.Dot(vectorBA, vectorBC) < 0;
    }

    // �X���C�v�̕��������o���ă��O�ɏo�͂���֐�
    public void DetectSwipeDirection(Vector2 currentPosition, Vector2 previousPosition)
    {
        Vector2 direction = currentPosition - previousPosition;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            Debug.Log(direction.x > 0 ? "�E�X���C�v" : "���X���C�v");
            RhythmTest.Instance.AddInputTime(Time.time);
        }
        else
        {
            Debug.Log(direction.y > 0 ? "��X���C�v" : "���X���C�v");
            RhythmTest.Instance.AddInputTime(Time.time);
        }
    }

    public void LeftCheck(Vector3 l)
    {

    }
}
