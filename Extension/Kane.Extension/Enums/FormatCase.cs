// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Kane.Extension;

/// <summary>
/// 字符串格式化方式
/// </summary>
public enum FormatCase
{
    /// <summary>
    /// 忽略处理
    /// </summary>
    [Description("忽略处理")]
    Ingore = 0,
    /// <summary>
    /// 全小写命名，如 lowercase
    /// </summary>
    [Description("全小写命名")]
    LowerCase = 1,
    /// <summary>
    /// 全大写命名，如 UPPERCASE
    /// </summary>
    [Description("全大写命名")]
    UpperCase = 2,
    /// <summary>
    /// 帕斯卡命名，如 PascalCase
    /// </summary>
    [Description("帕斯卡命名")]
    PascalCase = 3,
    /// <summary>
    /// 骆驼命名，如 camelCase
    /// </summary>
    [Description("骆驼命名")]
    CamelCase = 4,
    /// <summary>
    /// 蛇形命名，如 snake_case
    /// </summary>
    [Description("蛇形命名")]
    SnakeCase = 5,
    /// <summary>
    /// 大写蛇形命名，如 SNAKE_CASE
    /// </summary>
    [Description("大写蛇形命名")]
    UpperSnakeCase = 6,
    /// <summary>
    /// 烤串命名，如 kebab-case
    /// </summary>
    [Description("烤串命名")]
    KebabCase = 7,
    /// <summary>
    /// 大写烤串命名，如 KEBAB-CASE
    /// </summary>
    [Description("大写烤串命名")]
    UpperKebabCase = 8,
}