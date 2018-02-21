namespace PolyFilms.Data.Entity
{
    public partial class TblSetting
    {
        public short SettingId { get; set; }
        public string DisplayName { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public string Datatype { get; set; }
    }
}
