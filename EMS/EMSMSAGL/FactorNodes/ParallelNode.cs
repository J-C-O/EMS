using Microsoft.Msagl.Drawing;

namespace EMS.EMSMSAGL.FactorNodes
{
    class ParallelNode : CompositeNode
    {
        public ParallelNode(string id) : base(id)
        {
            this.Attr.Color = Color.GreenYellow;
            this.Attr.FillColor = Color.GreenYellow;
        }
    }
}
