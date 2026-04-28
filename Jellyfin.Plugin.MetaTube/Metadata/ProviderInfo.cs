using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// 提供商信息基类
/// 包含基本的提供商标识和主页信息
/// 作为其他元数据类的基类使用
/// </summary>
public class ProviderInfo
{
    /// <summary>
    /// 提供商中的唯一标识 ID
    /// 用于在特定提供商中定位资源
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// 提供商名称
    /// 标识元数据的来源提供商
    /// </summary>
    [JsonPropertyName("provider")]
    public string Provider { get; set; }

    /// <summary>
    /// 提供商主页 URL
    /// 指向提供商网站的链接
    /// </summary>
    [JsonPropertyName("homepage")]
    public string Homepage { get; set; }
}
