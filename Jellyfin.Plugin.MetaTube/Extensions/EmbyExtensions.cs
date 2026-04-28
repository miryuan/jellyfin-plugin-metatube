#if __EMBY__

using System.Text;
using MediaBrowser.Model.Logging;

namespace Jellyfin.Plugin.MetaTube.Extensions;

/// <summary>
/// Emby 平台扩展方法类
/// 提供 Emby 特定的扩展方法（仅 Emby 平台使用）
/// </summary>
public static class EmbyExtensions
{
    #region LogManager

    /// <summary>
    /// 创建指定类型的日志记录器
    /// 日志记录器名称格式：MetaTube.{TypeName}
    /// </summary>
    /// <typeparam name="T">要为其创建日志记录器的类型</typeparam>
    /// <param name="logManager">日志管理器</param>
    /// <returns>配置好的日志记录器</returns>
    public static ILogger CreateLogger<T>(this ILogManager logManager)
    {
        return logManager.GetLogger($"{Plugin.ProviderName}.{typeof(T).Name}");
    }

    #endregion

    #region Sorting

    /// <summary>
    /// 按字符串进行升序排序（使用字母数字比较器）
    /// 支持自然排序，如：file1, file2, file10 而不是 file1, file10, file2
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="list">要排序的集合</param>
    /// <param name="getName">获取名称的函数</param>
    /// <returns>排序后的集合</returns>
    public static IEnumerable<T> OrderByString<T>(this IEnumerable<T> list, Func<T, string> getName)
    {
        return list.OrderBy(getName, new AlphanumComparator());
    }

    /// <summary>
    /// 按字符串进行降序排序（使用字母数字比较器）
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="list">要排序的集合</param>
    /// <param name="getName">获取名称的函数</param>
    /// <returns>降序排序后的集合</returns>
    public static IEnumerable<T> OrderByStringDescending<T>(
        this IEnumerable<T> list,
        Func<T, string> getName)
    {
        return list.OrderByDescending(getName, new AlphanumComparator());
    }

    /// <summary>
    /// 在已排序的集合上按字符串进行次要升序排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="list">已排序的集合</param>
    /// <param name="getName">获取名称的函数</param>
    /// <returns>进一步排序后的集合</returns>
    public static IOrderedEnumerable<T> ThenByString<T>(
        this IOrderedEnumerable<T> list,
        Func<T, string> getName)
    {
        return list.ThenBy(getName, new AlphanumComparator());
    }

    /// <summary>
    /// 在已排序的集合上按字符串进行次要降序排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="list">已排序的集合</param>
    /// <param name="getName">获取名称的函数</param>
    /// <returns>进一步降序排序后的集合</returns>
    public static IOrderedEnumerable<T> ThenByStringDescending<T>(
        this IOrderedEnumerable<T> list,
        Func<T, string> getName)
    {
        return list.ThenByDescending(getName, new AlphanumComparator());
    }

    /// <summary>
    /// 字母数字比较器
    /// 实现自然排序算法，正确处理数字序列
    /// </summary>
    private sealed class AlphanumComparator : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return CompareValues(x, y);
        }

        /// <summary>
        /// 检查字符是否属于同一块（数字块或字母块）
        /// </summary>
        private static bool InChunk(char ch, char otherCh)
        {
            var chunkType = ChunkType.Alphanumeric;
            if (char.IsDigit(otherCh))
                chunkType = ChunkType.Numeric;
            return (chunkType != ChunkType.Alphanumeric || !char.IsDigit(ch)) &&
                   (chunkType != ChunkType.Numeric || char.IsDigit(ch));
        }

        /// <summary>
        /// 比较两个字符串的值
        /// 实现字母数字混合排序逻辑
        /// </summary>
        private static int CompareValues(string s1, string s2)
        {
            if (s1 == null || s2 == null)
                return 0;
            var index1 = 0;
            var index2 = 0;
            while (index1 < s1.Length || index2 < s2.Length)
            {
                if (index1 >= s1.Length)
                    return -1;
                if (index2 >= s2.Length)
                    return 1;
                var ch1 = s1[index1];
                var ch2 = s2[index2];
                var stringBuilder1 = new StringBuilder();
                var stringBuilder2 = new StringBuilder();
                
                // 提取第一个字符串的块
                while (index1 < s1.Length && (stringBuilder1.Length == 0 || InChunk(ch1, stringBuilder1[0])))
                {
                    stringBuilder1.Append(ch1);
                    ++index1;
                    if (index1 < s1.Length)
                        ch1 = s1[index1];
                }

                // 提取第二个字符串的块
                while (index2 < s2.Length && (stringBuilder2.Length == 0 || InChunk(ch2, stringBuilder2[0])))
                {
                    stringBuilder2.Append(ch2);
                    ++index2;
                    if (index2 < s2.Length)
                        ch2 = s2[index2];
                }

                var num = 0;
                // 如果两个块都是数字，则按数值比较
                if (char.IsDigit(stringBuilder1[0]) && char.IsDigit(stringBuilder2[0]))
                {
                    if (!int.TryParse(stringBuilder1.ToString(), out var result1) ||
                        !int.TryParse(stringBuilder2.ToString(), out var result2))
                        return 0;
                    if (result1 < result2)
                        num = -1;
                    if (result1 > result2)
                        num = 1;
                }
                else
                {
                    // 否则按字符串比较
                    num = string.Compare(stringBuilder1.ToString(), stringBuilder2.ToString(),
                        StringComparison.CurrentCulture);
                }

                if (num != 0)
                    return num;
            }

            return 0;
        }

        /// <summary>
        /// 块类型枚举
        /// </summary>
        private enum ChunkType
        {
            Alphanumeric,
            Numeric
        }
    }

    #endregion
}

#endif