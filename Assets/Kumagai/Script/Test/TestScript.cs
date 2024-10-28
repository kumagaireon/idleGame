using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    #region インスタンスへのstaticなアクセスポイント
    public static TestScript Instance
    {
        get { return instance; }
    }
    private static TestScript instance = null;
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
    [SerializeField] private GameObject[] iconObject = new GameObject[ICONNUM];
    private bool[] showFlag = new bool[ICONNUM];

    private void Update()
    {
        timer += Time.deltaTime; // Debug.Log(timer);
        if (number < CSVReader.data.Count && timer > CSVReader.data[number].time)
        {
            switch (CSVReader.data[number].type)
            {
                case 1:
                    for (int i = 0; i < ICONNUM; ++i)
                    {
                        if (!showFlag[i])
                        {
                            //もしInputHandlerのisSwip=trueならCount++するコードを追加
                            InputHandler inputHandler = FindObjectOfType<InputHandler>();
                            if (inputHandler != null)
                            {
                                inputHandler.isSwip = true;
                            }
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
                            //もしInputHandlerのisSwip=trueならCount++するコードを追加
                         

                            InputHandler inputHandler = FindObjectOfType<InputHandler>();
                            if (inputHandler != null)
                            {
                                inputHandler.isSwip = true;
                            }
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
}