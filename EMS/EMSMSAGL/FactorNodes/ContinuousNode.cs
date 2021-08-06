using Microsoft.Msagl.Drawing;

namespace EMS.EMSMSAGL.FactorNodes
{
    class ContinuousNode : LeafNode
    {
        public ContinuousNode(string id) : base(id)
        {
            this.Attr.Color = Color.OrangeRed;
            this.Attr.FillColor = Color.OrangeRed;
        }
    }
}
