namespace EMS.EMSFactorClasses
{
    /// <summary>
    /// Diese Klasse beschreibt ermöglicht es parallele Faktoren abzubilden.
    /// </summary>
    public class FactorParallel : FactorComplex
    {
        /// <summary>
        /// Konstruktor der Klasse.
        /// </summary>
        /// <param name="name">Definiert die Eigenschaft Name.</param>
        public FactorParallel(string name)
        {
            Name = name;
            Composite = true;
        }
        /// <summary>
        /// Konstruktor der Klasse.
        /// Benötigt für Deserialisierung.
        /// </summary>
        public FactorParallel()
        {
            Name = "Parallel";
            Composite = true;
        }

        
    }
}