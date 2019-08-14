using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class result
    {
        public string code;
        public string msg;

        public result(string code, string msg)
        {
            this.code = code;
            this.msg = msg;
        }
        public result(){}
    }
}
