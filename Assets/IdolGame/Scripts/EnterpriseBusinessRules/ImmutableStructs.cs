using System;
using System.Text.Json.Serialization;

namespace IdolGame.EnterpriseBusinessRules;

public readonly record struct SaveData( 
    [property: JsonPropertyName("id")] SaveDataId Id,
    [property: JsonPropertyName("is_auto_save")] bool IsAutoSave,
    [property: JsonPropertyName("saved_location_name")]  string SavedLocationName,
    [property: JsonPropertyName("saved_at")]  DateTimeOffset SavedAt)
{
}

//ライブ画面関係、選曲画面関係
public readonly record struct MusicData(
    [property:JsonPropertyName("id")] MusicId Id,
    [property:JsonPropertyName("name")] MusicName Name,
    [property:JsonPropertyName("image_path")] MusicImagePath ImagePath,
    [property:JsonPropertyName("description")] MusicDescription Description)
{}

//ライブが画面関係
public readonly record struct LiveData(
    [property:JsonPropertyName("video_id")] VideoLiveID VideoId,
    [property:JsonPropertyName("notes_id")] NotesID NotesID,
    [property:JsonPropertyName("sound_id")] SoundID SoundID,
    [property:JsonPropertyName("call_id")] CallID CallID)
{}

//推し選択画面関係
public readonly record struct AlbumData(
    [property:JsonPropertyName("id")] AlbumId Id,
    [property:JsonPropertyName("name")] AlbumName Name,
    [property:JsonPropertyName("image_path")] AlbumImagePath ImagePath,
    [property:JsonPropertyName("description")] AlbumDescription Description,
    [property:JsonPropertyName("recommendation")] AlbumRecommendation Recommendation)
{
}
