using Jellyfin.Plugin.MetaTube.Helpers;
using Jellyfin.Plugin.MetaTube.Translation;
#if __EMBY__
using System.ComponentModel;
using Emby.Web.GenericEdit;
using MediaBrowser.Model.Attributes;

#else
using MediaBrowser.Model.Plugins;
#endif

namespace Jellyfin.Plugin.MetaTube.Configuration;

/// <summary>
/// MetaTube 插件配置类
/// 支持 Jellyfin 和 Emby 双平台的配置管理
/// </summary>
#if __EMBY__
public class PluginConfiguration : EditableOptionsBase
{
    public override string EditorTitle => Plugin.ProviderName;
#else
public class PluginConfiguration : BasePluginConfiguration
{
#endif

#if __EMBY__
    [DisplayName("Server")]
    [Description("Full url of the MetaTube Server, HTTPS protocol is recommended.")]
    [Required]
#endif
    /// <summary>
    /// MetaTube 服务器地址
    /// 完整的 URL，建议使用 HTTPS 协议
    /// </summary>
    public string Server { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Token")]
    [Description(
        "Access token for the MetaTube Server, or blank if no token is set by the backend."
    )]
#endif
    /// <summary>
    /// 访问令牌
    /// MetaTube 服务器的访问令牌，如果后端未设置则留空
    /// </summary>
    public string Token { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Enable auto update")]
    [Description("Automatically update the plugin through scheduled tasks.")]
    /// <summary>
    /// 启用自动更新（仅 Emby）
    /// 通过计划任务自动更新插件
    /// </summary>
    public bool EnableAutoUpdate { get; set; } = true;
#endif

#if __EMBY__
    [DisplayName("Enable collections")]
    [Description("Automatically create collections by series.")]
#endif
    /// <summary>
    /// 启用合集功能
    /// 按系列自动创建合集
    /// </summary>
    public bool EnableCollections { get; set; } = false;

#if __EMBY__
    [DisplayName("Enable directors")]
    [Description("Add directors to corresponding video metadata.")]
#endif
    /// <summary>
    /// 启用导演信息
    /// 将导演添加到相应的视频元数据中
    /// </summary>
    public bool EnableDirectors { get; set; } = true;

#if __EMBY__
    [DisplayName("Enable ratings")]
    [Description("Display community ratings from the original website.")]
#endif
    /// <summary>
    /// 启用评分显示
    /// 显示来自原始网站的社区评分
    /// </summary>
    public bool EnableRatings { get; set; } = true;

#if __EMBY__
    [DisplayName("Enable trailers")]
    [Description("Generate online video trailers in strm format.")]
#endif
    /// <summary>
    /// 启用预告片功能
    /// 生成 STRM 格式的在线视频预告片
    /// </summary>
    public bool EnableTrailers { get; set; } = false;

#if __EMBY__
    [DisplayName("Enable real actor names")]
    [Description("Search and replace with real actor names from AVBASE.")]
#endif
    /// <summary>
    /// 启用真实演员名称
    /// 从 AVBASE 搜索并替换为真实演员名称
    /// </summary>
    public bool EnableRealActorNames { get; set; } = false;

#if __EMBY__
    [DisplayName("Enable badges")]
    [Description("Add Chinese subtitle badges to primary images.")]
#endif
    /// <summary>
    /// 启用徽章功能
    /// 在主图片上添加中文字幕徽章
    /// </summary>
    public bool EnableBadges { get; set; } = false;

#if __EMBY__
    [DisplayName("Badge url")]
    [Description("Custom badge url, PNG format is recommended. (default: zimu.png)")]
#endif
    /// <summary>
    /// 徽章图片 URL
    /// 自定义徽章 URL，建议使用 PNG 格式（默认：zimu.png）
    /// </summary>
    public string BadgeUrl { get; set; } = "zimu.png";

#if __EMBY__
    [DisplayName("Primary image ratio")]
    [Description("Aspect ratio for primary images, set a negative value to use the default.")]
#endif
    /// <summary>
    /// 主图片纵横比
    /// 设置为负值则使用默认比例
    /// </summary>
    public double PrimaryImageRatio { get; set; } = -1;

#if __EMBY__
    [DisplayName("Default image quality")]
    [Description(
        "Default compression quality for JPEG images, set between 0 and 100. (default: 90)"
    )]
    [MinValue(0)]
    [MaxValue(100)]
    [Required]
#endif
    /// <summary>
    /// 默认图片质量
    /// JPEG 图片的默认压缩质量，范围 0-100（默认：90）
    /// </summary>
    public int DefaultImageQuality { get; set; } = 90;

#if __EMBY__
    [DisplayName("Enable movie provider filter")]
    [Description("Filter and reorder search results from movie providers.")]
#endif
    /// <summary>
    /// 启用影片提供商过滤器
    /// 过滤并重新排序来自影片提供商的搜索结果
    /// </summary>
    public bool EnableMovieProviderFilter { get; set; } = false;

#if __EMBY__
    [DisplayName("Movie provider filter")]
    [Description(
        "Provider names are case-insensitive, with decreasing precedence from left to right, separated by commas."
    )]
#endif
    /// <summary>
    /// 影片提供商过滤器
    /// 提供商名称不区分大小写，优先级从左到右递减，用逗号分隔
    /// </summary>
    public string RawMovieProviderFilter
    {
        get =>
            _movieProviderFilter?.Any() == true
                ? string.Join(',', _movieProviderFilter)
                : string.Empty;
        set =>
            _movieProviderFilter = value
                ?.Split(',')
                .Select(s => s.Trim())
                .Where(s => s.Any())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
    }

    /// <summary>
    /// 获取影片提供商过滤器列表
    /// </summary>
    /// <returns>提供商名称列表</returns>
    public List<string> GetMovieProviderFilter()
    {
        return _movieProviderFilter;
    }

    private List<string> _movieProviderFilter;

#if __EMBY__
    [DisplayName("Enable template")]
#endif
    /// <summary>
    /// 启用模板功能
    /// 使用模板自定义元数据格式
    /// </summary>
    public bool EnableTemplate { get; set; } = false;

#if __EMBY__
    [DisplayName("Name template")]
#endif
    /// <summary>
    /// 名称模板
    /// 用于格式化影片名称的模板字符串
    /// </summary>
    public string NameTemplate { get; set; } = DefaultNameTemplate;

#if __EMBY__
    [DisplayName("Tagline template")]
#endif
    /// <summary>
    /// 标语模板
    /// 用于格式化影片标语的模板字符串
    /// </summary>
    public string TaglineTemplate { get; set; } = DefaultTaglineTemplate;

    /// <summary>
    /// 默认名称模板：{number} {title}
    /// </summary>
    public static string DefaultNameTemplate => "{number} {title}";

    /// <summary>
    /// 默认标语模板：配信開始日 {date}
    /// </summary>
    public static string DefaultTaglineTemplate => "配信開始日 {date}";

#if __EMBY__
    [DisplayName("Translation mode")]
#endif
    /// <summary>
    /// 翻译模式
    /// 指定需要翻译的元数据内容
    /// </summary>
    public TranslationMode TranslationMode { get; set; } = TranslationMode.Disabled;

#if __EMBY__
    [DisplayName("Translation engine")]
#endif
    /// <summary>
    /// 翻译引擎
    /// 选择用于翻译的服务提供商
    /// </summary>
    public TranslationEngine TranslationEngine { get; set; } = TranslationEngine.Baidu;

#if __EMBY__
    [DisplayName("Baidu app id")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Baidu)]
#endif
    /// <summary>
    /// 百度翻译应用 ID
    /// 使用百度翻译服务时需要提供
    /// </summary>
    public string BaiduAppId { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Baidu app key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Baidu)]
#endif
    /// <summary>
    /// 百度翻译应用密钥
    /// 使用百度翻译服务时需要提供
    /// </summary>
    public string BaiduAppKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Google api key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Google)]
