using System.Text;

namespace Jellyfin.Plugin.MetaTube.Helpers;

/// <summary>
/// 替换表类
/// 继承自 Dictionary，用于存储和管理文本替换规则
/// 支持字符串和字符串集合的替换操作
/// </summary>
public class SubstitutionTable : Dictionary<string, string>
{
    /// <summary>
    /// 私有构造函数
    /// 初始化为不区分大小写的字典
    /// </summary>
    private SubstitutionTable()
        : base(StringComparer.OrdinalIgnoreCase) { }

    /// <summary>
    /// 解析文本内容生成替换表
    /// 文本格式：每行一条记录，格式为 "源文本=目标文本"
    /// 如果目标文本为空，则表示删除源文本
    /// </summary>
    /// <param name="text">要解析的文本内容</param>
    /// <returns>生成的替换表对象</returns>
    public static SubstitutionTable Parse(string text)
    {
        var dictionary = new SubstitutionTable();

        var reader = new StringReader(text ?? string.Empty);
        while (reader.ReadLine() is { } line)
        {
            var kvp = line.Split('=', 2).Select(s => s.Trim()).ToList();
            if (string.IsNullOrWhiteSpace(kvp.First()))
                continue;
            dictionary[kvp[0]] = kvp.Count switch
            {
                1 => null,
                2 => kvp[1],
                _ => dictionary[kvp[0]],
            };
        }

        return dictionary;
    }

    /// <summary>
    /// 将替换表转换为字符串格式
    /// </summary>
    /// <returns>格式化的替换表字符串，每行一条记录</returns>
    public override string ToString()
    {
        var table = this;
        return table.Any() != true
            ? string.Empty
            : string.Join(
                '\n',
                table
                    .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
                    .Select(kvp => $"{kvp.Key?.Trim()}={kvp.Value?.Trim()}")
            );
    }

    /// <summary>
    /// 对字符串执行替换操作
    /// 使用 StringBuilder 进行高效的字符串替换
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <returns>替换后的字符串</returns>
    public string Substitute(string source)
    {
        var table = this;

        return table.Any() != true
            ? source
            : table
                .Aggregate(new StringBuilder(source), (sb, kvp) => sb.Replace(kvp.Key, kvp.Value))
                .ToString();
    }

    /// <summary>
    /// 对字符串集合执行替换操作
    /// 如果源字符串在替换表中存在，则替换为目标字符串
    /// 如果目标字符串为空，则从结果中移除该项
    /// </summary>
    /// <param name="source">源字符串集合</param>
    /// <returns>替换后的字符串集合</returns>
    public IEnumerable<string> Substitute(IEnumerable<string> source)
    {
        var table = this;

        if (table.Any() != true)
            return source;

        var target = new List<string>();

        foreach (var item in source ?? Enumerable.Empty<string>())
        {
            if (!table.TryGetValue(item, out var value))
                target.Add(item);
            else if (!string.IsNullOrWhiteSpace(value))
                target.Add(value);
        }

        return target;
    }
}
