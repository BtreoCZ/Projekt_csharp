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
    /// Interaction logic for UpdateSchoolWindow.xaml
    /// </summary>
    public partial class UpdateSchoolWindow : Window
    {
        private readonly MyORM _myORM;
        private readonly int _highSchoolID;
        private readonly HighSchool _highschool;
        public UpdateSchoolWindow(MyORM myORM, HighSchool highschool,int highSchoolID)
        {
            InitializeComponent();
            _myORM = myORM;
            _highSchoolID = highSchoolID;
            _highschool = highschool;

            Nazev.Text = _highschool.Name;
            Mesto.Text = _highschool.City;
            Adresa.Text = _highschool.Address;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void UpdateSchool(object sender, RoutedEventArgs e)
        {
            _highschool.Name = Nazev.Text;
            _highschool.City = Mesto.Text;
            _highschool.Address = Adresa.Text;
            await _myORM.UpdateInAsync(_highschool);

            this.Close();
        }
    }
}
