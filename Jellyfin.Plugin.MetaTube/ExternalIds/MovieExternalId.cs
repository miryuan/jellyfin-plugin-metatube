using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Model.Entities;
#if !__EMBY__
using MediaBrowser.Model.Providers;
#endif

namespace Jellyfin.Plugin.MetaTube.ExternalIds;

/// <summary>
/// 电影外部 ID 类
/// 用于标识和管理电影的外部 ID 信息
/// </summary>
public class MovieExternalId : BaseExternalId
{
#if !__EMBY__
    /// <summary>
    /// 外部 ID 媒体类型（仅 Jellyfin）
    /// 指定为电影类型
    /// </summary>
    public override ExternalIdMediaType? Type => ExternalIdMediaType.Movie;
#endif

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
