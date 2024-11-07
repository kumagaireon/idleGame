using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[DefaultExecutionOrder(1)]
//�^�b�v����A�C�R����\������R���g���[���[
public class DisplayIcon : MonoBehaviour
{
    #region�@�C���X�^���X�ւ�static�ȃA�N�Z�X�|�C���g
    public static DisplayIcon Instance
    {
        get { return instance; }
    }
    private static DisplayIcon instance = null;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("�^�b�v����A�C�R��")]
    [SerializeField] private GameObject[] iconPrefab;

    private float timer = 0;
    private int number = 0;

    private const int ICONNUM = 30;
    [SerializeField] private GameObject[] iconObject;
    private bool[] showFlag = new bool[ICONNUM];    

    private const int FPS = 60;

    // csvAAAA is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = FPS;        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * (CSVReader.Instance.BPM / FPS);
        //Debug.Log(timer);

        //�J�E���g���v�f����菬�����A�\�����ԂɒB�����Ƃ��ɃI�u�W�F�N�g�\��
        if(number < CSVReader.data.Count && timer > CSVReader.data[number].time)
        {            
            ManageCreationAndDestruction(CSVReader.data[number].direction);                
            number++;
        }
    }

    private void ManageCreationAndDestruction(int direction)
    {
        GameObject iconObject = Instantiate(iconPrefab[direction - 1]);
        Debug.Log(direction - 1);
        Destroy(iconObject, CSVReader.data[number].keepTime);

        //�m�[�c�̐������炩���ߑz�肵�ă��������m�ۂ�����@���I�u�W�F�N�g�v�[����
        //for (int i = 0; i < ICONNUM; ++i)
        //{

        //    if (!showFlag[i])
        //    {
        //        iconObject[i] = Instantiate(iconPrefab[direction - 1]);
        //        Destroy(iconObject[i], CSVReader.data[number].keepTime);
        //        break;
        //    }
        //}
    }    
}
