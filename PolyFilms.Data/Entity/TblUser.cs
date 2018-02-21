using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblJumbo = new HashSet<TblJumbo>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiryDateTime { get; set; }
        public bool IsSuperAdmin { get; set; }

        public TblRoles Role { get; set; }
        public ICollection<TblJumbo> TblJumbo { get; set; }
        public ICollection<TblSlitting> TblSlitting { get; set; }
    }
}
