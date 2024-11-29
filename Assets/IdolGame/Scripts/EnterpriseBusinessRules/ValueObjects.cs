using UnitGenerator;

namespace IdolGame.EnterpriseBusinessRules;

//SaveData
[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct  SaveDataId
{
    
}

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

//AlbumDeta

[UnitOf(typeof(int), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct AlbumId
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct AlbumName
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct AlbumImagePath
{
}
[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct AlbumDescription
{
}

[UnitOf(typeof(bool), UnitGenerateOptions.ImplicitOperator | UnitGenerateOptions.JsonConverter)]
public readonly partial struct AlbumRecommendation
{
}