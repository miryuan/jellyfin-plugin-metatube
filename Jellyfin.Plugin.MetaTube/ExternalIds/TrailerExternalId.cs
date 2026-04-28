using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Model.Entities;
#if !__EMBY__
using MediaBrowser.Model.Providers;
#endif

namespace Jellyfin.Plugin.MetaTube.ExternalIds;

/// <summary>
/// 预告片外部 ID 类
/// 用于存储和管理电影预告片的 URL 信息
/// 与其他外部 ID 不同，此类专门用于预告片链接
/// </summary>
public class TrailerExternalId : BaseExternalId
{
#if __EMBY__
    /// <summary>
    /// 提供商名称（Emby 平台）
    /// 使用 "TrailerUrl" 作为标识
    /// </summary>
    public override string Name => "TrailerUrl";
#else
    /// <summary>
    /// 提供商名称（Jellyfin 平台）
    /// 使用 "TrailerUrl" 作为标识
    /// </summary>
    public override string ProviderName => "TrailerUrl";

    /// <summary>
    /// 外部 ID 媒体类型（仅 Jellyfin）
    /// 指定为电影类型
    /// </summary>
    public override ExternalIdMediaType? Type => ExternalIdMediaType.Movie;
#endif

    /// <summary>
    /// 外部 ID 的键名
    /// 使用 "TrailerUrl" 作为键
    /// </summary>
    public override string Key => "TrailerUrl";

    /// <summary>
    /// URL 格式字符串
    /// 预告片不需要外部链接，因此返回 null
    /// </summary>
    public override string UrlFormatString => null;

    /// <summary>
    /// 判断是否支持指定的项目
    /// 仅支持 Movie 类型的项目
    /// </summary>
    /// <param name="item">要检查的项目</param>
    /// <returns>如果项目是 Movie 类型则返回 true，否则返回 false</returns>
    public override bool Supports(IHasProviderIds item)
    {
        return item is Movie;
    }
}
