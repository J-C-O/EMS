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
        EditorViewModel editor;


        /// <summary>
        /// Konstruktor der Klasse.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();



            //Views initialisieren
            manageView = new ManageTreeViewModel();
            editor = new EditorViewModel();

            DataContext = editor;

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
            Backend.EmsMsaglLinker.SetTreeGraph();
            DataContext = editor;
            
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

        private void button_testMAIN_Click(object sender, RoutedEventArgs e)
        {
            //Backend.EmsMsaglLinker.SetTreeGraph();
        }
    }
}

