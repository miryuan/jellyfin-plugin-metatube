namespace Jellyfin.Plugin.MetaTube.Helpers;

/// <summary>
/// Levenshtein 距离计算工具类
/// 提供计算两个字符串之间编辑距离的方法
/// 编辑距离是指将一个字符串转换为另一个字符串所需的最少编辑操作次数
/// </summary>
public static class Levenshtein
{
    /// <summary>
    /// 计算两个字符串之间的 Levenshtein 距离
    /// 使用动态规划算法，时间复杂度 O(m*n)，空间复杂度 O(n)
    /// 其中 m 和 n 分别为两个字符串的长度
    /// </summary>
    /// <param name="value1">第一个字符串</param>
    /// <param name="value2">第二个字符串</param>
    /// <returns>两个字符串之间的编辑距离，值越小表示越相似</returns>
    public static int Distance(string value1, string value2)
    {
        // 如果第二个字符串为空，距离为第一个字符串的长度
        if (value2.Length == 0)
        {
            return value1.Length;
        }

        // 使用一维数组优化空间复杂度
        int[] costs = new int[value2.Length];

        // 初始化第一行的成本值
        for (int i = 0; i < costs.Length; )
        {
            costs[i] = ++i;
        }

        // 遍历第一个字符串的每个字符
        for (int i = 0; i < value1.Length; i++)
        {
            int cost = i;
            int previousCost = i;

            char value1Char = value1[i];

            // 遍历第二个字符串的每个字符
            for (int j = 0; j < value2.Length; j++)
            {
                int currentCost = cost;

                cost = costs[j];

                // 如果字符不匹配，需要计算最小成本
                if (value1Char != value2[j])
                {
                    // 取插入、删除、替换操作中的最小成本
                    if (previousCost < currentCost)
                    {
                        currentCost = previousCost;
                    }

                    if (cost < currentCost)
                    {
                        currentCost = cost;
                    }

                    ++currentCost;
                }

                costs[j] = currentCost;
                previousCost = currentCost;
            }
        }

        // 返回最后一个成本值，即最终的编辑距离
        return costs[costs.Length - 1];
    }
}
