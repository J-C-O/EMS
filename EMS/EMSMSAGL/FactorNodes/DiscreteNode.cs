using Microsoft.Msagl.Drawing;

namespace EMS.EMSMSAGL.FactorNodes
{
    class DiscreteNode : LeafNode
    {
        public DiscreteNode(string id) : base(id)
        {
            this.Attr.Color = Color.LightGoldenrodYellow;
            this.Attr.FillColor = Color.LightGoldenrodYellow;
        }
    }
}
