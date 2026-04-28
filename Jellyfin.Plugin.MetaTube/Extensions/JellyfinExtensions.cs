#if !__EMBY__
#pragma warning disable CA2254

using MediaBrowser.Controller.Entities.Movies;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.MetaTube.Extensions;

/// <summary>
/// Jellyfin 平台扩展方法类
/// 提供 Jellyfin 特定的扩展方法（仅 Jellyfin 平台使用）
/// </summary>
public static class JellyfinExtensions
{
    #region Logger

    /// <summary>
    /// 记录调试级别日志
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public static void Debug(this ILogger logger, string message, params object[] args)
    {
        logger.LogDebug(message, args);
    }

    /// <summary>
    /// 记录信息级别日志
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public static void Info(this ILogger logger, string message, params object[] args)
    {
        logger.LogInformation(message, args);
    }

    /// <summary>
    /// 记录警告级别日志
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public static void Warn(this ILogger logger, string message, params object[] args)
    {
        logger.LogWarning(message, args);
    }

    /// <summary>
    /// 记录错误级别日志
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public static void Error(this ILogger logger, string message, params object[] args)
    {
        logger.LogError(message, args);
    }

    #endregion

    #region Movie

    /// <summary>
    /// 为电影添加合集信息
    /// 设置电影的合集名称
    /// </summary>
    /// <param name="movie">电影对象</param>
    /// <param name="name">合集名称</param>
    public static void AddCollection(this Movie movie, string name)
    {
        movie.CollectionName = name;
    }

    #endregion
}

#pragma warning restore CA2254
#endif