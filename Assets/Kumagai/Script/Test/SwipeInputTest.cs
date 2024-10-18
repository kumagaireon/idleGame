using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputTest : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private bool isSwiping = false;

    void Update()
    {
#if UNITY_EDITOR
        // PC �̏ꍇ�̓��͏���
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            endTouchPosition = Input.mousePosition;
            isSwiping = false;

            Vector2 swipeDelta = endTouchPosition - startTouchPosition;
            DetectSwipeDirection(swipeDelta);
        }
#else
        // ���o�C���̓��͏���
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                endTouchPosition = touch.position;
                isSwiping = false;

                Vector2 swipeDelta = endTouchPosition - startTouchPosition;
                DetectSwipeDirection(swipeDelta);
            }
        }
#endif
    }

    void DetectSwipeDirection(Vector2 swipeDelta)
    {
        if (swipeDelta.magnitude > 50)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                Debug.Log(swipeDelta.x > 0 ? "�E�X���C�v" : "���X���C�v");
            }
            else
            {
                Debug.Log(swipeDelta.y > 0 ? "��X���C�v" : "���X���C�v");
            }
        }
    }
}
