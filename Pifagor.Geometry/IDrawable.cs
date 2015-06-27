using System.Drawing;

namespace Pifagor.Geometry
{
    public interface IDrawable
    {
        void Draw(Graphics g, Pen pen);
    }
}