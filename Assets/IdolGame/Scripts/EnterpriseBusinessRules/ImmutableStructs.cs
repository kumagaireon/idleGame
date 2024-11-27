using System.Text.Json.Serialization;

namespace IdolGame.EnterpriseBusinessRules;

public readonly record struct MusicData(
    [property:JsonPropertyName("id")] MusicId Id,
    [property:JsonPropertyName("name")] MusicName Name,
    [property:JsonPropertyName("image_path")] MusicImagePath ImagePath,
    [property:JsonPropertyName("description")] MusicDescription Description)
{}

public readonly record struct LiveData(
    [property:JsonPropertyName("video_id")] VideoLiveID VideoId,
    [property:JsonPropertyName("notes_id")] NotesID NotesID,
    [property:JsonPropertyName("sound_id")] SoundID SoundID,
    [property:JsonPropertyName("call_id")] CallID CallID)
{}
public readonly record struct AlbumData(
    [property:JsonPropertyName("id")] AlbumId Id,
    [property:JsonPropertyName("name")] AlbumName Name,
    [property:JsonPropertyName("image_path")] AlbumImagePath ImagePath,
    [property:JsonPropertyName("description")] AlbumDescription Description,
    [property:JsonPropertyName("recommendation")] AlbumRecommendation Recommendation)
{
}
