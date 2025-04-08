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
    /// Interaction logic for AddSchoolWindow.xaml
    /// </summary>
    
    public partial class AddSchoolWindow : Window
    {
        public event EventHandler HighSchoolAdded;
        private readonly MyORM _myORM;
        public AddSchoolWindow(MyORM myORM)
        {
            _myORM = myORM;
            InitializeComponent();
        }

        private async void AddSchoolToDB(object sender, RoutedEventArgs e)
        {
            var newHighSchool = new HighSchool
            {
                Name = Nazev.Text,
                City = Mesto.Text,
                Address = Adresa.Text
            };
            await _myORM.InsertIntoAsync(newHighSchool);
            HighSchoolAdded?.Invoke(this, EventArgs.Empty);
            this.Close();

        }
    }
}
