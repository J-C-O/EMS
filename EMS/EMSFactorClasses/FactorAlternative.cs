namespace EMS.EMSFactorClasses
{
    /// <summary>
    /// Mit dieser Klasse werden alternative Faktoren beschrieben.
    /// Implementiert das alternative Verhalten von Faktoren.
    /// </summary>
    public class FactorAlternative : FactorComplex
    {
        /// <summary>
        /// Konstruktor der Klasse.
        /// </summary>
        /// <param name="name">Definiert die Eigenschaft Name.</param>
        public FactorAlternative(string name)
        {
            Name = name;
            Composite = true;
        }

        /// <summary>
        /// Konstruktor der  Klasse.
        /// Benötigt für Deserialisierung.
        /// </summary>
        public FactorAlternative()
        {
            Name = "Alternative";
            Composite = true;
        }

        /// <summary>
        /// Gibt die Eigenschaft Name und das Element in nodes[FactorIDX] als Zeichenkette zurück.
        /// </summary>
        /// <returns>Eine Zeichenkette mit der Eigenschaft Name und dem Element in nodes[FactorIDX].</returns>
        public override string PrintNodes()
        {
            string result = Name + "(";
            if(this.nodes.Count != 0)
            {
                result += this.nodes[FactorIDX].PrintNodes();
            }
            
            return result + ")";
        }

        /// <summary>
        /// Prüft ob das Ojekt an der Stelle nodes[FactorIDX] weitergeschalten werden kann und dekrementiert FactorIDX
        /// gegebenenfalls solange bis es möglich ist.
        /// Wenn zum nächsten Subfaktor weitergeschalten wird, wird dieser aktiviert und die Elemente ab
        /// nodes[FactorIDX + 1] werden deaktiviert.
        /// </summary>
        public override void GetNext()
        {
            //true = weiterschalten zum nächsten alternativen Faktor
            bool flag = (FactorIDX > 0) && !(this.nodes[FactorIDX].HasNext());
            while ((FactorIDX > 0) && !(this.nodes[FactorIDX].HasNext()))
            {
                FactorIDX--;
            }

            //Behebt Ausgabeproblem bei dem erster Wert beim weiterschalten verschluckt wird
            if (!flag)
            {
                this.nodes[FactorIDX].GetNext();
            }
            if (flag)
            {
                this.nodes[FactorIDX].Activate();
                for (int i = FactorIDX + 1; i <= this.nodes.Count - 1; i++)
                {
                    this.nodes[i].Deactivate();
                }
                
            }

        }

        /// <summary>
        /// Gibt an ob es sich um einen parallelen oder alternativen Faktor handelt.
        /// </summary>
        /// <returns>true = parallel; false = alternativ</returns>
        public override bool IsParallel()
        {
            return false;
        }

        /// <summary>
        /// Ruft für alle Elemente in nodes SetInitVal() auf und deaktiviert alle Elemente außer nodes[nodes.Count - 1].
        /// </summary>
        public override void SetInitVal()
        {
            base.SetInitVal();
            for (int i = 0; i < this.nodes.Count - 1; i++)
            {
                this.nodes[i].Deactivate();
            }
        }
    }
}