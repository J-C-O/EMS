using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EMS.Dialog
{
    /// <summary>
    /// Interaktionslogik für EditValues.xaml
    /// </summary>
    public partial class EditValues : Window
    {
        /// <summary>
        /// Liste mit Werten vom Typ StringValue, die in der Benutzeroberfläche dargestellt wird.
        /// </summary>
        ObservableCollection<StringValue> values = new ObservableCollection<StringValue>();

        /// <summary>
        /// Rückgabe-Liste, die mit Werten vom Typ string befüllt ist.
        /// </summary>
        List<string> returnList = new List<string>();

        /// <summary>
        /// Rückgabe-Array, das die Werte von returnList hält.
        /// </summary>
        public string[] ResultArray
        {
            get { return returnList.ToArray(); }
        }

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

        public EditValues()
        {
            InitializeComponent();

            DataContext = this.GetValue();
            currentValue.Text = "NOT SET";

            NameHolder.Visibility = Visibility.Visible;
            newNodeMode = true;
        }
        public EditValues(string [] valuestoedit)
        {
            InitializeComponent();

            DataContext = this.GetValue(valuestoedit);
            currentValue.Text = "NOT SET";
        }
        public EditValues(string curValue, string[] valuestoedit)
        {
            InitializeComponent();

            DataContext = this.GetValue(valuestoedit);
            currentValue.Text = curValue;
        }
        public EditValues(string curValue, string[] valuestoedit, string nodeName)
        {
            InitializeComponent();

            DataContext = this.GetValue(valuestoedit);
            currentValue.Text = curValue;
            textBlockofName.Text = "Name: " + nodeName;
        }

        /// <summary>
        /// Gibt values zurück.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<StringValue> GetValue()
        {
            return values;
        }
        /// <summary>
        /// Fügt die Werte aus valstoload values hinzu und gibt values zurück.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<StringValue> GetValue(string[] valstoload)
        {
            foreach(string item in valstoload)
            {
                values.Add(new StringValue() { Value = item });
            }
            return values;
        }

        /// <summary>
        /// Entfernt das Aufrufende Element aus values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeValue_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = (StringValue)button.DataContext;


           this.values.Remove(item);           
        }

        /// <summary>
        /// Fügt die Elemente der Listbox ArrayValues aus der Benutzeroberfläche returnList hinzu und schließt das Dialogfenster. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            string nullString = "";
            if ((newNodeMode && ResponseText != nullString && !ArrayValues.Items.IsEmpty) || (!newNodeMode && !ArrayValues.Items.IsEmpty))
            {
                foreach (StringValue item in ArrayValues.Items)
                {
                    returnList.Add(item.Value);
                }
                DialogResult = true;
            }            
        }

        /// <summary>
        /// Fügt values einen neuen Wert hinzu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addValue_Click(object sender, RoutedEventArgs e)
        {
            if(newValueHolder.Text != null)
            {
                values.Add(new StringValue() { Value = newValueHolder.Text });
            }
            newValueHolder.Text = null;
        }
    }

    /// <summary>
    /// Hilfsklasse für EditValues
    /// </summary>
    public class StringValue
    {
        public string Value { get; set; }
    }
    
}
