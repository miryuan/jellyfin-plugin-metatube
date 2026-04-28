using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// 影片详细信息类
/// 继承自 MovieSearchResult，包含影片的完整详细信息
/// 用于存储影片的完整元数据
/// </summary>
public class MovieInfo : MovieSearchResult
{
    /// <summary>
    /// 大封面图 URL
    /// 高分辨率的封面图片链接
    /// </summary>
    [JsonPropertyName("big_cover_url")]
    public string BigCoverUrl { get; set; }

    /// <summary>
    /// 大缩略图 URL
    /// 高分辨率的缩略图链接
    /// </summary>
    [JsonPropertyName("big_thumb_url")]
    public string BigThumbUrl { get; set; }

    /// <summary>
    /// 导演
    /// 影片的导演名称
    /// </summary>
    [JsonPropertyName("director")]
    public string Director { get; set; }

    /// <summary>
    /// 标签/类型数组
    /// 影片的类型标签列表（如：剧情、动作等）
    /// </summary>
    [JsonPropertyName("genres")]
    public string[] Genres { get; set; }

    /// <summary>
    /// 制片商
    /// 影片的制作公司名称
    /// </summary>
    [JsonPropertyName("maker")]
    public string Maker { get; set; }

    /// <summary>
    /// 预览图片数组
    /// 影片的预览截图列表
    /// </summary>
    [JsonPropertyName("preview_images")]
    public string[] PreviewImages { get; set; }

    /// <summary>
    /// HLS 预览视频 URL
    /// HLS 格式的预览视频流链接
    /// </summary>
    [JsonPropertyName("preview_video_hls_url")]
    public string PreviewVideoHlsUrl { get; set; }

    /// <summary>
    /// 预览视频 URL
    /// 标准格式的预览视频链接
    /// </summary>
    [JsonPropertyName("preview_video_url")]
    public string PreviewVideoUrl { get; set; }

    /// <summary>
    /// 标签/品牌
    /// 影片的标签或品牌信息
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; set; }

    /// <summary>
    /// 时长（分钟）
    /// 影片的总时长
    /// </summary>
    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }

    /// <summary>
    /// 系列
    /// 影片所属的系列名称
    /// </summary>
    [JsonPropertyName("series")]
    public string Series { get; set; }

    /// <summary>
    /// 简介
    /// 影片的详细描述和剧情介绍
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }
}
