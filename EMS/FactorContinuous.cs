using System.Xml.Serialization;

namespace EMSFactorClasses
{
    /// <summary>
    /// Basisklasse für kontinuierliche atomare Faktoren.
    /// </summary>
    //Benötigt für Serialisierung
    [XmlInclude(typeof(Intervall))]
    public class FactorContinuous : FactorLeaf
    {
        /// <summary>
        /// Gibt eine Zeichenkette mit den Werten der Eigenschaften Name und OutVal sowie eine Information
        /// dazu ob es ein diskreter oder kontinuierlicher Faktor ist.
        /// </summary>
        /// <returns>Eine Zeichenkette mit Name und OutVal</returns>
        public override string PrintNodes()
        {
            return "Continuous." + Name + " = " + OutVal;
        }
    }
}