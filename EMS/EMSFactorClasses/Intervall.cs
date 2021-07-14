using System.Xml.Serialization;

namespace EMS.EMSFactorClasses
{
    /// <summary>
    /// Beschreibt ein Objekt, welches einen kontinuierlichen Wertebereich über ein Intervall abbildet.
    /// </summary>
    public class Intervall : FactorContinuous
    {
        /// <summary>
        /// Feld für das aufsummieren der Werte.
        /// </summary>
        private decimal tmp;
        /// <summary>
        /// Startwert eines Intervalls.
        /// </summary>
        [XmlAttribute("StartValue")]
        public decimal StartVal { get; set; }
        /// <summary>
        /// Endwert eines Intervalls.
        /// </summary>
        [XmlAttribute("EndValue")] 
        public decimal EndVal { get; set; }
        /// <summary>
        /// Schrittweite eines Intervalls. 
        /// </summary>
        [XmlAttribute("IncrementValue")]
        public decimal Increment { get; set; }

        /// <summary>
        /// Konstruktor der Klasse.
        /// </summary>
        /// <param name="name">Definiert die Eigenschaft Name</param>
        /// <param name="sv">Definiert die Eigenschaft StartVal und OutVal</param>
        /// <param name="ev">Definiert die Eigenschaft EndVal</param>
        /// <param name="iv">Definiert die Eigenschaft tmp</param>
        public Intervall(string name, double sv, double ev, double iv)
        {
            Name = name;
            StartVal = (decimal)sv;
            EndVal = (decimal)ev;
            Increment = (decimal)iv;
            OutVal = sv.ToString();
            tmp = (decimal)sv;

            Composite = false;
        }
        public Intervall(string name, decimal sv, decimal ev, decimal iv)
        {
            Name = name;
            StartVal = sv;
            EndVal = ev;
            Increment = iv;
            OutVal = sv.ToString();
            tmp = sv;

            Composite = false;
        }
        /// <summary>
        /// Konstruktor der Klasse.
        /// Benötigt für Deserialisierung.
        /// </summary>
        public Intervall()
        {
            Name = "Discrete";
            Composite = false;
        }

        /// <summary>
        /// Setzt das Feld tmp auf den Wert von StartVal.
        /// Setzt die Eigenschaft OutVal auf den Wert von StartVal.
        /// </summary>
        public override void SetInitVal()
        {
            tmp = StartVal;
            OutVal = StartVal.ToString();
        }

        /// <summary>
        /// Prüft ob tmp + Increment kleiner gleich EndVal ist.
        /// Wenn ja, wird Summe aus tmp und Increment geschrieben und das Ergebnis in OutVal geschrieben.
        /// </summary>
        public override void GetNext()
        {
            if (tmp + Increment <= EndVal)
            {
                tmp += Increment;
                OutVal = tmp.ToString();
            }
        }

        /// <summary>
        /// Prüft tmp == EndVal.
        /// </summary>
        /// <returns>Einen booleschen Wert: true-Endwert wurde noch nicht erreicht, false-Endwert wurde erreicht</returns>
        public override bool HasNext()
        {
            if (tmp == EndVal)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}