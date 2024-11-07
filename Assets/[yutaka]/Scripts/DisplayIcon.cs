using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[DefaultExecutionOrder(1)]
//タップするアイコンを表示するコントローラー
public class DisplayIcon : MonoBehaviour
{
    #region　インスタンスへのstaticなアクセスポイント
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

    [Header("タップするアイコン")]
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

        //カウントが要素数より小さく、表示時間に達したときにオブジェクト表示
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

        //ノーツの数をあらかじめ想定してメモリを確保する方法→オブジェクトプールへ
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
