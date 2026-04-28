using System.ComponentModel;

namespace Jellyfin.Plugin.MetaTube.Translation;

/// <summary>
/// 翻译引擎枚举
/// 定义支持的翻译服务提供商
/// </summary>
public enum TranslationEngine
{
    /// <summary>
    /// 百度翻译
    /// 使用百度翻译 API 服务
    /// </summary>
    [Description("Baidu")]
    Baidu,

    /// <summary>
    /// Google 翻译
    /// 使用 Google Cloud Translation API
    /// </summary>
    [Description("Google")]
    Google,

    /// <summary>
    /// Google 翻译（免费）
    /// 使用免费的 Google 翻译服务
    /// </summary>
    [Description("Google (Free)")]
    GoogleFree,

    /// <summary>
    /// DeepL 翻译
    /// 使用 DeepL 翻译 API 服务
    /// </summary>
    [Description("DeepL")]
    DeepL,

    /// <summary>
    /// OpenAI 翻译
    /// 使用 OpenAI GPT 模型进行翻译
    /// </summary>
    [Description("OpenAI")]
    OpenAi
}