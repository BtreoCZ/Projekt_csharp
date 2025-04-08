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
using Microsoft.Build.Framework;

namespace Sprava_prihlasek
{
    /// <summary>
    /// Interaction logic for HighschoolsWindow.xaml
    /// </summary>
    
    public partial class HighschoolsWindow : Window
    {
        private readonly MyORM _myORM;
        public HighschoolsWindow(MyORM myORM)
        {
            _myORM = myORM;
            InitializeComponent();
            _ = LoadHighSchools();
        }

        private void AddSchool(object sender, RoutedEventArgs e)
        {
            AddSchoolWindow addSchoolWindow = new AddSchoolWindow(_myORM);
            addSchoolWindow.HighSchoolAdded += async (s, e) => await LoadHighSchools();
            addSchoolWindow.ShowDialog();
        }

        private void AddSchoolWindow_HighSchoolAdded(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async Task LoadHighSchools()
        {
            var highschools = await _myORM.SelectAllAsync<HighSchool>();
            HighSchoolDataGrid.ItemsSource = highschools;
        }
        private async void DeleteSchool(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null && int.TryParse(button.Tag.ToString(), out int highSchoolId))
            {
                var result = MessageBox.Show("Opravdu chcete odstranit tuto školu?", "Potvrzení odstranění", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    await _myORM.DeleteFromAsync<HighSchool>(highSchoolId);

                    await LoadHighSchools();
                }
            }
        }
        private async void UpdateSchool(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && int.TryParse(button.Tag.ToString(), out int highSchoolId))
            {
                HighSchool highschool = await _myORM.SelectFromAsync<HighSchool>(highSchoolId);
                UpdateSchoolWindow updateSchoolWindow = new UpdateSchoolWindow(_myORM, highschool, highSchoolId);
                updateSchoolWindow.ShowDialog();
            }
        }
        private async void ShowStudyFields(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && int.TryParse(button.Tag.ToString(), out int highSchoolId))
            {
                StudyFieldsWindow studyFieldsWindow = new StudyFieldsWindow(_myORM, highSchoolId);
                studyFieldsWindow.ShowDialog();
            }
        }

    }
}
