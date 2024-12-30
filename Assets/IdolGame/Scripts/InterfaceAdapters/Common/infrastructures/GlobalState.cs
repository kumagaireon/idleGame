using System.Collections.Generic;
using System.Text.Json;

namespace IdolGame.Common.infrastructures;

public static class GlobalState
{
    //グループ
    //ID
    public static int? GroupId { get; set; }

    //背景
    public static string? GroupBackgroundImagePath { get; set; }

    //ボタンUI
    public static string? GroupButtonUIPath { get; set; }

    //推しアイドル
    //ID
    public static int? IdolId { get; set; }

    //半身Image
    public static string? IdolImagePath { get; set; }

    //色
    public static IdolCollarCode? IdolColor { get; set; }

    //自己紹介テキスト
    public static string? IdolSerifMenuText { get; set; }

    //リザルトのSABC別の必要ポイント
    public static List<float>? IdolResultRankPoint { get; set; } = new List<float>(4);

    //リザルトのSABC別ボイス
    public static List<string>? IdolResultRankVoice { get; set; } = new List<string>(4);

    //リザルトのSABC別テキスト
    public static List<string>? IdolResultRankText { get; set; } = new List<string>(4);

    //現在ポイント
    public static float? IdolRewardPoint { get; set; }
}