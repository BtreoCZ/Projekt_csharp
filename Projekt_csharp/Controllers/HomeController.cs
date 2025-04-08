using Microsoft.AspNetCore.Mvc;
using Databaze_tabulky;
using Projekt_csharp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;


namespace Projekt_csharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyORM _myORM;
        public HomeController(ILogger<HomeController> logger,MyORM myORM)
        {
            _logger = logger;
            _myORM = myORM;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult FormPage()
        {
            ViewBag.skoly = _myORM.SelectAllAsync<HighSchool>().Result;
            return View();
        }
        // POST: InputForm
        [HttpPost]
        public async Task<IActionResult> Submit(InputForm model)
        {
            // Získání vybraných ID oborů z vlastnosti studyFields
            if(model.studyFields == null)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Nebyl vybrán žádný obor" });
            }
            var selectedStudyFieldIds = model.studyFields.Split(',').Select(int.Parse).ToList();

            
            foreach (var id in selectedStudyFieldIds)
            {          
                Console.WriteLine("Vybrané ID oboru: " + id);
            }
            try { 
            int studentID;
            if (ModelState.IsValid)
            {
                Student student = new Student();
                student.Jmeno = model.Jmeno;
                student.Prijmeni = model.Prijmeni;
                student.RodneCislo = model.RC;
                student.DatumNarozeni = (DateTime)model.DatumNarozeni;
                student.Adresa = model.Adresa;
                student.Telefon = model.Telefon;
                student.Email = model.Email;
                student.Mesto = model.Mesto;
                student.PSC = model.PSC;
                    // Kontrola existence studenta podle rodného čísla
                    var existingStudent = (await _myORM.SelectWhereAsync<Student>(
                        new Dictionary<string, object> { { "RodneCislo", model.RC } }, useLike: false)
                    ).FirstOrDefault();
                    if (existingStudent == null)
                    {
                        await _myORM.InsertIntoAsync(student);
                        studentID = student.StudentId;
                    }
                    else {
                        
                        studentID = existingStudent.StudentId;

                    }

                    // Kontrola již podaných přihlášek
                    var prihlasky = await _myORM.SelectWhereAsync<HighschoolApplication>(
                        new Dictionary<string, object> { { "StudentId", studentID } });

                    if (prihlasky.Any())
                    {
                        return View("Error", new ErrorViewModel { ErrorMessage = "Existuje přihláška pro daného studenta" });
                    }

                    var selectedProgramIds = selectedStudyFieldIds.Distinct().ToList();

                    if (selectedProgramIds.Count != selectedStudyFieldIds.Count)
                    {

                        return View("Error", new ErrorViewModel { ErrorMessage = "Přihláška podána na stejný obor" });
                    }

                    HighschoolApplication highschoolApplication = new HighschoolApplication();
                    highschoolApplication.StudentId = studentID;
                    highschoolApplication.ApplicationDate = DateTime.Now;
                    highschoolApplication.Status = "Čekající";

                    await _myORM.InsertIntoAsync(highschoolApplication);

                    foreach (var programId in selectedProgramIds)
                    {
                        StudyFieldApplication studyFieldApplication = new StudyFieldApplication();
                        studyFieldApplication.HighschoolApplicationId = highschoolApplication.HighschoolApplicationId;
                        studyFieldApplication.StudyFieldId = programId;
                        await _myORM.InsertIntoAsync(studyFieldApplication);
                    }
                    



                    return View("Success");
            }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error", new ErrorViewModel { ErrorMessage = "Něco se pokazilo" });
            }
            return View("Index", model);
        }
        // GET: Success
        public IActionResult Success()
        {
            return View();
        }

        public JsonResult GetStudyFields(int highschoolId)
        {
            var studyFields = _myORM.SelectAllWhereAsync<StudyField>(
                new Dictionary<string, object> { { "HighSchoolId", highschoolId } },
                useLike: false
            ).Result;
            return Json(studyFields);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}