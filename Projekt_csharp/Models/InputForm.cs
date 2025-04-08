using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Projekt_csharp.Models
{
    public class InputForm
    {
        [Required(ErrorMessage = "Jméno je povinné.")]
        public string? Jmeno { get; set; }

        [Required(ErrorMessage = "Příjmení je povinné.")]
        public string? Prijmeni { get; set; }

        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefon je povinný.")]
        [Phone(ErrorMessage = "Neplatné telefonní číslo.")]
        public string? Telefon { get; set; }

        [Required(ErrorMessage = "Rodné číslo je povinné.")]
        [CustomValidation(typeof(InputForm), "ValidateRC")]
        public string? RC { get; set; }

        [Required(ErrorMessage = "Datum narození je povinné.")]
        public DateTime? DatumNarozeni { get; set; }

        [Required(ErrorMessage = "Adresa je povinná.")]
        public string? Adresa { get; set; }

        [Required(ErrorMessage = "Město je povinné.")]
        public string? Mesto { get; set; }

        [Required(ErrorMessage = "PSČ je povinné.")]
        public string? PSC { get; set; }

        public int? studyProgramID1 { get; set; }
        public int? studyProgramID2 { get; set; }

        public int? studyProgramID3 { get; set; }

        public string? studyFields { get; set; }
        
        public static ValidationResult ValidateRC(string rc, ValidationContext context)
        {
            if (string.IsNullOrEmpty(rc))
            {
                return new ValidationResult("Rodné číslo je povinné.");
            }

            // Příklad jednoduché validace rodného čísla
            var regex = new Regex(@"^\d{6}\/?\d{4}$");
            if (!regex.IsMatch(rc))
            {
                return new ValidationResult("Neplatné rodné číslo.");
            }

            return ValidationResult.Success;
        }
    }
}