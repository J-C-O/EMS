using System;
using System.IO;
using System.Windows.Controls;
using System.Xml.Serialization;
using EMSFactorClasses;

namespace EMSFactorClient
{
    /// <summary>
    /// Klasse zum Verwalten eines Objektes vom Typ Factor.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Objekt vom Typ Factor.
        /// </summary>
        Factor tree = new FactorComplex();
        /// <summary>
        /// Objekt vom Typ TextBlock für GUI-Kompatibilität.
        /// </summary>
        TextBlock StateBox = new TextBlock();

        /// <summary>
        /// Konstruktor der Klasse.
        /// </summary>
        /// <param name="textBlock">Definiert die Eigenschaft StateBox</param>
        public Client(TextBlock textBlock)
        {
            StateBox = textBlock;
        }
        /// <summary>
        /// Konstruktor der Klasse
        /// </summary>
        public Client()
        {

        }

        /// <summary>
        /// Gibt die Baumstruktur in der Konsole aus
        /// </summary>
        public void PrintTree()
        {
            Console.WriteLine($"RESULT: {tree.PrintNodes()}\n");
        }

        /// <summary>
        /// Gibt den Baum auf der GUI aus
        /// </summary>
        public void PrintTreeGUI()
        {
            StateBox.Text += "\n" + tree.PrintNodes();
        }

        /// <summary>
        /// Prüft ob ein Faktorbaum weitergeschalten werden kann und schaltet diesen weiter falls möglich.
        /// Sollte ein weiterschalten nicht möglich sein wird eine Meldung ein TextBlock-Objekt geschrieben.
        /// </summary>
        public void Next()
        {
            if (tree.HasNext())
            {
                tree.GetNext();
                StateBox.Text += "\n" + tree.PrintNodes();
            }
            else
            {
                StateBox.Text += "\n no new configurations";
            }   
        }

        /// <summary>
        /// Initialisiert einen Faktorbaum und gibt diesen in einem Textblock (GUI) aus
        /// </summary>
        public void Initialize()
        {
            StateBox.Text += "\n Initialize...";
            tree.SetInitVal();
            StateBox.Text = tree.PrintNodes();
        }

        /// <summary>
        /// Exportiert eine Experimentkonfiguration in eine XML-Datei.
        /// </summary>
        /// <param name="configPath">Dateipfad der XML-Datei</param>
        public void WriteConfig(string configPath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Factor));
            TextWriter writer = new StreamWriter(configPath);

            xmlSerializer.Serialize(writer, tree);
            writer.Close();
            StateBox.Text += "\nKonfiguration gespeichert";
        }

        /// <summary>
        /// Lädt einen Faktorbaum aus einer XML-Datei
        /// </summary>
        /// <param name="configPath">Dateipfad der XML-Datei</param>
        public void LoadConfig(string configPath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Factor));
            TextReader reader = new StreamReader(configPath);

            tree = (Factor)xmlSerializer.Deserialize(reader);
            reader.Close();
            StateBox.Text += "\nKonfiguration eingelesen";
        }

        /// <summary>
        /// Baut eine Baumstruktur auf und Prüft ob rootNode kein Blatt ist
        /// </summary>
        /// <param name="rootNode">Faktor der als Wurzel dient</param>
        /// <param name="nextNode">Faktor der als weiterer Knoten dient</param>
        public void Build(Factor rootNode, Factor nextNode)
        {
            if (rootNode.IsComposite())
            {
                rootNode.AddNode(nextNode);
            }
        }

    }
}
