using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  partial  class C_Code
    {
        public C_Code(int oID, string codeClass, string codeName, string memo, string mender, DateTime mendDate)
        {
            OID = oID;
            CodeClass = codeClass;
            CodeName = codeName;
            Memo = memo;
            Mender = mender;
            MendDate = mendDate;
        }
        public C_Code(){}
        public int OID { get; set; }
        public string CodeClass { get; set; }
        public string CodeName { get; set; }
        public string Memo { get; set; }
        public string Mender { get; set; }
        public DateTime MendDate { get; set; }

    }
}
