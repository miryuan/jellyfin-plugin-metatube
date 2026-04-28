using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
#if !__EMBY__
using MediaBrowser.Model.Providers;
#endif

namespace Jellyfin.Plugin.MetaTube.ExternalIds;

/// <summary>
/// 外部 ID 基类
/// 提供外部 ID 的基础实现，所有具体的外部 ID 类都继承此类
/// 实现 IExternalId 接口，用于在 Jellyfin/Emby 中标识外部资源
/// </summary>
public abstract class BaseExternalId : IExternalId
{
#if __EMBY__
    /// <summary>
    /// 提供商名称（Emby 平台）
    /// </summary>
    public virtual string Name => Plugin.ProviderName;
#else
    /// <summary>
    /// 提供商名称（Jellyfin 平台）
    /// </summary>
    public virtual string ProviderName => Plugin.ProviderName;

    /// <summary>
    /// 外部 ID 媒体类型（仅 Jellyfin）
    /// 子类需要重写此属性以指定支持的媒体类型
    /// </summary>
    public abstract ExternalIdMediaType? Type { get; }
#endif

    /// <summary>
    /// 外部 ID 的键名
    /// 用于在 ProviderIds 字典中存储和检索
    /// </summary>
    public virtual string Key => Plugin.ProviderId;

    /// <summary>
    /// URL 格式字符串
    /// 用于生成外部资源的链接，{0} 将被替换为具体的 ID
    /// </summary>
    public virtual string UrlFormatString => Plugin.Instance.Configuration.Server + "?redirect={0}";

    /// <summary>
    /// 判断是否支持指定的项目
    /// 子类需要重写此方法以指定支持的项目类型
    /// </summary>
    /// <param name="item">要检查的项目</param>
    /// <returns>如果支持该项目则返回 true，否则返回 false</returns>
    public abstract bool Supports(IHasProviderIds item);
}
