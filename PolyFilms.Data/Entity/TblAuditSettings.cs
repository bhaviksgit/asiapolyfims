namespace PolyFilms.Data.Entity
{
    public partial class TblAuditSettings
    {
        public short SettingId { get; set; }
        public string TableName { get; set; }
        public string ScreenName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
