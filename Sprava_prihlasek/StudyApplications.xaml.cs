using Databaze_tabulky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sprava_prihlasek
{
    /// <summary>
    /// Interaction logic for StudyApplications.xaml
    /// </summary>
    public partial class StudyApplications : Window
    {
        private readonly MyORM _myORM;
        public StudyApplications(MyORM myORM)
        {
            _myORM = myORM;
            InitializeComponent();
            List<HighschoolApplication> highschoolApplication = new List<HighschoolApplication>();
            highschoolApplication.Add(new HighschoolApplication
            {
                HighschoolApplicationId = 1,
                StudentId = 1,
                ApplicationDate = DateTime.Now,
                Status = "Waiting"
            });

            //Prihlasky.ItemsSource = highschoolApplication;
            LoadApplications();
        }
        public async void LoadApplications()
        {
            
            var applications = await _myORM.SelectAllAsync<HighschoolApplication>();
            Prihlasky.ItemsSource = applications;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        private async void Update(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            

            if (button != null && int.TryParse(button.Tag.ToString(), out int applicationId))
            {
                var updateApplication = await _myORM.SelectFromAsync<HighschoolApplication>(applicationId);
                var updateWindow = new UpdateApplication(_myORM, updateApplication);
                updateWindow.HighSchoolApplicationUpdated += async (s, ev) => LoadApplications();
                updateWindow.ShowDialog();
            }
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && int.TryParse(button.Tag.ToString(), out int applicationId))
            {
                Console.WriteLine(applicationId);
                var studyFields = new StudyFields(_myORM, applicationId);
                studyFields.Show();
            }
            
        }
    }
}
