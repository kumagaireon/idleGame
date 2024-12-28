using UnityEngine;
using UnityEngine.Video;

namespace IdolGame.Sample;

public class GoogleDriveVideoLoader: MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; 
    //Google DriveのファイルIDを入力してください
    private string googleDriveFileID = "YOUR_GOOGLE_DRIVE_FILE_ID";

    void Start()
    {
        // Google Driveの共有リンクを適切な形式に変換
        string googleDriveURL = $"https://drive.google.com/uc?export=download&id={googleDriveFileID}";
        
        // VideoPlayerのURLを設定
        videoPlayer.url = googleDriveURL;
        
        // 動画の再生開始
        videoPlayer.Play();
    }
}