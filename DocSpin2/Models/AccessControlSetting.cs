using System;


namespace DocSpin2.Models
{    
    [Flags]
    public enum AccessControlSetting : byte
    {
        None = 0,
        Read = 1,
        Comment = 2,
        Write = 3,
        Move = 4,
        Archival = 5,
        SupervisorOnly = 6
    }
}
