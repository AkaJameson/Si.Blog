namespace Si.Framework.ToolKit.Extension
{
    public static class TypeExtension
    {
        public static bool IsBaseOn(this Type type, Type baseType)
        {
            if (type == null || baseType == null)
            {
                throw new ArgumentNullException("Type parameters cannot be null");
            }

            // 检查 baseType 是否是接口
            if (baseType.IsInterface)
            {
                return baseType.IsAssignableFrom(type);
            }

            // 检查 type 是否是 baseType 的子类
            return type.IsSubclassOf(baseType) || type == baseType;
        }
    }
}
