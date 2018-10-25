using System;
using System.Collections.Generic;
using System.Text;

namespace Mas.Tool.Common.Model
{
    public interface IUser
    {
        string UserId { get; set; }

        string UserName { get; set; }
    }
}
