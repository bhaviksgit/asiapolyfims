using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblMenu
    {
        public TblMenu()
        {
            TblRoleMenuMap = new HashSet<TblRoleMenuMap>();
        }

        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public string MenuName { get; set; }
        public string ImagePath { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

        public ICollection<TblRoleMenuMap> TblRoleMenuMap { get; set; }
    }
}
