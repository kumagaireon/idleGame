using System;
using System.Collections.Generic;
using R3;
using UnityEngine.UIElements;

namespace UIToolkit.R3.Integration;

    // UIElementExtensionsクラスの定義。このクラスはVisualElementおよびListViewの拡張メソッドを提供
    public static class UIElementExtensions
    {
        // VisualElementのイベントをObservableとして登録する拡張メソッド
        public static Observable<TEventType> RegisterCallbackAsObservable<TEventType>(this VisualElement source,
            TrickleDown trickleDown = TrickleDown.NoTrickleDown)
            where TEventType : EventBase<TEventType>, new()
        {
            return Observable.FromEvent<EventCallback<TEventType>, TEventType>(
                h => x => h(x),
                h => source.RegisterCallback(h, trickleDown),
                h => source.UnregisterCallback(h, trickleDown));
        }

        // VisualElementのイベントをObservableとして登録する拡張メソッド（追加の引数あり）
        public static Observable<(TEventType evt, TArgType arg)> RegisterCallbackAsObservable<TEventType, TArgType>(
            this VisualElement source, TArgType arg, TrickleDown trickleDown = TrickleDown.NoTrickleDown)
            where TEventType : EventBase<TEventType>, new()
        {
            return Observable.FromEvent<EventCallback<TEventType, TArgType>, (TEventType, TArgType)>(
                h => (x, y) => h((x, y)),
                h => source.RegisterCallback(h, arg, trickleDown),
                h => source.UnregisterCallback(h, trickleDown));
        }

        // ListViewの選択変更イベントをObservableとして取得する拡張メソッド
        public static Observable<IEnumerable<object>> SelectionChangedAsObservable(this ListView source)
        {
            return Observable.FromEvent<Action<IEnumerable<object>>, IEnumerable<object>>(
                h => h,
                h => source.selectionChanged += h,
                h => source.selectionChanged -= h);
        }

        // ListViewの選択インデックス変更イベントをObservableとして取得する拡張メソッド
        public static Observable<IEnumerable<int>> SelectedIndicesChangedAsObservable(this ListView source)
        {
            return Observable.FromEvent<Action<IEnumerable<int>>, IEnumerable<int>>(
                h => h,
                h => source.selectedIndicesChanged += h,
                h => source.selectedIndicesChanged -= h);
        }

        // ListViewのアイテム選択イベントをObservableとして取得する拡張メソッド
        public static Observable<IEnumerable<object>> ItemsChosenAsObservable(this ListView source)
        {
            return Observable.FromEvent<Action<IEnumerable<object>>, IEnumerable<object>>(
                h => h,
                h => source.itemsChosen += h,
                h => source.itemsChosen -= h);
        }


    }