using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace EMS.Dialog
{
    /// <summary>
    /// Interaktionslogik für EditIntervall.xaml
    /// </summary>
    public partial class EditIntervall : Window
    {
        private static readonly Regex _regex = new Regex("[^-0-9,]+");
        //private static readonly Regex _regex = new Regex(@"^(-?[1-9]+\d*([.]\d+)?)$|^(-?0[.]\d*[1-9]+)$|^0$|^0.0$");

        /// <summary>
        /// Eigenschaft für den Startwert.
        /// </summary>
        public decimal StartValue 
        { get; set; }

        /// <summary>
        /// Eigenschaft für den Endwert.
        /// </summary>
        public decimal EndValue
        { get; set; }

        /// <summary>
        /// Eigenschaft für die Schrittweite.
        /// </summary>
        public decimal Increment
        { get; set; }

        /// <summary>
        /// Eigenschaft über die man den Text der Textbox NameHolder erreichen kann.
        /// </summary>
        public string ResponseText
        {
            get { return NameHolder.Text; }
            set { NameHolder.Text = value; }
        }

        /// <summary>
        /// Dieses Feld entscheidet ob ein neuer Knoten angelegt wird (Name kann geändert werden) oder ein bereits vorhandener editiert wird.
        /// </summary>
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
        
        /// <summary>
        /// Prüft ob die Eingabe in numerische Textboxen zulässig ist.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        /// <summary>
        /// Eventhandler für Texteingabe.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTextInputHandler(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        /// <summary>
        /// Schließt das Fenster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nullString = "";
            if((newNodeMode && ResponseText != nullString
                && tb_SV.Text != nullString
                && tb_EV.Text != nullString
                && tb_IV.Text != nullString) 
                || 
                (!newNodeMode && tb_SV.Text != nullString && tb_EV.Text != nullString && tb_IV.Text != nullString)
            )
            {
                try
                {
                    StartValue = decimal.Parse(tb_SV.Text);
                    EndValue = decimal.Parse(tb_EV.Text);
                    Increment = decimal.Parse(tb_IV.Text);

                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }
    }
}
