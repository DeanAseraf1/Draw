using System;
using System.Collections.Generic;
using System.Text;

namespace Draw
{
    class SRectangle:Shape
    {
        public SRectangle(Canvas canvas, Tile startTile, string symbol, int width, int height) :
            base(canvas ,startTile, symbol, width, height)
        {
        }
        public override void Draw()
        {
            int hOffset = height % 2;
            int wOffset = width % 2;

            for (int h = -height / 2; h < height / 2 + hOffset; h++)
            {
                for (int w = -width / 2; w < width / 2 + wOffset; w++)
                {
                    int newW = startTile.x + w;
                    int newH = startTile.y + h;
                    AddTile(newW, newH);
                }
            }
        }
    }
}
