// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

namespace Kane.Extension
{
    /// <summary>
    /// 枚举类转成List集合实体
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}