using UnitGenerator;

namespace IdolGame;

//SaveData
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct  SaveDataId
{
    
}
// アイドルグループ

//グループID
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupId
{
}
//グループ名
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupName
{
}
//グループロゴ画像
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupImagelogoPath
{
}
//ボタン画像
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupButtonUIPath
{
}


//アイドル単体
//アイドルID
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolId
{
}
//アイドル名前
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolName
{
}
//アイドル決めポーズ
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolImagePath
{
}
//アイドルカラー
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolCollarCode
{
}

//報酬D
//報酬チェキ１
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardChekiImage1Path
{}
//報酬チェキ２
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardChekiImage2Path
{}
//報酬チェキ３
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardChekiImage3Path
{}
//報酬ボイス
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardVideoPath
{}
//報酬チェキ1ポイント
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardCheki1Point
{}
//報酬チェキ2ポイント
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardCheki2Point
{}
//報酬チェキ3ポイント
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardCheki3Point
{}
//報酬ボイスポイント
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardVoicePoint
{}
//合計報酬ポイント
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardPoint
{
}

//自己紹介
//自己紹介セリフ
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolSelfIntroductionText
{}
//自己紹介ボイス
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolSelfIntroductionVoicePath
{}
//推し設定画像
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolFavoriteImagePath
{}

//結果
//SABC別リザルトテキスト
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct SRankText
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ARankText
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct BRankText
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct CRankText
{}
//SABC別リザルトポイント
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct SRankPoint
{}
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ARankPoint
{}
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct BRankPoint
{}
[UnitOf(typeof(float), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct CRankPoint
{}
//SABC別リザルトボイス
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct SRankVoicePath
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ARankVoicePath
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct BRankVoicePath
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct CRankVoicePath
{}



//MusicData
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct MusicId
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct MusicName
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct MusicImagePath
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct MusicDescription
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct MusicVoicePath
{
}


//シーンUIData

//背景
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct BackgroundImagePath
{
}
//ロゴor画像
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct LogoOrImagePath
{
}

//セリフ
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct SerifText
{
}
//メニューセリフ
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct SerifMenuText
{
}
//ボタンID
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ButtonUIID
{
}
//ボタンUI
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ButtonUIPath
{
}
//ボタンテキスト
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ButtonText
{
}