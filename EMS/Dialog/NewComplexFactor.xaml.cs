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

namespace EMS.Dialog
{
    /// <summary>
    /// Interaktionslogik für NewComplexFactor.xaml
    /// </summary>
    public partial class NewComplexFactor : Window
    {
        /// <summary>
        /// Eigenschaft über die man den Text der Textbox tb_nodeName erreichen kann.
        /// </summary>
        public string ResponseText
        {
            get { return tb_nodeName.Text; }
            set { tb_nodeName.Text = value; }
        }

        /// <summary>
        /// Schließt das Fenster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(ResponseText != "")
            {
                DialogResult = true;
            }
            
        }

        public NewComplexFactor(string title)
        {
            InitializeComponent();
            this.Title = title;
        }


    }
}
