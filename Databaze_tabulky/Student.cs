using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databaze_tabulky
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string RodneCislo { get; set; }
        public DateTime DatumNarozeni { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Mesto { get; set;}
        public string PSC { get; set;}
    }
}
