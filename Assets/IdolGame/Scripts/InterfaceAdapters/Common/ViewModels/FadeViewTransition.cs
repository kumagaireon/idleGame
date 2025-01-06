using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine.UIElements;

namespace IdolGame.Common.ViewModels;

/// <summary>
/// フェードイン・フェードアウトのビュー遷移を制御するクラス
/// </summary>
public class FadeViewTransition:IViewTransition
{ 
    // フェードアニメーションの透明度と不透明度を定義する定数
    const float Transparent = 0.0f;
    const float Opaque = 1.0f;
    const float Duration = 1.0f;// アニメーションの継続時間
    // フェードアニメーションを表示するためのVisualElement
    readonly VisualElement fadeViewElement = new() { name = "fade-view-transition-element" };
    readonly UIDocument rootDocument; // ルートUIDocument
    bool isAdded;// フェードエレメントが追加されたかどうかを示すフラグ

    // コンストラクタ
    public FadeViewTransition(UIDocument rootDocument)
    {
        this.rootDocument = rootDocument;
        fadeViewElement.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
    }
    
    // フェードエレメントを追加
    public void AddTransition()
    {
        if (isAdded)
        {
            return;
        }

        isAdded = true;
        rootDocument.rootVisualElement.Add(fadeViewElement);
    }
   
    // フェードエレメントを削除
    public void RemoveTransition()
    {
        if (!isAdded)
        {
            return;
        }

        isAdded = false;
        rootDocument.rootVisualElement.Remove(fadeViewElement);
    }

    // フェードインアニメーションを実行
    public async UniTask TransitionInAsync(SceneTransitionState state, CancellationToken ct)
    {
        fadeViewElement.style.opacity = Opaque;// 初期状態を不透明に設定
        AddTransition();
        // 不透明から透明へのアニメーション
        await LMotion.Create(Opaque, Transparent, Duration)
            .Bind(x => fadeViewElement.style.opacity = x)
            .ToUniTask(ct);
        
        RemoveTransition();
    }

    // フェードアウトアニメーションを実行
    public async UniTask TransitionOutAsync(SceneTransitionState state, CancellationToken ct)
    {
        AddTransition();
        // 透明から不透明へのアニメーション
        await LMotion.Create(Transparent, Opaque, Duration)
            .Bind(x => fadeViewElement.style.opacity = x)
            .ToUniTask(ct);
    }
    
    // オブジェクトを破棄
    public void Dispose()
    {
        fadeViewElement.UnregisterCallback<MouseDownEvent>(OnMouseDownEvent);// マウスダウンイベントの登録解除
    }
    // マウスダウンイベントの処理
    void OnMouseDownEvent(MouseDownEvent e) => fadeViewElement.focusController.IgnoreEvent(e);
}