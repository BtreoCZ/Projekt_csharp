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
using static System.Net.Mime.MediaTypeNames;

namespace Sprava_prihlasek
{
    /// <summary>
    /// Interaction logic for StudyFields.xaml
    /// </summary>
    public partial class StudyFields : Window
    {
        private readonly MyORM _myORM;
        private readonly int _applicationID;
        public StudyFields(MyORM myORM,int applicationID)
        {
            _myORM = myORM;
            _applicationID = applicationID;
            InitializeComponent();
            _=LoadStudyFields();
        }

        private async Task LoadStudyFields()
        {
            var application =  await _myORM.SelectWhereAsync<StudyFieldApplication>(
                new Dictionary<string, object> { { "HighSchoolApplicationId", _applicationID } });
            var studyFields = new List<StudyField>();

            foreach (var studyFieldApp in application)
            {
                Console.WriteLine(studyFieldApp.StudyFieldId);
                var studyField = await _myORM.SelectFromAsync<StudyField>(studyFieldApp.StudyFieldId);
                if (studyField != null) {
                    studyFields.Add(studyField);
                }
                
            }
            StudyFieldsDataGrid.ItemsSource = studyFields;

        }
        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void UpdateStudyField(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                int studyFieldId = (int)button.Tag;

                var studyFieldApplication = _myORM.SelectWhereAsync<StudyFieldApplication>(
                    new Dictionary<string, object>
                    {
                        { "HighSchoolApplicationId", _applicationID },
                        { "StudyFieldId", studyFieldId }
                    }).Result.FirstOrDefault();

                if (studyFieldApplication != null)
                {
                    var updateWindow = new UpdateStudyField(_myORM, studyFieldApplication);

                    updateWindow.StudyFieldUpdated += async (s, ev) => await LoadStudyFields();

                    updateWindow.ShowDialog();
                }
            }
        }
    }
}