#endif
    /// <summary>
    /// Google 翻译 API 密钥
    /// 使用 Google 翻译服务时需要提供
    /// </summary>
    public string GoogleApiKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Google api url")]
    [Description("Custom Google translate api url. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.Google)]
#endif
    /// <summary>
    /// Google 翻译 API URL
    /// 自定义 Google 翻译 API 地址（可选）
    /// </summary>
    public string GoogleApiUrl { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("DeepL api key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.DeepL)]
#endif
    /// <summary>
    /// DeepL API 密钥
    /// 使用 DeepL 翻译服务时需要提供
    /// </summary>
    public string DeepLApiKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("DeepL api url")]
    [Description("Custom DeepL-compatible api url. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.DeepL)]
#endif
    /// <summary>
    /// DeepL API URL
    /// 自定义 DeepL 兼容 API 地址（可选）
    /// </summary>
    public string DeepLApiUrl { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("OpenAI api key")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.OpenAi)]
#endif
    /// <summary>
    /// OpenAI API 密钥
    /// 使用 OpenAI 翻译服务时需要提供
    /// </summary>
    public string OpenAiApiKey { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("OpenAI api url")]
    [Description("Custom OpenAI-compatible api url. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.OpenAi)]
#endif
    /// <summary>
    /// OpenAI API URL
    /// 自定义 OpenAI 兼容 API 地址（可选）
    /// </summary>
    public string OpenAiApiUrl { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("OpenAI model")]
    [Description("Custom OpenAI-compatible api model. (optional)")]
    [VisibleCondition(nameof(TranslationEngine), ValueCondition.IsEqual, TranslationEngine.OpenAi)]
#endif
    /// <summary>
    /// OpenAI 模型名称
    /// 自定义 OpenAI 兼容模型（可选）
    /// </summary>
    public string OpenAiModel { get; set; } = string.Empty;

#if __EMBY__
    [DisplayName("Enable title substitution")]
#endif
    /// <summary>
    /// 启用标题替换功能
    /// 使用替换表自动替换标题中的特定文本
    /// </summary>
    public bool EnableTitleSubstitution { get; set; } = false;

#if __EMBY__
    [DisplayName("Title substitution table")]
    [Description(
        "One record per line, separated by equal signs. Leave the target substring blank to delete the source substring."
    )]
    [EditMultiline(5)]
#endif
    /// <summary>
    /// 标题替换表
    /// 每行一条记录，用等号分隔。目标子字符串留空则删除源子字符串
    /// </summary>
    public string TitleRawSubstitutionTable
    {
        get => _titleSubstitutionTable?.ToString();
        set => _titleSubstitutionTable = SubstitutionTable.Parse(value);
    }

    /// <summary>
    /// 获取标题替换表对象
    /// </summary>
    /// <returns>替换表实例</returns>
    public SubstitutionTable GetTitleSubstitutionTable()
    {
        return _titleSubstitutionTable;
    }

    private SubstitutionTable _titleSubstitutionTable;

#if __EMBY__
    [DisplayName("Enable actor substitution")]
#endif
    /// <summary>
    /// 启用演员替换功能
    /// 使用替换表自动替换演员名称
    /// </summary>
    public bool EnableActorSubstitution { get; set; } = false;

#if __EMBY__
    [DisplayName("Actor substitution table")]
    [Description(
        "One record per line, separated by equal signs. Leave the target actor blank to delete the source actor."
    )]
    [EditMultiline(5)]
#endif
    /// <summary>
    /// 演员替换表
    /// 每行一条记录，用等号分隔。目标演员留空则删除源演员
    /// </summary>
    public string ActorRawSubstitutionTable
    {
        get => _actorSubstitutionTable?.ToString();
        set => _actorSubstitutionTable = SubstitutionTable.Parse(value);
    }

    /// <summary>
    /// 获取演员替换表对象
    /// </summary>
    /// <returns>替换表实例</returns>
    public SubstitutionTable GetActorSubstitutionTable()
    {
        return _actorSubstitutionTable;
    }

    private SubstitutionTable _actorSubstitutionTable;

#if __EMBY__
    [DisplayName("Enable genre substitution")]
#endif
    /// <summary>
    /// 启用标签替换功能
    /// 使用替换表自动替换标签名称
    /// </summary>
    public bool EnableGenreSubstitution { get; set; } = false;

#if __EMBY__
    [DisplayName("Title substitution table")]
    [Description(
        "One record per line, separated by equal signs. Leave the target genre blank to delete the source genre."
    )]
    [EditMultiline(5)]
#endif
    /// <summary>
    /// 标签替换表
    /// 每行一条记录，用等号分隔。目标标签留空则删除源标签
    /// </summary>
    public string GenreRawSubstitutionTable
    {
        get => _genreSubstitutionTable?.ToString();
        set => _genreSubstitutionTable = SubstitutionTable.Parse(value);
    }

    /// <summary>
    /// 获取标签替换表对象
    /// </summary>
    /// <returns>替换表实例</returns>
    public SubstitutionTable GetGenreSubstitutionTable()
    {
        return _genreSubstitutionTable;
    }

    private SubstitutionTable _genreSubstitutionTable;
}
