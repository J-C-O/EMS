using System.Text.RegularExpressions;
using System.Windows;

namespace EMS.Dialog
{
    /// <summary>
    /// Interaktionslogik für NewContinuous.xaml
    /// </summary>
    public partial class NewContinuous : Window
    {
        public NewContinuous()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return tb_nodeName.Text; }
            set { tb_nodeName.Text = value; }
        }
        public string ResponseStartValue
        {
            get { return tb_startValue.Text; }
            set { tb_startValue.Text = value; }
        }
        public string ResponseEndValue
        {
            get { return tb_endValue.Text; }
            set { tb_endValue.Text = value; }
        }
        public string ResponseIncrement
        {
            get { return tb_increment.Text; }
            set { tb_increment.Text = value; }
        }

        private static readonly Regex _regex = new Regex("[^-0-9,]+");

        public decimal StartValueNUM { get; set; }
        public decimal EndValueNUM { get; set; }
        public decimal IncrementNUM { get; set; }


        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void PreviewTextInputHandler(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ResponseText == null || ResponseStartValue == null || ResponseEndValue == null || ResponseIncrement == null)
            {
                MessageBox.Show("Name oder Werte dürfen nicht leer sein");
            }
            else
            {
                StartValueNUM = decimal.Parse(ResponseStartValue);
                EndValueNUM = decimal.Parse(ResponseEndValue);
                IncrementNUM = decimal.Parse(ResponseIncrement);

                DialogResult = true;
            }
        }

        
    }
}
