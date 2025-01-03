﻿using Si.Framework.EntityFramework.Abstraction;

namespace Si.Framework.Rbac.Entity
{
    public class Role: BaseEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
