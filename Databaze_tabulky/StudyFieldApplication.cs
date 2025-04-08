using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databaze_tabulky
{
    public class StudyFieldApplication
    {
        public int StudyFieldApplicationId { set; get; }

        public int HighschoolApplicationId { set; get; }
        public int StudyFieldId { set; get; }
    }
}
