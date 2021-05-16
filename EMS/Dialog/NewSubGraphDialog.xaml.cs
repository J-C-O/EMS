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
    /// Interaktionslogik für NewSubGraphDialog.xaml
    /// </summary>
    public partial class NewSubGraphDialog : Window
    {
        public NewSubGraphDialog()
        {
            InitializeComponent();
        }

        public string RootName
        {
            get { return tb_rootName.Text; }
            set { tb_rootName.Text = value; }
        }

        public string ChildName
        {
            get { return tb_childName.Text; }
            set { tb_childName.Text = value; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
