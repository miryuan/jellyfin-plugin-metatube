using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// 错误信息类
/// 用于存储 API 请求失败时返回的错误详情
/// </summary>
public class ErrorInfo
{
    /// <summary>
    /// 错误代码
    /// 数值型的错误标识码
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// 错误消息
    /// 人类可读的错误描述信息
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
