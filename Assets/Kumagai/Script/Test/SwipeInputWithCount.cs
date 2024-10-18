using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputWithCount : MonoBehaviour
{
    public float touchInterval = 0.1f; // �^�b�`�|�W�V�������擾����Ԋu
    public float swipeThreshold = 2f; // �X���C�v�Ƃ��ĔF�����邽�߂̍ŏ�����
    [SerializeField] private Vector2[] touchPositions = new Vector2[3]; // �^�b�`�ʒu���i�[����z��
    private bool isSwiping = false; // �X���C�v�����ǂ����������t���O
    [SerializeField] private int swipeCount = 0; // �X���C�v�̕����ύX��
    private float lastTouchTime; // �Ō�Ƀ^�b�`�ʒu���擾��������

    void Update()
    {
#if UNITY_EDITOR
        // PC �̏ꍇ�̓��͏���
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�{�^���������ꂽ�Ƃ��A�^�b�`�ʒu��������
            touchPositions[0] = touchPositions[1] = touchPositions[2] = Input.mousePosition;
            isSwiping = true;
            lastTouchTime = Time.time;
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            // �}�E�X�{�^����������Ă���ԁA����I�Ƀ^�b�`�ʒu���X�V
            if (Time.time - lastTouchTime >= touchInterval)
            {
                touchPositions[2] = touchPositions[1];
                touchPositions[1] = touchPositions[0];
                touchPositions[0] = Input.mousePosition;
                lastTouchTime = Time.time;

                // �X���C�v�̕������ς�������ǂ������`�F�b�N
                if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
                {
                    swipeCount++;
                    Debug.Log("�����]��");
                    DetectSwipeDirection(touchPositions[0], touchPositions[1]);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            // �}�E�X�{�^���������ꂽ�Ƃ��A�X���C�v�̑��������O�ɏo��
            isSwiping = false;
            Debug.Log("�X���C�v����: " + swipeCount);
            swipeCount = 0;
        }
#else
        // ���o�C���̓��͏���
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // �^�b�`���n�܂����Ƃ��A�^�b�`�ʒu��������
                touchPositions[0] = touchPositions[1] = touchPositions[2] = touch.position;
                isSwiping = true;
                lastTouchTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                // �^�b�`���ړ����Ă���ԁA����I�Ƀ^�b�`�ʒu���X�V
                if (Time.time - lastTouchTime >= touchInterval)
                {
                    touchPositions[2] = touchPositions[1];
                    touchPositions[1] = touchPositions[0];
                    touchPositions[0] = touch.position;
                    lastTouchTime = Time.time;

                    // �X���C�v�̕������ς�������ǂ������`�F�b�N
                    if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
                    {
                        swipeCount++;
                        Debug.Log("�����]��");
                        DetectSwipeDirection(touchPositions[0], touchPositions[1]);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                // �^�b�`���I�������Ƃ��A�X���C�v�̑��������O�ɏo��
                isSwiping = false;
                Debug.Log("�X���C�v����: " + swipeCount);
                swipeCount = 0;
            }
        }
#endif
    }

    // �X���C�v�̕������t���ǂ����𔻒肷��֐�
    private bool AreVectorsReversed(Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 vectorBA = a - b;
        Vector2 vectorBC = b - c;
        return Vector2.Dot(vectorBA, vectorBC) < 0;
    }

    // �X���C�v�̕��������o���ă��O�ɏo�͂���֐�
    private void DetectSwipeDirection(Vector2 currentPosition, Vector2 previousPosition)
    {
        Vector2 direction = currentPosition - previousPosition;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            Debug.Log(direction.x > 0 ? "�E�X���C�v" : "���X���C�v");
        }
        else
        {
            Debug.Log(direction.y > 0 ? "��X���C�v" : "���X���C�v");
        }
    }
}
