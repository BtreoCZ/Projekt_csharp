using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databaze_tabulky
{
    public class StudyField
    {
        public int StudyFieldId { get; set; }
        public int HighSchoolId { get; set; }
        public string StudyFieldCode { get; set; }
        public string StudyName { get; set;}
        public string StudyDesc { get; set;}
        public int StudySlots { get; set;}
        public bool Graduation { get; set;}

    }
}
