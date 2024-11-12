using UnityEngine;
using Kumagai.Entites;
using Kumagai.UseCase;

namespace Kumagai.FrameworksDrivers
{
    public class InputManager : MonoBehaviour
    {
        public float touchInterval = 0.1f;
        public float swipeThreshold = 2f;

        private SwipeData swipeData;
        private ISwipeinputUseCase swipeInputUseCase;
        private ITapInputUseCase tapInputUseCase;
        private SwipeJudge swipeJudge;

        private void Awake()
        {
            swipeData = new SwipeData();
            swipeJudge = GetComponent<SwipeJudge>();
            swipeInputUseCase = new SwipeInputUseCase(swipeJudge);
            tapInputUseCase = new TapInputUseCase();
        }

        private void Update()
        {
            tapInputUseCase.CheckTap(0.5f);
            SwipeInput();
        }

        private void OnDestroy()
        {
            // オブジェクトが破棄される際にスワイプ回数をログに出力
            Debug.Log($"Total Swipes: {swipeData.swipeCount}"); 
        }

        private void SwipeInput()
        {
            #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                swipeInputUseCase.IntitalizeSwipe(Input.mousePosition,swipeData);
            }
            else if (Input.GetMouseButton(0) && swipeData.isSwiping)
            {
                swipeInputUseCase.UpdateSwipe(Input.mousePosition, touchInterval, swipeThreshold, swipeData);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                swipeInputUseCase.EndSwipe(swipeData);
                OnDestroy();
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    swipeInputUseCase.IntitalizeSwipe(touch.position,swipeData);
                }
                else if (touch.phase == TouchPhase.Moved && swipeData.isSwiping)
                {
                    swipeInputUseCase.UpdateSwipe(touch.position, touchInterval, swipeThreshold, swipeData);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    swipeInputUseCase.EndSwipe(swipeData);
                }
            }
#endif
        }
    }
}