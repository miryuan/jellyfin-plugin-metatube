using System.ComponentModel;

namespace Jellyfin.Plugin.MetaTube.Translation;

/// <summary>
/// 翻译模式枚举
/// 定义需要翻译的元数据内容类型
/// </summary>
public enum TranslationMode
{
    /// <summary>
    /// 已禁用
    /// 不进行任何翻译
    /// </summary>
    [Description("Disabled")]
    Disabled,

    /// <summary>
    /// 仅标题
    /// 只翻译影片标题
    /// </summary>
    [Description("Title")]
    Title,

    /// <summary>
    /// 仅简介
    /// 只翻译影片简介
    /// </summary>
    [Description("Summary")]
    Summary,

    /// <summary>
    /// 标题和简介
    /// 同时翻译标题和简介
    /// </summary>
    [Description("Title and Summary")]
    Both
}