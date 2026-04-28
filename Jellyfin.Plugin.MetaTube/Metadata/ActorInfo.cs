using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MetaTube.Metadata;

/// <summary>
/// 演员详细信息类
/// 继承自 ActorSearchResult，包含演员的完整详细信息
/// 用于存储演员的详细元数据
/// </summary>
public class ActorInfo : ActorSearchResult
{
    /// <summary>
    /// 演员别名数组
    /// 演员的其他名称或艺名
    /// </summary>
    [JsonPropertyName("aliases")]
    public string[] Aliases { get; set; }

    /// <summary>
    /// 生日
    /// 演员的出生日期
    /// </summary>
    [JsonPropertyName("birthday")]
    public DateTime Birthday { get; set; }

    /// <summary>
    /// 血型
    /// 演员的血型信息
    /// </summary>
    [JsonPropertyName("blood_type")]
    public string BloodType { get; set; }

    /// <summary>
    /// 罩杯尺寸
    /// 演员的罩杯信息
    /// </summary>
    [JsonPropertyName("cup_size")]
    public string CupSize { get; set; }

    /// <summary>
    /// 出道日期
    /// 演员的出道时间
    /// </summary>
    [JsonPropertyName("debut_date")]
    public DateTime DebutDate { get; set; }

    /// <summary>
    /// 身高（厘米）
    /// 演员的身高数值
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// 爱好
    /// 演员的兴趣爱好
    /// </summary>
    [JsonPropertyName("hobby")]
    public string Hobby { get; set; }

    /// <summary>
    /// 特技
    /// 演员的特殊技能
    /// </summary>
    [JsonPropertyName("skill")]
    public string Skill { get; set; }

    /// <summary>
    /// 三围
    /// 演员的三围数据（胸围-腰围-臀围）
    /// </summary>
    [JsonPropertyName("measurements")]
    public string Measurements { get; set; }

    /// <summary>
    /// 国籍
    /// 演员的国籍信息
    /// </summary>
    [JsonPropertyName("nationality")]
    public string Nationality { get; set; }

    /// <summary>
    /// 简介
    /// 演员的详细描述和介绍
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }
}
