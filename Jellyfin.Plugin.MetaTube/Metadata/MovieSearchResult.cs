using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// 影片搜索结果类
/// 继承自 ProviderInfo，包含影片的基本搜索信息
/// 用于搜索影片时返回的简要信息
/// </summary>
public class MovieSearchResult : ProviderInfo
{
    /// <summary>
    /// 演员列表
    /// 影片中出演的演员名称数组
    /// </summary>
    [JsonPropertyName("actors")]
    public string[] Actors { get; set; }

    /// <summary>
    /// 封面图 URL
    /// 影片的封面图片链接
    /// </summary>
    [JsonPropertyName("cover_url")]
    public string CoverUrl { get; set; }

    /// <summary>
    /// 影片编号
    /// 影片的唯一编号标识（如：ABC-123）
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; }

    /// <summary>
    /// 发布日期
    /// 影片的发行日期
    /// </summary>
    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// 评分
    /// 影片的社区评分（通常为 0-10 分）
    /// </summary>
    [JsonPropertyName("score")]
    public float Score { get; set; }

    /// <summary>
    /// 缩略图 URL
    /// 影片的缩略图链接
    /// </summary>
    [JsonPropertyName("thumb_url")]
    public string ThumbUrl { get; set; }

    /// <summary>
    /// 标题
    /// 影片的标题名称
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }
}
