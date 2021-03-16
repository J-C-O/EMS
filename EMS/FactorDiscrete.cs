using System.Xml.Serialization;

namespace EMSFactorClasses
{
    /// <summary>
    /// Basisklasse für diskrete atomare Faktoren.
    /// </summary>
    //Benötigt für Serialisierung bei weiteren Datentypen erweitern
    [XmlInclude(typeof(ArrayValue<int>))]
    [XmlInclude(typeof(ArrayValue<string>))]
    [XmlInclude(typeof(ArrayValue<double>))]
    public class FactorDiscrete : FactorLeaf
    {
        /// <summary>
        /// Gibt eine Zeichenkette mit den Werten der Eigenschaften Name und OutVal sowie eine Information
        /// dazu ob es ein diskreter oder kontinuierlicher Faktor ist.
        /// </summary>
        /// <returns>Eine Zeichenkette mit Name und OutVal</returns>
        public override string PrintNodes()
        {
            return "Discrete." + Name + " = " + OutVal;
        }

    }
}