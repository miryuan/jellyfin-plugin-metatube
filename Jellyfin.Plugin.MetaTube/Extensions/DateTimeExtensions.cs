namespace Jellyfin.Plugin.MetaTube.Extensions;

/// <summary>
/// DateTime 扩展方法类
/// 提供日期时间相关的扩展方法
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// 获取有效的 DateTime 对象
    /// 如果年份大于 1 则返回该日期，否则返回 null
    /// </summary>
    /// <param name="dateTime">要验证的日期时间</param>
    /// <returns>有效的日期时间或 null</returns>
    public static DateTime? GetValidDateTime(this DateTime dateTime)
    {
        return dateTime.Year > 1 ? dateTime : null;
    }

    /// <summary>
    /// 获取有效的年份
    /// 如果日期有效则返回年份，否则返回 null
    /// </summary>
    /// <param name="dateTime">要提取年份的日期时间</param>
    /// <returns>有效年份或 null</returns>
    public static int? GetValidYear(this DateTime dateTime)
    {
        return dateTime.GetValidDateTime()?.Year;
    }
}