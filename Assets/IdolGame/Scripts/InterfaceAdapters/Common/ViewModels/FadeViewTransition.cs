using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine.UIElements;

namespace IdolGame.Common.ViewModels;

public class FadeViewTransition:IViewTransition
{ 
    const float Transparent = 0.0f;
    const float Opaque = 1.0f;
    const float Duration = 1.0f;
    
    readonly VisualElement fadeViewElement = new() { name = "fade-view-transition-element" };
    readonly UIDocument rootDocument; 
    bool isAdded;

    public FadeViewTransition(UIDocument rootDocument)
    {
        this.rootDocument = rootDocument;
        fadeViewElement.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
    }
    
   

    public void AddTransition()
    {
        if (isAdded)
        {
            return;
        }

        isAdded = true;
        rootDocument.rootVisualElement.Add(fadeViewElement);
    }
   
    public void RemoveTransition()
    {
        if (!isAdded)
        {
            return;
        }

        isAdded = false;
        rootDocument.rootVisualElement.Remove(fadeViewElement);
    }

    public async UniTask TransitionInAsync(SceneTransitionState state, CancellationToken ct)
    {
        fadeViewElement.style.opacity = Opaque;
        AddTransition();
        
        await LMotion.Create(Opaque, Transparent, Duration)
            .Bind(x => fadeViewElement.style.opacity = x)
            .ToUniTask(ct);
        
        RemoveTransition();
    }

    public async UniTask TransitionOutAsync(SceneTransitionState state, CancellationToken ct)
    {
        AddTransition();
        
        await LMotion.Create(Transparent, Opaque, Duration)
            .Bind(x => fadeViewElement.style.opacity = x)
            .ToUniTask(ct);
    }
    
    public void Dispose()
    {
        fadeViewElement.UnregisterCallback<MouseDownEvent>(OnMouseDownEvent);
    }
    
    void OnMouseDownEvent(MouseDownEvent e) => fadeViewElement.focusController.IgnoreEvent(e);
}