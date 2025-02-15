using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Common.ViewModels;
using IdolGame.Options.Views;
using Microsoft.Extensions.Logging;
using R3;
using UnityEngine.UIElements;

namespace IdolGame.Options.ViewModels;

public sealed class MainViewModel: ViewModelBase<MainView>
{
    enum OptionsType
    {
        GraphicsSettings,
        SoundEnabled,
        BgmVolume,
        SeVolume
    }
    // ログ記録用のロガー
    readonly ILogger<MainViewModel> logger;
    OptionsType currentOptions;
    DisposableBag bag;
    
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガーのインスタンス</param>
    /// <param name="view">ビューのインスタンス</param>
    /// <param name="rootDocument">ルートドキュメントのインスタンス</param>
    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view,
        UIDocument rootDocument)
        : base(view, rootDocument, new FadeViewTransition(rootDocument))
    {
        this.logger = logger;
    }

    /// <summary>
    /// 非同期にビューを初期化するメソッド
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    public async UniTask InitializeAsync(CancellationToken ct)
    {
        
        //非同期で画像を読み込む
        var visualElement = new VisualElement(); 
        
        // visualElement.style.backgroundImage = Background.FromTexture2D("テクスチャー");
        // visualElement.style.backgroundImage = Background.FromRenderTexture("テクスチャー");
        
        
        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }

    /// <summary>
    /// ビューが開く前に実行される処理
    /// </summary>
    public override void PreOpen()
    {
    }

    /// <summary>
    /// ビューの破棄時に実行される処理
    /// </summary>
    protected override void OnDispose()
    {
    }
}