using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeWithMetrics : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private float startTime, endTime;
    private bool isSwiping = false;
    private int swipeCount = 0;

    void Update()
    {
#if UNITY_EDITOR
        // PC �̏ꍇ�̓��͏���
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            startTime = Time.time;
            isSwiping = true;
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            Vector2 currentSwipePosition = Input.mousePosition;
            if (Vector2.Distance(currentSwipePosition, startTouchPosition) > 50)
            {
                swipeCount++;
                startTouchPosition = currentSwipePosition;
                Debug.Log("Swipe Count: " + swipeCount);
            }
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            endTouchPosition = Input.mousePosition;
            endTime = Time.time;
            isSwiping = false;

            Vector2 swipeDelta = endTouchPosition - startTouchPosition;
            float swipeTime = endTime - startTime;

            Debug.Log("�X���C�v�����o����܂���: ");
            Debug.Log("����: " + swipeDelta.magnitude);
            Debug.Log("����: " + swipeTime + " �b");
            Debug.Log("�X���C�v�񐔁i�ŏI�j: " + swipeCount);

            DetectSwipeDirection(swipeDelta);
            swipeCount = 0;
        }
#else
        // ���o�C���̓��͏���
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                startTime = Time.time;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                Vector2 currentSwipePosition = touch.position;
                if (Vector2.Distance(currentSwipePosition, startTouchPosition) > 50)
                {
                    swipeCount++;
                    startTouchPosition = currentSwipePosition;
                    Debug.Log("Swipe Count: " + swipeCount);
                }
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                endTouchPosition = touch.position;
                endTime = Time.time;
                isSwiping = false;

                Vector2 swipeDelta = endTouchPosition - startTouchPosition;
                float  swipeTime = endTime - startTime;
                Debug.Log("�X���C�v�����o����܂���: ");
                Debug.Log("����: " + swipeDelta.magnitude);
                Debug.Log("����: " + swipeTime + " �b");
                Debug.Log("�X���C�v�񐔁i�ŏI�j: " + swipeCount); swipeTime = endTime - startTime;


                DetectSwipeDirection(swipeDelta);
                swipeCount = 0;
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
