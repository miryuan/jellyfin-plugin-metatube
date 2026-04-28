using System.Web;
using Jellyfin.Plugin.MetaTube.Helpers;
using MediaBrowser.Model.Entities;

namespace Jellyfin.Plugin.MetaTube.Extensions;

/// <summary>
/// ProviderIds 扩展方法类
/// 提供提供商 ID 相关的扩展方法
/// </summary>
public static class ProviderIdsExtensions
{
    /// <summary>
    /// 获取提供商 ID 对象
    /// 从实例中解析指定名称的提供商 ID
    /// </summary>
    /// <param name="instance">具有提供商 ID 的实例</param>
    /// <param name="name">提供商名称</param>
    /// <returns>解析后的 ProviderId 对象</returns>
    public static ProviderId GetPid(this IHasProviderIds instance, string name)
    {
        return ProviderId.Parse(instance.GetProviderId(name));
    }

    /// <summary>
    /// 设置提供商 ID
    /// 将提供商信息编码后存储到实例中
    /// </summary>
    /// <param name="instance">具有提供商 ID 的实例</param>
    /// <param name="name">提供商名称</param>
    /// <param name="provider">提供商标识</param>
    /// <param name="id">ID 值（会被 URL 编码）</param>
    /// <param name="position">可选的位置信息</param>
    /// <param name="update">是否更新的标志</param>
    public static void SetPid(this IHasProviderIds instance, string name, string provider, string id,
        double? position = null, bool? update = null)
    {
        var pid = new ProviderId
        {
            Provider = provider,
            Id = Uri.EscapeDataString(id),
            Position = position,
            Update = update
        };
        instance.SetProviderId(name, pid.ToString());
    }

    /// <summary>
    /// 获取预告片 URL
    /// 从提供商 ID 中解码并返回预告片 URL
    /// </summary>
    /// <param name="instance">具有提供商 ID 的实例</param>
    /// <returns>预告片 URL，如果没有则返回空字符串</returns>
    public static string GetTrailerUrl(this IHasProviderIds instance)
    {
        return !instance.ProviderIds.Any()
            ? string.Empty
            : HttpUtility.UrlDecode(instance.GetProviderId("TrailerUrl"));
    }

    /// <summary>
    /// 设置预告片 URL
    /// 将 URL 编码后存储到提供商 ID 中
    /// </summary>
    /// <param name="instance">具有提供商 ID 的实例</param>
    /// <param name="url">预告片 URL（会被 URL 编码）</param>
    public static void SetTrailerUrl(this IHasProviderIds instance, string url)
    {
        instance.SetProviderId("TrailerUrl", HttpUtility.UrlEncode(url));
    }
}