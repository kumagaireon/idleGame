using UnityEngine;

public class TapTest : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        // PC の場合の入力処理
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log("マウスクリック: " + mousePosition);
        }
#else
        // モバイルの入力処理
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            Debug.Log("タッチ検出: " + touchPosition);
        }
#endif
        if (Input.touchCount == 0)
        {
            //画面に触れていない場合の処理 
        }

        if(OnTouchDown())
        {
            Debug.Log("タップされました");
        }
    }

    //スマホ向け そのオブジェクトがタッチされていたらtrue（マルチタップ対応）
    bool OnTouchDown()
    {
        // タッチされているとき
        if (0 < Input.touchCount)
        {
            // タッチされている指の数だけ処理
            for (int i = 0; i < Input.touchCount; i++)
            {
                // タッチ情報をコピー
                Touch t = Input.GetTouch(i);
                // タッチしたときかどうか
                if (t.phase == TouchPhase.Began)
                {
                    //タッチした位置からRayを飛ばす
                    Ray ray = Camera.main.ScreenPointToRay(t.position);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit))
                    {
                        //Rayを飛ばしてあたったオブジェクトが自分自身だったら
                        if (hit.collider.gameObject == this.gameObject)
                        {
                            return true;
                        }
                    }
                    Debug.Log("タッチされた");
                    return true;
                }
            }
        }
        return false; //タッチされてなかったらfalse
    }
}
