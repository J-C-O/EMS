using Microsoft.Win32;
using System;
using System.Windows;
using EMSFactorClient;

namespace EMS
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Objekt vom Typ Client um mit einem Faktorbaum arbeiten zu können.
        /// </summary>
        Client client;
        /// <summary>
        /// Objekt vom Typ OpenFileDialog zum Laden von XML-Konfigurationen.
        /// </summary>
        OpenFileDialog openFile = new OpenFileDialog();
        /// <summary>
        /// Objekt vom Typ SaveFileDialog zum Speichern von XML-Konfigurationen.
        /// </summary>
        SaveFileDialog saveFile = new SaveFileDialog();

        /// <summary>
        /// Konstruktor der Klasse.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Client-Instanz mit übergebenem TextBlock zur Ausgabe
            client = new Client(tbOutPut);

            //Setzen des Initialverzeichniss und des Dateifilters
            openFile.InitialDirectory = "c:\\";
            openFile.Filter = "xml files (*.xml)|*.xml";

            //Setzen des Initialverzeichniss und des Dateifilters sowie den Standardnamen im Format config_<TIMESTAMP>.xml
            saveFile.InitialDirectory = "c:\\";
            saveFile.Filter = "xml files (*.xml)|*.xml";
            saveFile.FileName = "config_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        /// Initialisiert einen Faktorbaum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInitialize_Click(object sender, RoutedEventArgs e)
        {
            client.Initialize();
        }

        /// <summary>
        /// Schaltet einen Faktorbaum weiter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetNext_Click(object sender, RoutedEventArgs e)
        {
            client.Next();
        }

        /// <summary>
        /// Speichert eine Faktorbaumkonfiguration ab.
        /// Hierfür wird ein Dialogfenster geöffnet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string savePath;
            saveFile.ShowDialog();

            savePath = saveFile.FileName;
            textBoxSave.Text = savePath;
            client.WriteConfig(savePath);
        }

        /// <summary>
        /// Lädt eine Faktorbaumkonfiguration.
        /// Hierfür wird ein Dialogfenster geöffnet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            string loadPath;
            if (openFile.ShowDialog() == true)
            {
                loadPath = openFile.FileName;

                textBoxLoad.Text = loadPath;
                client.LoadConfig(loadPath);
            }

        }

        /// <summary>
        /// Gibt eine aktuelle Faktorbaumkonfiguration aus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            client.PrintTreeGUI();
        }
    }
}

