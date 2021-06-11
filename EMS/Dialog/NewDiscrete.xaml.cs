using EMS.EMSFactorClasses;
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
    /// Interaktionslogik für NewDiscrete.xaml
    /// </summary>
    public partial class NewDiscrete : Window
    {
        public NewDiscrete()
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
        List<string> valList = new List<string>();
        public string[] Values
        {
            get { return valList.ToArray(); }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(ResponseText == null)
            {
                MessageBox.Show("Name oder Wert dürfen nicht leer sein");
            } else
            {
                DialogResult = true;
            }  
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            valList.Add(ResponseValue);
            tb_Output.Text = ResponseValue + " added";
        }

        private void PrintButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string valString = "";
            foreach (string value in Values)
            {
                valString += value + " | ";
            }
            tb_Output.Text = "Values = | " + valString;
        }

        /// <summary>Indicates whether the specified array is null or has a length of zero.</summary>
        /// <param name="array">The array to test.</param>
        /// <returns>true if the array parameter is null or has a length of zero; otherwise, false.</returns>
        //public bool IsNullOrEmpty(this Array array)
        //{
        //    return (array == null || array.Length == 0);
        //}
    }
}
