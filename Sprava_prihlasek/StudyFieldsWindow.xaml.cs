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
using Databaze_tabulky;

namespace Sprava_prihlasek
{
    /// <summary>
    /// Interaction logic for StudyFieldsWindow.xaml
    /// </summary>
    public partial class StudyFieldsWindow : Window
    {
        private readonly MyORM _myORM;
        private readonly int _highSchoolID;
        public StudyFieldsWindow(MyORM myORM, int highschoolId)
        {

            InitializeComponent();
            _myORM = myORM;
            _highSchoolID = highschoolId;

            _ = LoadStudyFields();
        }

        private async Task LoadStudyFields()
        {
            var studyFields =  await _myORM.SelectAllWhereAsync<StudyField>(
                   new Dictionary<string, object> { { "HighSchoolId", _highSchoolID } });
            Obory.ItemsSource = studyFields;
        }
    }
}
