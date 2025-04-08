using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Databaze_tabulky;

namespace Sprava_prihlasek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyORM _myORM;
        private readonly MyORM _databaseContext;
        string database_path = "Data Source=C:\\Users\\judis\\AppData\\Local\\prihlasky.db;";
        string assebly_path = "C:\\Users\\judis\\source\\repos\\Projekt_csharp\\Databaze_tabulky\\bin\\Debug\\net8.0\\Databaze_tabulky.dll";

        public MainWindow()
        {
            //_databaseContext = new DatabaseContext();
            _myORM = new MyORM(database_path, assebly_path);
            InitializeComponent();
        }

        private void Open_applications(object sender, RoutedEventArgs e)
        {
            StudyApplications okenko = new StudyApplications(_myORM);
            okenko.Show();
            this.Close();

        }

        private void Open_highschools(object sender, RoutedEventArgs e)
        {
            HighschoolsWindow okenko = new HighschoolsWindow(_myORM);
            okenko.Show();
            this.Close();
        }


    }
}