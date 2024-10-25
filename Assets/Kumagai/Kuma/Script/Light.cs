using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] float Speed = 3;  // �m�[�c�̏����鑬�x
    [SerializeField] int Num = 0;  // ���[���ԍ��i�ǂ̃L�[�������ꂽ���𔻒肷�邽�߂Ɏg�p�j
    private Renderer Renderer;  // �m�[�c�̃����_���[�R���|�[�l���g
    private float Alpha = 0;  // �m�[�c�̓����x

    private void Awake()
    {
        // Renderer�R���|�[�l���g���擾
        Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // �m�[�c�̓����x��0���傫���ꍇ�A�����x���X�V
        if (!(Renderer.material.color.a <= 0))
        {
            Renderer.material.color = new Color(
                Renderer.material.color.r,
                Renderer.material.color.g,
                Renderer.material.color.b,
                Alpha
            );
        }

        // Num�̒l�ɉ����ē���̃L�[�������ꂽ���ǂ������`�F�b�N
        if (Num == 1)
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                ColoerChange();  // �m�[�c�̓����x��ύX
            }
        }
        if (Num == 2)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                ColoerChange();  // �m�[�c�̓����x��ύX
            }
        }
        if (Num == 3)
        {
            if (Input.GetKeyUp(KeyCode.J))
            {
                ColoerChange();  // �m�[�c�̓����x��ύX
            }
        }
        if (Num == 4)
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                ColoerChange();  // �m�[�c�̓����x��ύX
            }
        }

        // ���t���[�����Ƃ�Alpha�l��Speed�Ɋ�Â��Č���
        Alpha -= Speed * Time.deltaTime;
    }

    // �m�[�c�̓����x��ύX���郁�\�b�h
    void ColoerChange()
    {
        Alpha = 0.3f;  // �����x��0.3�ɐݒ�
        Renderer.material.color = new Color(
            Renderer.material.color.r,
            Renderer.material.color.g,
            Renderer.material.color.b,
            Alpha
        );
    }
}