﻿using Si.Framework.EntityFramework.Abstraction;

namespace Si.Framework.Rbac.Entity
{
    public class RolePermission: BaseEntity
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
