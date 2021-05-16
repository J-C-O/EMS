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
    /// Interaktionslogik für NewLeafNodeDialog.xaml
    /// </summary>
    public partial class NewLeafNodeDialog : Window
    {
        public NewLeafNodeDialog()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return tb_nodeName.Text; }
            set { tb_nodeName.Text = value; }
        }

        public string ResponseValue
        {
            get { return tb_value.Text; }
            set { tb_value.Text = value; }
        }

        public string[] values;
        private int idx = 0;
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void AddValue_Click(object sender, RoutedEventArgs e)
        {
            values[idx] = this.ResponseValue;
            //this.PrintValues();
            this.ResponseValue = "";
            idx++;
        }

        private void PrintValues()
        {
            foreach(string val in values)
            {
                outputBlock.Text += val + ";";
            }
        }
    }
}
