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
    /// Interaction logic for UpdateStudyField.xaml
    /// </summary>
    public partial class UpdateStudyField : Window
    {
        private readonly MyORM _myORM;
        private readonly StudyFieldApplication _studyFieldApplication;

        public event EventHandler StudyFieldUpdated;
        public UpdateStudyField(MyORM myORM, StudyFieldApplication studyFieldApplication )
        {
            InitializeComponent();
            _myORM = myORM;
            _studyFieldApplication = studyFieldApplication;
            LoadStudyFields();
        }

        private  async void Update(object sender, RoutedEventArgs e)
        {
            int newStudyFieldId = (int)StudyFieldComboBox.SelectedValue;

            var existingApplications = await _myORM.SelectWhereAsync<StudyFieldApplication>(
                new Dictionary<string, object> { { "HighSchoolApplicationId", _studyFieldApplication.HighschoolApplicationId } });

            if (existingApplications.Any(app => app.StudyFieldId == newStudyFieldId && app.StudyFieldApplicationId != _studyFieldApplication.StudyFieldApplicationId))
            {
                MessageBox.Show("Nelze mít dva stejné studijní programy v jedné přihlášce.");

                return;
            }

            _studyFieldApplication.StudyFieldId = newStudyFieldId;

            await _myORM.UpdateInAsync(_studyFieldApplication);

            StudyFieldUpdated?.Invoke(this, EventArgs.Empty);

            Close();
        }

        private async void LoadStudyFields()
        {
            var studyFields = await _myORM.SelectAllAsync<StudyField>();

            StudyFieldComboBox.ItemsSource = studyFields;
            StudyFieldComboBox.DisplayMemberPath = "StudyFieldCode";
            StudyFieldComboBox.SelectedValuePath = "StudyFieldId";
            StudyFieldComboBox.SelectedValue = _studyFieldApplication.StudyFieldId;
        }   
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
