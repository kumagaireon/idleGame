using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
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
    private GameObject[] iconObject = new GameObject[ICONNUM];
    private bool[] showFlag = new bool[ICONNUM];

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(CSVReader.data[0].time);
        //Debug.Log(CSVReader.data[0].keepTime);
        //Debug.Log(CSVReader.data[0].type);

        //for (int i = 0; i < CSVReader.data.Count; ++i)
        //{
        //    switch (CSVReader.data[i].type)
        //    {
        //        case 1:
        //            Instantiate(iconPrefab[0]); break;
        //        case 2:
        //            Instantiate(iconPrefab[1]); break;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log("0");
        if(number < CSVReader.data.Count && timer > CSVReader.data[number].time)
        {
            switch (CSVReader.data[number].type)
            {
                case 1:
                    for(int i = 0; i < ICONNUM; ++i)
                    {
                        if (!showFlag[i])
                        {
                            iconObject[i] = Instantiate(iconPrefab[0]);
                            Destroy(iconObject[i], CSVReader.data[number].keepTime);
                            break;
                        }
                    }                                                                                                            
                    break;
                case 2:
                    for (int i = 0; i < ICONNUM; ++i)
                    {
                        if (!showFlag[i])
                        {
                            iconObject[i] = Instantiate(iconPrefab[1]);
                            Destroy(iconObject[i], CSVReader.data[number].keepTime);
                            break;
                        }
                    }
                    break;
            }            
            number++;
        }
    }

    //Destroy���\�b�h�̈����Ő��b����w��ł���
    //private void DestroyTimer(float time, int num)
    //{
    //    Debug.Log(time);
    //    float destroyTimer = 0;
    //    while (destroyTimer < time)
    //    {
    //        destroyTimer += Time.deltaTime;
    //        Debug.Log(destroyTimer);
    //        if (destroyTimer > time)
    //        {
    //            Debug.Log("ijfi");
    //            showFlag[num] = false;
    //            Destroy(iconObject[num]);
    //            break;
    //        }
    //    }
    //}
}
