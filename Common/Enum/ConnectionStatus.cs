using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enum
{
    public enum ConnectionStatus
    {
        limbo = 0,
        open = 1,
        altered = 2,
        committed = 3,
        rolledback = 4,
        closed = 5
    }
}
