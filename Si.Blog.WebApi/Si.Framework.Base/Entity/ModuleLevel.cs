namespace Si.Framework.Base.Entity
{
    public enum ModuleLevel
    {
        /// <summary>
        /// 核心模块，优先加载
        /// </summary>
        Core = 0,
        /// <summary>
        /// 应用级模块
        /// </summary>
        Application = 2,
        /// <summary>
        /// 自定义模块
        /// </summary>
        Custom = 3
    }
}
