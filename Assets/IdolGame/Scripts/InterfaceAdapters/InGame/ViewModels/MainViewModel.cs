using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Common.ViewModels;
using IdolGame.InGame.Views;
using Microsoft.Extensions.Logging;
using UnityEngine.UIElements;

namespace IdolGame.InGame.ViewModels;

public sealed class MainViewModel: ViewModelBase<MainView>
{
    // ログ記録用のロガー
    readonly ILogger<MainViewModel> logger;
 
    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view,
        UIDocument rootDocument)
        : base(view, rootDocument, new FadeViewTransition(rootDocument))
    {
        this.logger = logger;
    }

    public async UniTask InitializeAsync(CancellationToken ct)
    {   
        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }

    public override void PreOpen()
    {
    }

    protected override void OnDispose()
    {
    }
}