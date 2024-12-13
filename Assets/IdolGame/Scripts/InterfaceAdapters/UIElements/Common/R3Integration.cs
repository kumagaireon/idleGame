using R3;
using UnityEngine.UIElements;

namespace IdolGame.UIElements;

public static class R3Integration
{
    public static Observable<Unit> OnClickAsObservable(this Button button)
        => Observable.FromEvent(h => button.clicked += h, h => button.clicked -= h);

    public static Observable<PointerDownEvent> OnInputAsObservable(this VisualElement visualElement)
        => Observable.FromEvent<EventCallback<PointerDownEvent>, PointerDownEvent>(
            h => (x) => h(x), 
            h => visualElement.RegisterCallback(h),
            h => visualElement.UnregisterCallback(h));


}