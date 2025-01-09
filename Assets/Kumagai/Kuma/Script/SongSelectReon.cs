using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SongSelectReon : MonoBehaviour
{
    // データベースの参照
    [SerializeField] SongDataBaseReon dataBase;

    // 曲名を表示するためのTextMeshProUGUIの配列
    [SerializeField] TextMeshProUGUI[] songNameText;

    // 曲のレベルを表示するためのTextMeshProUGUIの配列
    [SerializeField] TextMeshProUGUI[] songLevelText;

    // CSVファイル名を表示するためのTextMeshProUGUIの配列
    [SerializeField] TextMeshProUGUI[] csvFileText;

    // 曲の画像を表示するためのImageオブジェクト [
    [SerializeField] Image songImage;


    AudioSource audio; // オーディオソースコンポーネント
    AudioClip Music; // 現在再生中の曲のオーディオクリップ
    string songName; // 現在選択中の曲名
    int select; // 現在選択中のインデックス
    [SerializeField] private string selectedVideoPath; // 選択されたビデオのパス
    public static string csvFileName { get; private set; } // CSVファイル名のプロパティ

    private void Start()
    {
        // 最初の選択を0に設定
        select = 0;
        // AudioSourceコンポーネントを取得
        audio = GetComponent<AudioSource>();
        // 最初の曲の情報を取得
        songName = dataBase.songData[select].songName;
        selectedVideoPath = dataBase.songData[select].selectedVideoPath; 
        // リソースから最初の曲をロード
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        // すべての曲情報を更新
        SongUpdateALL();
    }

    void Update()
    {
        // 下矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (select < dataBase.songData.Length - 1) // インデックスがデータベースの範囲内であるかを確認
            {
                select++; // インデックスを増加
                SongUpdateALL(); // すべての曲情報を更新
            }
        }

        // 上矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (select > 0) // インデックスが0以上であるかを確認
            {
                select--; // インデックスを減少
                SongUpdateALL(); // すべての曲情報を更新
            }
        }

        // スペースキーが押された場合
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SongStart(); // 曲を再生開始
        }
    }

    // すべての曲情報を更新するメソッド
    private void SongUpdateALL()
    {
        // 現在の選択中の曲の情報を取得
        songName = dataBase.songData[select].songName;
        selectedVideoPath = dataBase.songData[select].selectedVideoPath;

        // リソースから現在の曲をロード
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        // 現在再生中の曲を停止
        audio.Stop();
        // 新しい曲を再生
        audio.PlayOneShot(Music);
        // 5曲分の情報を更新
        for (int i = 0; i < 5; i++)
        {
            SongUpdate(i - 2);
        }
    }

    // 曲情報を更新するメソッド
    private void SongUpdate(int id)
    {
        try
        {
            // 曲名とレベルを対応するテキストに表示
            songNameText[id + 2].text = dataBase.songData[select + id].songName;
            songLevelText[id + 2].text = "Lv." + dataBase.songData[select + id].songLevel;
        }
        catch
        {
            // インデックスが範囲外の場合、テキストを空にする
            songNameText[id + 2].text = "";
            songLevelText[id + 2].text = "";
        }

        // 中央の曲の場合、画像を更新
        if (id == 0)
        {
            songImage.sprite = dataBase.songData[select + id].songImage; 
        }
    }

    // 曲を開始するメソッド
    public void SongStart()
    {
//<<<<<<< HEAD
        // GManagerに現在の曲IDを設定
        GManagerReon.instance.songID = select;
        // CSVファイル名を設定
        csvFileName = dataBase.songData[select].csvFileName;
        // LiveSceneをロード
        SceneManager.LoadScene("LiveScene");
    
//=======
        GManagerReon.instance.songID = select; // �I�����ꂽ�Ȃ�ID��GManager�ɐݒ�
        csvFileName = dataBase.songData[select].csvFileName;//�Ȃ̏����i�[
        SceneManager.LoadScene("Game2Scene"); // ���y�V�[�������[�h
    }    
//>>>>>>> 301bb4f56dbb50943f168bd81f73bd896e9ce57e
}