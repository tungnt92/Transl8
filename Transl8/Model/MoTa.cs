using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGhiChu.Model
{
    class MoTa
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Target { get; set; }
        public MoTa()
        {
            //empty constructor  
        }
        public MoTa(string type, string title, string icon, string target)
        {
            Type = type;
            Title = title;
            Icon = icon;
            Target = target;
        }
    }
}
