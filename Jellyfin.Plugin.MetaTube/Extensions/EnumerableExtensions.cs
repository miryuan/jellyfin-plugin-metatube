namespace Jellyfin.Plugin.MetaTube.Extensions;

/// <summary>
/// Enumerable 扩展方法类
/// 提供可枚举集合相关的扩展方法
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// 为集合中的每个元素添加索引
    /// 将集合转换为包含索引和元素的元组序列
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">源集合</param>
    /// <returns>包含索引和元素的元组序列</returns>
    public static IEnumerable<(int index, T item)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (index, item));
    }
}