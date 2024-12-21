namespace Si.Framework.Base.Entity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DependencyAttribute : Attribute
    {
        public Type[] DependencyModule { get; set; }

        public DependencyAttribute(params Type[] DependencyModules)
        {
            DependencyModule = DependencyModules;
        }
    }
}
