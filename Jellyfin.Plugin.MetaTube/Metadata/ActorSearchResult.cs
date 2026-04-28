using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// 演员搜索结果类
/// 继承自 ProviderInfo，包含演员的基本搜索信息
/// 用于搜索演员时返回的简要信息
/// </summary>
public class ActorSearchResult : ProviderInfo
{
    /// <summary>
    /// 演员图片 URL 数组
    /// 包含演员的多张图片链接
    /// </summary>
    [JsonPropertyName("images")]
    public string[] Images { get; set; }

    /// <summary>
    /// 演员名称
    /// 演员的显示名称
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
