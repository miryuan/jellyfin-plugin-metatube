namespace Jellyfin.Plugin.MetaTube.Helpers;

/// <summary>
/// 提供商 ID 类
/// 用于存储和解析提供商标识信息
/// 格式：Provider:Id:Position:Update
/// </summary>
public class ProviderId
{
    /// <summary>
    /// 提供商名称
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// 提供商中的唯一标识 ID
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 位置信息（可选）
    /// 用于排序或定位
    /// </summary>
    public double? Position { get; set; }

    /// <summary>
    /// 更新标志（可选）
    /// 指示是否需要更新
    /// </summary>
    public bool? Update { get; set; }

    /// <summary>
    /// 解析原始提供商 ID 字符串
    /// 字符串格式：Provider:Id:Position:Update
    /// </summary>
    /// <param name="rawPid">原始提供商 ID 字符串</param>
    /// <returns>解析后的 ProviderId 对象</returns>
    public static ProviderId Parse(string rawPid)
    {
        var values = rawPid?.Split(':');
        return new ProviderId
        {
            Provider = values?.Length > 0 ? values[0] : string.Empty,
            Id = values?.Length > 1 ? Uri.UnescapeDataString(values[1]) : string.Empty,
            Position = values?.Length > 2 ? ToDouble(values[2]) : null,
            Update = values?.Length > 3 ? ToBool(values[3]) : null,
        };
    }

    /// <summary>
    /// 将提供商 ID 转换为字符串格式
    /// </summary>
    /// <returns>格式化的提供商 ID 字符串</returns>
    public override string ToString()
    {
        var pid = this;
        var values = new List<string> { pid.Provider, pid.Id };
        if (pid.Position.HasValue)
            values.Add(pid.Position.ToString());
        if (pid.Update.HasValue)
            values.Add((values.Count == 2 ? ":" : string.Empty) + pid.Update);
        return string.Join(':', values);
    }

    /// <summary>
    /// 将字符串转换为布尔值
    /// 支持多种格式：1/0, t/f, true/false（不区分大小写）
    /// </summary>
    /// <param name="s">要转换的字符串</param>
    /// <returns>转换后的布尔值，如果无法转换则返回 null</returns>
    private static bool? ToBool(string s)
    {
        switch (s)
        {
            case "1":
            case "t":
            case "T":
            case "true":
            case "True":
            case "TRUE":
                return true;
            case "0":
            case "f":
            case "F":
            case "false":
            case "False":
            case "FALSE":
                return false;
        }

        return null;
    }

    /// <summary>
    /// 将字符串转换为双精度浮点数
    /// </summary>
    /// <param name="s">要转换的字符串</param>
    /// <returns>转换后的数值，如果无法转换则返回 null</returns>
    private static double? ToDouble(string s)
    {
        return double.TryParse(s, out var result) ? result : null;
    }
}
