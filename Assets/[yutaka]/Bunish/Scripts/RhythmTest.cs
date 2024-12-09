using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTest : MonoBehaviour
{
    public static RhythmTest Instance;

    private List<float> expectedTimes = new List<float> { 3f, 5f, 7f, 9f, 11f, 13f, 15f, 17f, 19f, 21f, 23f, 25f, 27f, 29f };
    private List<float> inputTimes = new List<float>();
    private List<float> lags = new List<float>();

    private float startTime;
    private float tolerance = 0.5f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float inputTime = Time.time - startTime;
            inputTimes.Add(inputTime);
            //Debug.Log(inputTime + "second");
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            CulcLag();
        }
    }

    public void AddInputTime(float time)
    {
        float inputTime = time - startTime;
        inputTimes.Add(inputTime);
    }

    private void CulcLag()
    {
        int inputIndex = 0;

        for(int i = 0; i < expectedTimes.Count; i++)
        {
            while(inputIndex < inputTimes.Count)
            {
                float lag = inputTimes[inputIndex] - expectedTimes[i];
                if(Mathf.Abs(lag) < tolerance)
                {
                    Debug.Log(
                        "Expected time: " + expectedTimes[i]
                        + "s, Input time:" + inputTimes[inputIndex]
                        + "s, Lag:" + lag + "s");
                    lags.Add(lag);
                    inputIndex++;
                    break;
                }
                else if (inputTimes[inputIndex] < expectedTimes[i] - tolerance)
                {
                    inputIndex++;
                }
                else
                {
                    break;
                }
            }
        }
        float sumOfLag = 0f;
        for(int i = 0; i < lags.Count; i++)
        {
            sumOfLag += lags[i];
        }
        float averageLag = sumOfLag / lags.Count;
        Debug.Log("averageLag:" + averageLag);
    }
}
