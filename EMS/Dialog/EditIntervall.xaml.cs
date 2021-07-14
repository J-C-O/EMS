using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaktionslogik für EditIntervall.xaml
    /// </summary>
    public partial class EditIntervall : Window
    {
        private static readonly Regex _regex = new Regex("[^-0-9.]+");

        public decimal StartValue 
        { get; set; }
        public decimal EndValue
        { get; set; }
        public decimal Increment
        { get; set; }

        public string ResponseText
        {
            get { return NameHolder.Text; }
            set { NameHolder.Text = value; }
        }

        private bool newNodeMode = false;

        public EditIntervall()
        {
            InitializeComponent();
            NameHolder.Visibility = Visibility.Visible;
            newNodeMode = true;
        }
        public EditIntervall(decimal sv, decimal ev, decimal iv)
        {
            InitializeComponent();
            tb_SV.Text = sv.ToString();
            tb_EV.Text = ev.ToString();
            tb_IV.Text = iv.ToString();
        }
        public EditIntervall(decimal sv, decimal ev, decimal iv, decimal currentvalue)
        {
            InitializeComponent();
            tb_SV.Text = sv.ToString();
            tb_EV.Text = ev.ToString();
            tb_IV.Text = iv.ToString();

            tb_CV.Text = currentvalue.ToString();
        }
        public EditIntervall(decimal sv, decimal ev, decimal iv, decimal currentvalue, string nodeName)
        {
            InitializeComponent();
            tb_SV.Text = sv.ToString();
            tb_EV.Text = ev.ToString();
            tb_IV.Text = iv.ToString();

            tb_CV.Text = currentvalue.ToString();
            textBlockofName.Text = "Name: " + nodeName;
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void PreviewTextInputHandler(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((newNodeMode && ResponseText != null && tb_SV.Text != null && tb_EV.Text != null && tb_IV.Text != null) || 
                (!newNodeMode && tb_SV.Text != null && tb_EV.Text != null && tb_IV.Text != null))
            {
                StartValue = decimal.Parse(tb_SV.Text);
                EndValue = decimal.Parse(tb_EV.Text);
                Increment = decimal.Parse(tb_IV.Text);

                DialogResult = true;
            }
            //if(tb_SV.Text != null && tb_EV.Text != null && tb_IV.Text != null)
            //{
            //    StartValue = decimal.Parse(tb_SV.Text);
            //    EndValue = decimal.Parse(tb_EV.Text);
            //    Increment = decimal.Parse(tb_IV.Text);
            //}
            //DialogResult = true;
        }
    }
}
