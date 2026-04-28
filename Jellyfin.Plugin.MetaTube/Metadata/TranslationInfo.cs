using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// 翻译信息类
/// 用于存储文本翻译的结果信息
/// 包含源语言、目标语言和翻译后的文本
/// </summary>
public class TranslationInfo
{
    /// <summary>
    /// 源语言
    /// 原始文本的语言代码（如：ja, en, zh）
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; set; }

    /// <summary>
    /// 目标语言
    /// 翻译后的语言代码（如：zh, en, ja）
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; set; }

    /// <summary>
    /// 翻译后的文本
    /// 翻译引擎输出的翻译结果
    /// </summary>
    [JsonPropertyName("translated_text")]
    public string TranslatedText { get; set; }
}
