#if !__EMBY__
using Jellyfin.Plugin.MetaTube.ExternalIds;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;

namespace Jellyfin.Plugin.MetaTube.Providers;

/// <summary>
/// 外部 URL 提供器
/// 为电影和演员提供外部链接
/// 仅 Jellyfin 平台使用
/// </summary>
public class ExternalUrlProvider : IExternalUrlProvider
{
    public string Name => Plugin.ProviderName;

    public IEnumerable<string> GetExternalUrls(BaseItem item)
    {
        if (item.TryGetProviderId(Plugin.ProviderId, out var pid))
        {
            switch (item)
            {
                case Movie:
                    yield return string.Format(new MovieExternalId().UrlFormatString, pid);
                    break;
                case Person:
                    yield return string.Format(new ActorExternalId().UrlFormatString, pid);
                    break;
            }
        }
    }
}
#endif
