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
    /// Interaction logic for UpdateApplication.xaml
    /// </summary>
    public partial class UpdateApplication : Window
    {
        private readonly MyORM _myORM;
        private readonly HighschoolApplication _highschoolApplication;
        public event EventHandler HighSchoolApplicationUpdated;
        public UpdateApplication(MyORM myORM,HighschoolApplication highschoolApplication)
        {
            InitializeComponent();
            _myORM = myORM;
            _highschoolApplication = highschoolApplication;
            Status.Text = _highschoolApplication.Status;
        }


        private async void Update(object sender, RoutedEventArgs e)
        {

            _highschoolApplication.Status = Status.Text;
            await _myORM.UpdateInAsync(_highschoolApplication);

            HighSchoolApplicationUpdated?.Invoke(this, EventArgs.Empty);

            this.Close();

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
