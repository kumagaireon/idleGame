using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetector : MonoBehaviour
{
    public float longPressThreshold = 0.5f; // �������Ƃ��ĔF�����邽�߂̎���
    private bool isPressing = false; // �{�^����������Ă��邩�ǂ����̃t���O
    private float pressStartTime; // �{�^����������n�߂�����

    public void CheckTap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;
            pressStartTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0) && isPressing)
        {
            isPressing = false;
            float pressDuration = Time.time - pressStartTime;

            if (pressDuration >= longPressThreshold)
            {
               // Debug.Log("����������");
            }
            else
            {
              //  Debug.Log("�P��������");
            }
        }
    }
}
