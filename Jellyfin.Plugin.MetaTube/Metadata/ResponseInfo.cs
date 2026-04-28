using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// API 响应信息泛型类
/// 用于封装 MetaTube API 的标准响应格式
/// 支持成功和失败两种响应情况
/// </summary>
/// <typeparam name="T">响应数据的类型</typeparam>
public class ResponseInfo<T>
{
    /// <summary>
    /// 响应数据
    /// 请求成功时返回的数据对象
    /// </summary>
    [JsonPropertyName("data")]
    public T Data { get; set; }

    /// <summary>
    /// 错误信息
    /// 请求失败时返回的错误详情
    /// </summary>
    [JsonPropertyName("error")]
    public ErrorInfo Error { get; set; }
}
