#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：多个类名
* 类 描 述 ：Json扩展类，使用【System.Text.Json】
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Json
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2021/01/30 15:28:37
* 更新时间 ：2021/01/30 15:28:37
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if NETCOREAPP3_1_OR_GREATER
using System.Text.Json;

namespace Kane.Extension.Json
{
    #region 自定义名称策略，属性名全为【小写】 + LowerCaseNamingPolicy : JsonNamingPolicy
    /// <summary>
    /// 自定义名称策略，属性名全为【小写】
    /// </summary>
    public class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// 重写转换器
        /// </summary>
        /// <param name="name">原名称</param>
        /// <returns></returns>
        public override string ConvertName(string name) => name.ToLower();
    }
    #endregion
}
#endif