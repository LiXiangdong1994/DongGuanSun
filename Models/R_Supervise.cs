using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class R_Supervise
    {
      
        public R_Supervise() { }

        public R_Supervise(int rID, int superviseType, string title, int urgency, string mender, DateTime mendDate)
        {
            RID = rID;
            SuperviseType = superviseType;
            Title = title;
            Urgency = urgency;
            Mender = mender;
            MendDate = mendDate;
        }

        public int RID { get; set; }
        public int SuperviseType { get; set; }
        public string Title { get; set; }
        public int Urgency { get; set; }
        public string Mender { get; set; }
        public DateTime MendDate { get; set; }
    }
}
