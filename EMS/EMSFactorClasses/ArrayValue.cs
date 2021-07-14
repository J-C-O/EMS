using System.Xml.Serialization;

namespace EMS.EMSFactorClasses
{
    /// <summary>
    /// Beschreibt ein Objekt welches Werte aus einem diskrete Werte in einem Array vom Typ T hält.
    /// </summary>
    /// <typeparam name="T">Platzhalter für den Datentyp</typeparam>
    public class ArrayValue<T> : FactorDiscrete
    {
        /// <summary>
        /// Generisches Array in dem alle möglichen Werte des Faktors gespeichert werden.
        /// </summary>
        [XmlArray("Range")]
        public T[] Values { get; set; }
        /// <summary>
        /// Index für den in OutVal genutzten Wert aus Values[ValIDX].
        /// </summary>
        [XmlAttribute("RangePosition")]
        public int ValIDX { get; set; }
        
        /// <summary>
        /// Konstruktor der generischen Klasse.
        /// Unterstützte Datentypen für die Serialisierung: string, int, double
        /// Weitere Datentypen müssen in FactorDiscrete.cs ergänzt werden.
        /// Setzt die Eigenschaft OutVal auf den Wert in Values[0].
        /// </summary>
        /// <param name="name">Definiert die Eigenschaft Name</param>
        /// <param name="vals">Definiert die Eigenschaft Values</param>
        public ArrayValue(string name, T[] vals)
        {
            Name = name;
            Values = vals;
            OutVal = ValToString(0);

            Composite = false;
        }

        /// <summary>
        /// Konstruktor der Klasse.
        /// Benötigt für Deserialisierung.
        /// </summary>
        public ArrayValue()
        {
            Name = "Discrete";
            Composite = false;
        }

        /// <summary>
        /// Führt für Elemente in Values an der Stelle i die ToString()-Methode aus und gibt das Ergebnis zurück
        /// </summary>
        /// <param name="i">Index für die Eigenschaft Values</param>
        /// <returns>Eine Zeichenkette mit dem Wert aus Values[i]</returns>
        public string ValToString(int i)
        {
            return this.Values[i].ToString();
        }

        /// <summary>
        /// Setzt die Eigenschaft ValIDX auf 0.
        /// Setzt die Eigenschaft OutVal auf Values[0].
        /// </summary>
        public override void SetInitVal()
        {
            ValIDX = 0;
            OutVal = ValToString(0);
        }

        /// <summary>
        /// Inkrementiert die Eigenschaft ValIDX um 1 und setzt die Eigenschaft OutVal auf Values[ValIDX].
        /// </summary>
        public override void GetNext()
        {
            ValIDX++;

            if ((ValIDX > -1) && (ValIDX < Values.Length))
            {
                OutVal = ValToString(ValIDX);
            }
        }

        /// <summary>
        /// Prüft ob ValIDX gleich Values.Length - 1 ist.
        /// </summary>
        /// <returns>einen booleschen Wert: true-weitere Werte vorhanden; false-alle Werte wurden ausgegeben</returns>
        public override bool HasNext()
        {
            if (ValIDX == Values.Length - 1)
            {
                return false;
            }
            return true;
        }
    }
}