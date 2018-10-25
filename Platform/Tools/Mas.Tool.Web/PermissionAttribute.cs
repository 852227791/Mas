using System;
using System.Collections.Generic;
using System.Text;

namespace Mas.Tool.Web
{
    public class PermissionAttribute : Attribute
    {
        public PermissionAttribute(string moduleId = "", int actionValue = 0)
        {
            ModuleId = moduleId;
            ActionValue = actionValue;
        }

        public string ModuleId { get; set; }
        public int ActionValue { get; set; }
    }
}
