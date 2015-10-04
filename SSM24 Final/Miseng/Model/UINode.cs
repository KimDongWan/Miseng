using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miseng.Model
{
    public class UINode
    {
        public int Id { get; set; }

        public string ControlName { get; set; }

        public int ParentId { get; set; }
    }
}
