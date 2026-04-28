using Jellyfin.Plugin.MetaTube.Configuration;
using MediaBrowser.Common.Plugins;
#if __EMBY__
using MediaBrowser.Common;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Drawing;

#else
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Common.Configuration;
#endif

namespace Jellyfin.Plugin.MetaTube;

/// <summary>
/// MetaTube 插件主类
/// 提供插件的核心功能和配置管理
/// 支持 Jellyfin 和 Emby 双平台
/// </summary>
#if __EMBY__
public class Plugin : BasePluginSimpleUI<PluginConfiguration>, IHasThumbImage
{
    public Plugin(IApplicationHost applicationHost)
        : base(applicationHost)
    {
        Instance = this;
    }
#else
public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
{
    public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
        : base(applicationPaths, xmlSerializer)
    {
        Instance = this;
    }
#endif

    /// <summary>
    /// 提供商名称
    /// 用于在 Jellyfin/Emby 中标识此插件
    /// </summary>
    public const string ProviderName = "MetaTube";

    /// <summary>
    /// 提供商 ID
    /// 用于在元数据中标识此提供商
    /// </summary>
    public const string ProviderId = "MetaTube";

    public override string Name => ProviderName;

    public override string Description => "MetaTube Plugin for Jellyfin/Emby";

    /// <summary>
    /// 插件唯一标识符
    /// 固定 GUID，用于插件注册和识别
    /// </summary>
    public override Guid Id => Guid.Parse("01cc53ec-c415-4108-bbd4-a684a9801a32");

    /// <summary>
    /// 插件单例实例
    /// 提供全局访问点
    /// </summary>
    public static Plugin Instance { get; private set; }

#if !__EMBY__
    /// <summary>
    /// 获取插件配置页面
    /// 仅 Jellyfin 平台使用
    /// </summary>
    /// <returns>插件页面信息数组</returns>
    public IEnumerable<PluginPageInfo> GetPages()
    {
        return new[]
        {
            new PluginPageInfo
            {
                Name = Name,
                EmbeddedResourcePath = $"{GetType().Namespace}.Configuration.configPage.html",
            },
        };
    }
#endif

#if __EMBY__
    /// <summary>
    /// 获取插件配置
    /// 仅 Emby 平台使用
    /// </summary>
    public PluginConfiguration Configuration => GetOptions();

    /// <summary>
    /// 获取插件缩略图
    /// 仅 Emby 平台使用
    /// </summary>
    /// <returns>缩略图资源流</returns>
    public Stream GetThumbImage()
    {
        return GetType().Assembly.GetManifestResourceStream($"{GetType().Namespace}.thumb.png");
    }

    /// <summary>
    /// 缩略图图片格式
    /// 仅 Emby 平台使用
    /// </summary>
    public ImageFormat ThumbImageFormat => ImageFormat.Png;
#endif
}
