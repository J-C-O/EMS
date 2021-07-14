using System;
using System.Windows;
using System.Windows.Controls;
using EMSFactorClient;
using Microsoft.Win32;
using EMS.Backend;

namespace EMS.Views
{
    /// <summary>
    /// Interaktionslogik für ManageTreeView.xaml
    /// </summary>
    public partial class ManageTreeView : UserControl
    {
        /// <summary>
        /// Objekt vom Typ OpenFileDialog zum Laden von XML-Konfigurationen.
        /// </summary>
        OpenFileDialog openFile = new OpenFileDialog();
        /// <summary>
        /// Objekt vom Typ SaveFileDialog zum Speichern von XML-Konfigurationen.
        /// </summary>
        SaveFileDialog saveFile = new SaveFileDialog();
        public ManageTreeView()
        {
            InitializeComponent();

            //Setzen des Initialverzeichniss und des Dateifilters
            openFile.InitialDirectory = "c:\\";
            openFile.Filter = "xml files (*.xml)|*.xml";

            //Setzen des Initialverzeichniss und des Dateifilters sowie den Standardnamen im Format config_<TIMESTAMP>.xml
            saveFile.InitialDirectory = "c:\\";
            saveFile.Filter = "xml files (*.xml)|*.xml";
            saveFile.FileName = "config_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        /// Lädt eine Faktorbaumkonfiguration.
        /// Hierfür wird ein Dialogfenster geöffnet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Load_Click(object sender, RoutedEventArgs e)
        {
            string loadPath;
            if (openFile.ShowDialog() == true)
            {
                loadPath = openFile.FileName;

                tb_Load.Text = loadPath;
                EmsMsaglLinker.LoadTreeConfig(loadPath);
                tbOutPut.Text = EmsMsaglLinker.StatusMessage;
            }
        }

        /// <summary>
        /// Speichert eine Faktorbaumkonfiguration ab.
        /// Hierfür wird ein Dialogfenster geöffnet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            string savePath;
            saveFile.ShowDialog();

            savePath = saveFile.FileName;
            tb_Save.Text = savePath;
            EmsMsaglLinker.SaveTreeConfig(savePath);
            tbOutPut.Text = EmsMsaglLinker.StatusMessage;
        }

        /// <summary>
        /// Initialisiert einen Faktorbaum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Initialize_Click(object sender, RoutedEventArgs e)
        {
            EmsMsaglLinker.InitializeTree();
            tbOutPut.Text = EmsMsaglLinker.PrintTree();
        }

        /// <summary>
        /// Gibt eine aktuelle Faktorbaumkonfiguration aus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Print_Click(object sender, RoutedEventArgs e)
        {
            tbOutPut.Text = EmsMsaglLinker.PrintTree();
        }

        /// <summary>
        /// Schaltet einen Faktorbaum weiter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Next_Click(object sender, RoutedEventArgs e)
        {
            EmsMsaglLinker.NextFactor();
            tbOutPut.Text = EmsMsaglLinker.StatusMessage;
        }
    }
}
