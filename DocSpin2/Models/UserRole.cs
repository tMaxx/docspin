using System;
    

namespace DocSpin2.Models
{
    public enum UserRole : byte
    {
        User = 0,
        Moderator = 1,
        Admin = 2,
		None = 8
    }
}
