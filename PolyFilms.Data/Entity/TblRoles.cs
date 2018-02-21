using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblRoles
    {
        public TblRoles()
        {
            TblRoleMenuMap = new HashSet<TblRoleMenuMap>();
            TblUser = new HashSet<TblUser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TblRoleMenuMap> TblRoleMenuMap { get; set; }
        public ICollection<TblUser> TblUser { get; set; }
    }
}
