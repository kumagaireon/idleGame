using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;    // スコアを表示するためのTextMeshProUGUIオブジェクト
    [SerializeField] TextMeshProUGUI perfectText;  // パーフェクトの数を表示するためのTextMeshProUGUIオブジェクト
    [SerializeField] TextMeshProUGUI greatText;    // グレートの数を表示するためのTextMeshProUGUIオブジェクト
    [SerializeField] TextMeshProUGUI badText;      // バッドの数を表示するためのTextMeshProUGUIオブジェクト
    [SerializeField] TextMeshProUGUI missText;     // ミスの数を表示するためのTextMeshProUGUIオブジェクト

    // 有効になったときに呼び出されるメソッド
    private void OnEnable()
    {
        // GManagerから各スコア情報を取得してテキストに反映
        scoreText.text = GManagerReon.instance.score.ToString();
        perfectText.text = GManagerReon.instance.perfect.ToString();
        greatText.text = GManagerReon.instance.great.ToString();
        badText.text = GManagerReon.instance.bad.ToString();
        missText.text = GManagerReon.instance.miss.ToString();
    }

    // リトライボタンが押されたときに呼び出されるメソッド
    public void Retry()
    {
        // GManagerのスコア情報をリセット
        GManagerReon.instance.perfect = 0;
        GManagerReon.instance.great = 0;
        GManagerReon.instance.bad = 0;
        GManagerReon.instance.miss = 0;
        GManagerReon.instance.maxScore = 0;
        GManagerReon.instance.ratioScore = 0;
        GManagerReon.instance.score = 0;
        GManagerReon.instance.combo = 0;

        // "MusicScene" シーンを再ロード
        SceneManager.LoadScene("MusicScene");
    }
}
