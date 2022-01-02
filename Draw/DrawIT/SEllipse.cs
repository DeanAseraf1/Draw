using System;
using System.Collections.Generic;
using System.Text;

namespace Draw
{
    class SEllipse:Shape
    {
        public SEllipse(Canvas canvas, Tile startTile, string symbol, int wRadius, int hRadius) :
            base(canvas, startTile, symbol, wRadius, hRadius)
        {
        }
        public override void Draw()
        {
            for (int h = -height; h <= height; h++)
            {
                for (int w = -width; w <= width; w++)
                {
                    int newW = startTile.x + w;
                    int newH = startTile.y + h;

                    int value = height * height * w * w + width * width * h * h;
                    if (value <= height * height * width * width)
                    {
                        AddTile(newW, newH);
                    }

                }
            }
        }
    }
}
