using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�g���Ȃ��؂�Ԃ��̔��肪�ł��ĂȂ�

public class SwipeInputWithCount : MonoBehaviour
{
    public float followSpeed = 10f; // Cyalume�I�u�W�F�N�g�̒Ǐ]���x
    public float swipeThreshold = 0.2f; // �X���C�v�Ƃ��ĔF�����邽�߂̍ŏ�����
    public float directionChangeThreshold = 0.5f; // �X���C�v�����̕ύX�����o���邽�߂�臒l
    [SerializeField] private Vector2[] touchPositions = new Vector2[3]; // �^�b�`�ʒu���i�[����z��
    private bool isSwiping = false; // �X���C�v�����ǂ����������t���O
    [SerializeField] private int swipeCount = 0; // �X���C�v�̕����ύX��
    [SerializeField] GameObject Cyalume; // �^�b�v�������ɏo��I�u�W�F�N�g
    private GameObject currentCyalume; // ���ݐ�������Ă���Cyalume�I�u�W�F�N�g

    void Update()
    {
#if UNITY_EDITOR
        // PC �̏ꍇ�̓��͏���
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartSwipe(Input.mousePosition));
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            StartCoroutine(UpdateSwipe(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            StartCoroutine(EndSwipe());
        }
#else
        // ���o�C���̓��͏���
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartCoroutine(StartSwipe(touch.position));
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                StartCoroutine(UpdateSwipe(touch.position));
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                StartCoroutine(EndSwipe());
            }
        }
#endif

        // Cyalume�����݂̃^�b�`�ʒu�ɃX���[�Y�Ɉړ�
        if (currentCyalume != null && isSwiping)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPositions[0].x, touchPositions[0].y, 10));
            currentCyalume.transform.position = Vector3.Lerp(currentCyalume.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private IEnumerator StartSwipe(Vector2 position)
    {
        touchPositions[0] = touchPositions[1] = touchPositions[2] = position;
        isSwiping = true;

        // Cyalume�𐶐����Č��݂̃^�b�`�ʒu�ɔz�u
        currentCyalume = Instantiate(Cyalume, Camera.main.ScreenToWorldPoint(new Vector3(touchPositions[0].x, touchPositions[0].y, 10)), Quaternion.identity);

        yield return null;
    }

    private IEnumerator UpdateSwipe(Vector2 position)
    {
        touchPositions[2] = touchPositions[1];
        touchPositions[1] = touchPositions[0];
        touchPositions[0] = position;

        // �X���C�v�̕������ς�������ǂ������`�F�b�N
        if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
        {
            swipeCount++;
            Debug.Log("�����]��");
            DetectSwipeDirection(touchPositions[0], touchPositions[1]);
        }

        yield return null;
    }

    private IEnumerator EndSwipe()
    {
        isSwiping = false;
        Debug.Log("�X���C�v����: " + swipeCount);
        swipeCount = 0;

        // Cyalume���폜
        if (currentCyalume != null)
        {
            Destroy(currentCyalume);
        }

        yield return null;
    }

    // �X���C�v�̕������t���ǂ����𔻒肷��֐�
    private bool AreVectorsReversed(Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 vectorBA = a - b;
        Vector2 vectorBC = b - c;

        // �X���C�v�����̕ύX�����o���邽�߂�臒l���g�p���Ĕ���
        return Vector2.Dot(vectorBA, vectorBC) < -directionChangeThreshold;
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