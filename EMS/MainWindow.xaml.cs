using System.Windows;
using EMS.ViewModels;

namespace EMS
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        ManageTreeViewModel manageView;
        EditTreeViewModel editView;

        /// <summary>
        /// Konstruktor der Klasse.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            

            //Views initialisieren
            editView = new EditTreeViewModel();
            manageView = new ManageTreeViewModel();
        }

        
        /// <summary>
        /// Öffnet die "Manage Tree" Ansicht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ManageTree_Click(object sender, RoutedEventArgs e)
        {
            DataContext = manageView;
        }

        /// <summary>
        /// Öffnet die "Edit Tree" Ansicht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_EditTree_Click(object sender, RoutedEventArgs e)
        {
            DataContext = editView;
        }

        /// <summary>
        /// Beendet die Anwendung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

