using System;
using System.Collections.Generic;
using System.Text;

namespace Draw
{
    class STriangle:Shape
    {
        public STriangle(Canvas canvas, Tile startTile, string symbol, int basis, int height) :
            base(canvas, startTile, symbol, basis, height)
        {

        }
        public override void Draw()
        {
            double stair = 0;

            int offset = height % 2;
            for (int h = -height / 2; h <= height / 2 + offset; h++)
            {
                for (int w = (int)-stair; w < stair + offset; w++)
                {
                    int newW = startTile.x + w;
                    int newH = startTile.y + h;

                    AddTile(newW, newH);
                }
                stair += (double)(width) / (double)(height);
            }
        }
    }
}
