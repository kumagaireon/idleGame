using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("�^�b�`�|�W�V�������擾����Ԋu")]
    public float touchInterval = 0.1f; // �^�b�`�|�W�V�������擾����Ԋu

    [Header("�X���C�v�Ƃ��ĔF�����邽�߂̍ŏ�����")]
    public float swipeThreshold = 2f; // �X���C�v�Ƃ��ĔF�����邽�߂̍ŏ�����

    [SerializeField]
    [Header("�^�b�`�ʒu���i�[����z��")]
    private Vector2[] touchPositions = new Vector2[3]; // �^�b�`�ʒu���i�[����z��

    [Header("�X���C�v�����ǂ����������t���O")]
    private bool isSwiping = false; // �X���C�v�����ǂ����������t���O

    public bool isSwip = false;
    [SerializeField]
    [Header("�X���C�v�̕����ύX��")]
    private int swipeCount = 0; // �X���C�v�̕����ύX��

    [Header("�Ō�Ƀ^�b�`�ʒu���擾��������")]
    private float lastTouchTime; // �Ō�Ƀ^�b�`�ʒu���擾��������

    private TapDetector tapDetector; // �P�����ƒ������̌��o��
    private SwipeJudge swipeJudge; // �X���C�v����̃N���X

    // �P�����񐔂��J�E���g����v���p�e�B
    public int TapCount { get; private set; } = 0;
    public int LengthCount { get; private set; } = 0;

    private void Start()
    {
        tapDetector = GetComponent<TapDetector>();
        swipeJudge = GetComponent<SwipeJudge>();
    }

    private void Update()
    {
        tapDetector.CheckTap();
        SwipeInput();
    }

    public void IncrementTapCount()
    {
        if (isSwip)
        {
            TapCount++;
            Debug.Log("�P������: " + TapCount);
        }
    }

    public void IncrementLengthCount()
    {
        if (isSwip)
        {
            LengthCount++;
            Debug.Log("�P������: " + TapCount);
        }
    }

    private void SwipeInput()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            InitializeSwipe(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            UpdateSwipe(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //  EndSwipe();
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                InitializeSwipe(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                UpdateSwipe(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
             //   EndSwipe();
            }
        }
#endif
    }

    private void InitializeSwipe(Vector2 position)
    {
        touchPositions[0] = touchPositions[1] = touchPositions[2] = position;
        isSwiping = true;
        lastTouchTime = Time.time;
    }

    private void UpdateSwipe(Vector2 position)
    {
        if (Time.time - lastTouchTime >= touchInterval)
        {
            touchPositions[2] = touchPositions[1];
            touchPositions[1] = touchPositions[0];
            touchPositions[0] = position;
            lastTouchTime = Time.time;

            if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && swipeJudge.AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
            {
                swipeCount++;
                isSwip = true;
              //  Debug.Log("�����]��");
                swipeJudge.DetectSwipeDirection(touchPositions[0], touchPositions[1]);
            }
            else
            {
                isSwip = false;
            }
        }
    }

    private void EndSwipe()
    {
        if (isSwiping)
        {
            Debug.Log("�X���C�v����: " + swipeCount);
            swipeCount = 0;
            isSwiping = false;
        }
    }
}