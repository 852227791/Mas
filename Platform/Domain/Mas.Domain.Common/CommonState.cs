using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mas.Domain.Common
{
    [Description("通用状态")]
    public enum CommonState
    {
        [Description("启用")] Enable = 1,
        [Description("停用")] Disable = 2
    }
}
