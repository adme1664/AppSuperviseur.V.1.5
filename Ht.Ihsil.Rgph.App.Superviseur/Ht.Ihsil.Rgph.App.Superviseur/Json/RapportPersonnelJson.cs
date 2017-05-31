using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
   public class RapportPersonnelJson
    {
        public long rapportId { get; set; }
        public Nullable<long> persId { get; set; }
        public string codeDistrict { get; set; }
        public string comId { get; set; }
        public string deptId { get; set; }
        public Nullable<long> q1 { get; set; }
        public Nullable<long> q2 { get; set; }
        public Nullable<long> q3 { get; set; }
        public Nullable<long> q4 { get; set; }
        public Nullable<long> q5 { get; set; }
        public Nullable<long> q6 { get; set; }
        public Nullable<long> q7 { get; set; }
        public Nullable<long> q8 { get; set; }
        public Nullable<long> q9 { get; set; }
        public Nullable<long> q10 { get; set; }
        public Nullable<long> q11 { get; set; }
        public Nullable<long> q12 { get; set; }
        public Nullable<long> q13 { get; set; }
        public string q14 { get; set; }
        public Nullable<long> q15 { get; set; }
        public Nullable<long> score { get; set; }
        public string dateEvaluation { get; set; }
        public Nullable<long> reportSenderId { get; set; }
        public string rapportName { get; set; }
    }
}
