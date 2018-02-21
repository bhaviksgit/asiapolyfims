namespace PolyFilms.Data.CustomModel
{
    public class UserAccessPermission
    {
        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public string MenuName { get; set; }
        public string ImagePath { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        //public bool? IsActive { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsChangeStatus { get; set; }
    }
}
