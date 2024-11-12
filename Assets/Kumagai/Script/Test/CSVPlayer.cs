using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVPlayer : MonoBehaviour
{
    #region //インスタンスへのstaticなアクセスポイント
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
    private float timer = 0;// タイマー変数
    private int number = 0;// CSVデータのインデックス
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
            return;// インデックスがデータの数を超えたら処理を終了

        timer += Time.deltaTime * (BPM / FPS);

        if (number < CSVAAAA.data.Count && timer > CSVAAAA.data[number].time)
        {
            LogManager.Instance.LogMusicData(CSVAAAA.data[number]);// 現在の音楽データをログに出力
            ManageCreationAndDestruction(CSVAAAA.data[number]);// アイコンの作成と破棄を管理
            number++;// インデックスをインクリメント
        }
    }

    public List<GameObject> iconObj = new List<GameObject>();
// アイコンの作成と破棄を管理するメソッド
    private void ManageCreationAndDestruction(CSVAAAA.MusicData musicData)
    {
        // 指定された方向のアイコンを生成
        GameObject iconObject = Instantiate(iconPrefab[musicData.direction - 1]);
        iconObj.Add(iconObject);
        Destroy(iconObject, musicData.keepTime);
        StartCoroutine(LogKeepTimeEnd(iconObject, musicData.keepTime));// 指定時間後にログを出力するコルーチンを開始
    }
// 継続時間終了後にログを出力するコルーチン
    private IEnumerator LogKeepTimeEnd(GameObject obj, float keepTime)
    {
        yield return new WaitForSeconds(keepTime);// 指定時間待機
        LogManager.Instance.LogKeepTimeEnd();// 継続時間終了をログ出力
        iconObj.Remove(obj);// アイコンオブジェクトをリストから削除
        Destroy(obj);// アイコンオブジェクトを破棄
    }
}