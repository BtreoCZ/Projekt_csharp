using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databaze_tabulky
{
    public class HighschoolApplication
    {
        public int HighschoolApplicationId { get; set; }
        public int StudentId { get; set; }
        public DateTime ApplicationDate { get; set; }

        public string Status { get; set; }
    }
}
