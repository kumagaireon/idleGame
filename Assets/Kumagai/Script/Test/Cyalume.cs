using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�g����

public class Cyalume : MonoBehaviour
{
    [SerializeField] GameObject CyalumeObl; // �^�b�v�������ɏo��I�u�W�F�N�g
    private GameObject currentCyalume; // ���ݐ�������Ă���Cyalume�I�u�W�F�N�g
    void Update()
    {
#if UNITY_EDITOR
        // PC �̏ꍇ�̓��͏���
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartCyalume(Input.mousePosition));
        }
        else if (Input.GetMouseButton(0))
        {
            StartCoroutine(UpdateCyalume(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(EndCyalume());
        }
#else
        // ���o�C���̓��͏���
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartCoroutine(StartCyalume(touch.position));
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                StartCoroutine(UpdateCyalume(touch.position));
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(EndCyalume());
            }
        }
#endif
    }

    private IEnumerator StartCyalume(Vector2 position)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
        currentCyalume = Instantiate(CyalumeObl, worldPosition, Quaternion.identity);
        yield return null;
    }

    private IEnumerator UpdateCyalume(Vector2 position)
    {
        if (currentCyalume != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
            currentCyalume.transform.position = worldPosition;
        }
        yield return null;
    }

    private IEnumerator EndCyalume()
    {
        if (currentCyalume != null)
        {
            Destroy(currentCyalume);
        }
        yield return null;
    }
}