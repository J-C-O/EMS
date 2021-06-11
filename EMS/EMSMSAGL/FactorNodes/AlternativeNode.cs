using Microsoft.Msagl.Drawing;

namespace EMS.EMSMSAGL.FactorNodes
{
    class AlternativeNode : CompositeNode
    {
        public AlternativeNode(string id) : base(id)
        {
            this.Attr.Color = Color.LightSkyBlue;
            this.Attr.FillColor = Color.LightSkyBlue;
        }
    }
}
