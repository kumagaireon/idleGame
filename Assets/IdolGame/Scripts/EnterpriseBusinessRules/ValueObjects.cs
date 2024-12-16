using UnitGenerator;

namespace IdolGame.EnterpriseBusinessRules;

//SaveData
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct  SaveDataId
{
    
}
// アイドルグループ
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupId
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupName
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupImagelogoPath
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolGroupDescription
{
}

//アイドル単体
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolId
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolName
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolImagePath
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardVoicePath
{
}

[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolCollarCode
{
}

//報酬D
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardChekiImage1Path
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardChekiImage2Path
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardChekiImage3Path
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolRewardVideoPath
{}

//自己紹介
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolSelfIntroductionText
{}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolSelfIntroductionVoicePath
{}

[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct IdolFavoriteImagePath
{}

//結果
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
public readonly partial struct MusicVideoPath
{
}


//LiveData
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct VideoLiveID
{
}
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct NotesID
{
}
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct SoundID
{
}
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct CallID
{
}

//シーンUIData

[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct BackgroundImagePath
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct DialogueSpeechBubbleImagePath
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct SerifText
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ButtonUIPath
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct ButtonText
{
}