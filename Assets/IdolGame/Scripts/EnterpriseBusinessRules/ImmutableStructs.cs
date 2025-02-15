using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IdolGame;

public readonly record struct SaveData( 
    [property: JsonPropertyName("id")] SaveDataId Id,
    [property: JsonPropertyName("is_auto_save")] bool IsAutoSave,
    [property: JsonPropertyName("saved_location_name")]  string SavedLocationName,
    [property: JsonPropertyName("saved_at")]  DateTimeOffset SavedAt)
{
}

//ゲーム開始時に保存する情報
public readonly record struct GlobalStateData(
    [property: JsonPropertyName("group-id")] int? GroupId,
    [property: JsonPropertyName("idol-id")] int? IdolId )
{}
//アイドルグループ
public readonly record struct IdolGroupData(
    [property: JsonPropertyName("group-id")] IdolGroupId GroupId,
    [property: JsonPropertyName("group-name")] IdolGroupName GroupName,
    [property: JsonPropertyName("group-image-logo-path")]  IdolGroupImagelogoPath GroupImagelogoPath,
    [property: JsonPropertyName("background-image-path")]  BackgroundImagePath　BackgroundImagePath,
    [property: JsonPropertyName("idol-button-ui-path")]  IdolGroupButtonUIPath　IdolButtonUIPath,
    [property: JsonPropertyName("members")] IdolMembersData[]? Members)
{
}
//アイドル
public readonly record struct IdolMembersData(
    [property: JsonPropertyName("id")] IdolId Id,
    [property: JsonPropertyName("idol-name")] IdolName Name,
    [property: JsonPropertyName("image-path")]  IdolImagePath ImagePath,
    [property: JsonPropertyName("collar-code")]  IdolCollarCode CollarCode,
    [property: JsonPropertyName("serif-menu-text")]  SerifMenuText SerifMenuText,
    [property: JsonPropertyName("idol-reward")]  IdolRewardData IdolReward,
    [property: JsonPropertyName("idol-self-introduction")]  IdolSelfIntroductionData IdolSelfIntroduction,
    [property: JsonPropertyName("result-idol")]  ResultIdolData ResultIdol )
{
}
//報酬
public readonly record struct IdolRewardData(
    [property: JsonPropertyName("reward-cheki-image-1-path")] IdolRewardChekiImage1Path RewardChekiImage1Path,
    [property: JsonPropertyName("reward-cheki-image-2-path")] IdolRewardChekiImage2Path RewardChekiImage2Path,
    [property: JsonPropertyName("reward-cheki-image-3-path")] IdolRewardChekiImage3Path RewardChekiImage3Path,
    [property: JsonPropertyName("reward-vidce-path")] IdolRewardVideoPath RewardVideoPath,
    [property: JsonPropertyName("date-acquisitio-reward-check-1")]  DateTimeOffset DateAcquisitioRewardCheck1,
    [property: JsonPropertyName("date-acquisitio-reward-check-2")]  DateTimeOffset DateAcquisitioRewardCheck2,
    [property: JsonPropertyName("date-acquisitio-reward-check-3")]  DateTimeOffset DateAcquisitioRewardCheck3,
    [property: JsonPropertyName("date-acquisitio-reward-video")]  DateTimeOffset DateAcquisitioRewardVideo,
    [property: JsonPropertyName("cheki-1-point")]  IdolRewardCheki1Point Cheki1Point,
    [property: JsonPropertyName("cheki-2-point")]  IdolRewardCheki2Point Cheki2Point,
    [property: JsonPropertyName("cheki-3-point")]  IdolRewardCheki3Point Cheki3Point,
    [property: JsonPropertyName("video-point")]  IdolRewardVoicePoint VoicePoint,
    [property: JsonPropertyName("idol-point")]  IdolRewardPoint IdolPoint)
{
}
//自己紹介
public readonly record struct IdolSelfIntroductionData(
    [property: JsonPropertyName("self-introduction-text")] IdolSelfIntroductionText SelfIntroductionText,
    [property: JsonPropertyName("self-introduction-voice-path")] IdolSelfIntroductionVoicePath SelfIntroductionVoicePath,
    [property: JsonPropertyName("favorite-image-path")] IdolFavoriteImagePath FavoriteImagePath)
{}
//結果
public readonly record struct ResultIdolData(
    [property: JsonPropertyName("s-rank")] SRankText SRank,
    [property: JsonPropertyName("a-rank")] ARankText ARank,
    [property: JsonPropertyName("b-rank")] BRankText BRank,
    [property: JsonPropertyName("c-rank")] CRankText CRank,
    [property: JsonPropertyName("s-rank-point")] SRankPoint SRankPoint,
    [property: JsonPropertyName("a-rank-point")] ARankPoint ARankPoint,
    [property: JsonPropertyName("b-rank-point")] BRankPoint BRankPoint,
    [property: JsonPropertyName("c-rank-point")] CRankPoint CRankPoint,
    [property: JsonPropertyName("s-rank-voice")] SRankVoicePath SRankVoice,
    [property: JsonPropertyName("a-rank-voice")] ARankVoicePath ARankVoice,
    [property: JsonPropertyName("b-rank-voice")] BRankVoicePath BRankVoice,
    [property: JsonPropertyName("c-rank-voice")] CRankVoicePath CRankVoice)
{}

//選曲Data
public readonly record struct MusicData(
    [property:JsonPropertyName("id")] MusicId Id,
    [property:JsonPropertyName("name")] MusicName Name,
    [property:JsonPropertyName("image_path")] MusicImagePath ImagePath,
    [property:JsonPropertyName("description")] MusicDescription Description,
    [property:JsonPropertyName("video-path")] MusicVoicePath VideoPath,
    [property:JsonPropertyName("csv-path")] CsvFilePath CsvPath,
    [property:JsonPropertyName("max-score")] float MaxScorePoint
    )
{}

public readonly record struct ScoreData(
    [property:JsonPropertyName("score-point")] float ScorePoint
)
{}