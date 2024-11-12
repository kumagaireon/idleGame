using Cysharp.Threading.Tasks;
using UnityEngine;
using Kumagai.Entites;
using Kumagai.InterfaceAdapters;
using Kumagai.UseCase;

namespace Kumagai.FrameworksDrivers
{
    public class InputManager : MonoBehaviour
    {
        public float touchInterval = 0.1f;
        public float swipeThreshold = 2f;
        [SerializeField] private GameObject cyalumeObj;

        private SwipeData swipeData;
        private ISwipeinputUseCase swipeInputUseCase;
        private ITapInputUseCase tapInputUseCase;
        private SwipeJudge swipeJudge;

        private void Awake()
        {
            swipeData = new SwipeData();
            swipeJudge = GetComponent<SwipeJudge>();
            if (swipeJudge != null)
            {
                swipeInputUseCase = new SwipeInputUseCase(swipeJudge);
            }
            else
            {
                Debug.LogError("SwipeJudge is null in Awake. Please check the component on the GameObject.");
            }

            tapInputUseCase = new TapInputUseCase();
            cyalumeObj.SetActive(false);
        }

        private void Update()
        {
            tapInputUseCase.CheckTap(0.5f, cyalumeObj, swipeData);
            UpdateCyalumePosition();
            if (swipeData.isSwiping)
            {
            }
            SwipeInput();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ManageSwipingState().Forget();

            }
        }

        //仮でtrue/falseの制御
        //
        private async UniTask ManageSwipingState()
        {
            Debug.Log(swipeData.isSwiping);
            swipeData.isSwiping = true;
            await UniTask.DelayFrame(10000); // さらに5フレーム待機
            Debug.Log(swipeData.isSwiping);
            swipeData.isSwiping = false;
        }

        private void LogSwipeCount()
        {
            // オブジェクトが破棄される際にスワイプ回数をログに出力
            Debug.Log($"Total Swipes: {swipeData.swipeCount}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SwipeInput()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                swipeInputUseCase.IntitalizeSwipe(Input.mousePosition, swipeData);
                cyalumeObj.SetActive(true);
               // Debug.Log(swipeData.isSwiping);
            }
            else if (Input.GetMouseButton(0) && swipeData.isSwiping)
            {
                swipeInputUseCase.UpdateSwipe(Input.mousePosition, touchInterval, swipeThreshold, swipeData);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swipeInputUseCase.EndSwipe(swipeData);
                cyalumeObj.SetActive(false);
                LogSwipeCount();
             //   Debug.Log(swipeData.isSwiping);
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    swipeInputUseCase.IntitalizeSwipe(touch.position, swipeData);
                    cyalumeObj.SetActive(true);
                }
                else if (touch.phase == TouchPhase.Moved && swipeData.isSwiping)
                {
                    swipeInputUseCase.UpdateSwipe(touch.position, touchInterval, swipeThreshold, swipeData);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    swipeInputUseCase.EndSwipe(swipeData);
                    cyalumeObj.SetActive(false);
LogSwipeCount();
                }
            }
#endif
        }

        // Cyalumeの位置を更新するメソッド
        private void UpdateCyalumePosition()
        {
            if (cyalumeObj.activeSelf)
            {
                Vector3 cursorPosition = Input.mousePosition;
                cursorPosition.z =
                    10; // カメラのNearClipPlaneなどを考慮して調整
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
                cyalumeObj.transform.position = worldPosition;
            }
        }
    }
}