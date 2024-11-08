using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVPlayer : MonoBehaviour
{
    #region インスタンスへのstaticなアクセスポイント
    public static CSVPlayer Instance
    {
        get { return instance; }
    }
    private static CSVPlayer instance = null;
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
    [Header("曲のBPM")]
    [SerializeField] private float BPM;
    private const int FPS = 60;
    [SerializeField] public static string csvFileName;

    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    public void StartPlayback()
    {
        timer = 0;
        number = 0;
    }

    void Update()
    {
        if (number >= CSVAAAA.data.Count)
            return;

        timer += Time.deltaTime * (BPM / FPS);

        if (number < CSVAAAA.data.Count && timer > CSVAAAA.data[number].time)
        {
            LogManager.Instance.LogMusicData(CSVAAAA.data[number]);
            ManageCreationAndDestruction(CSVAAAA.data[number]);
            number++;
        }
    }

    public List<GameObject> iconObj = new List<GameObject>();

    private void ManageCreationAndDestruction(CSVAAAA.MusicData musicData)
    {
        GameObject iconObject = Instantiate(iconPrefab[musicData.direction - 1]);
        iconObj.Add(iconObject);
        Destroy(iconObject, musicData.keepTime);
        StartCoroutine(LogKeepTimeEnd(iconObject, musicData.keepTime));
    }

    private IEnumerator LogKeepTimeEnd(GameObject obj, float keepTime)
    {
        yield return new WaitForSeconds(keepTime);
        LogManager.Instance.LogKeepTimeEnd();
        iconObj.Remove(obj);
        Destroy(obj);
    }
}