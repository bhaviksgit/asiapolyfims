namespace PolyFilms.Data.CustomModel
{
    public class UserRightsModel
    {
        public int RoleMenuPk { get; set; }

        public int RoleId { get; set; }

        public int MenuId { get; set; }

        public bool IsView { get; set; }

        public bool IsInsert { get; set; }

        public bool IsEdit { get; set; }

        public bool IsDelete { get; set; }

        public bool IsChangeStatus { get; set; }

        public string MenuName { get; set; }
    }
}
