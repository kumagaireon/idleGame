using UnityEngine;

public class InputChecker : MonoBehaviour
{    
    public static InputChecker instance;
    private bool tabAble = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool GetMouseButtonDown()
    {
        return Input.GetMouseButton(0);
    }

    public bool GetMouseButtonUp()
    {
        return Input.GetMouseButtonUp(0);
    }

    //public void SetTapAble() {
    //    tabAble = true;
    //}

    //public void SetTapNotAble()
    //{
    //    tabAble = false;
    //}
    

    public bool TappedEnter()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool InputShake()
    {
        //サイリウムの入力判定を記入する
        return true;
    }
}
