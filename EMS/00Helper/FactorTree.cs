using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EMS.EMSFactorClasses;

namespace EMS._00Helper
{
    public class FactorTree
    {
        public Factor Tree { get; set; }

        public FactorTree()
        {
            Tree = new FactorParallel();
            //Tree.Name = "Root";

            FactorParallel para1_1 = new FactorParallel("Parallel 1.1");
            para1_1.AddNode(new ArrayValue<int>("ArrayValue-int", new int[] { 1, 3, 56, 20 }));
            para1_1.AddNode(new ArrayValue<string>("ArrayValue-string", new string[] { "Wert 1", "Wert 2" }));

            FactorParallel para1_2 = new FactorParallel("Parallel 1.2");
            para1_2.AddNode(new Intervall("Intervall", 0.5, 1.5, 0.25));
            para1_2.AddNode(new ArrayValue<string>("ArrayValue-string", new string[] { "Einzelwert" }));

            Tree.AddNode(para1_1);
            Tree.AddNode(para1_2);
        }
    }
}
