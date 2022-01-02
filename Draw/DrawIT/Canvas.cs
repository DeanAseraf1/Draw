using System;

namespace Draw
{
    class Canvas
    {
        private int width { get; set; }
        private int height { get; set; }
        private Tile[,] tiles { get; set; }
        private string emptySymbol { get; set; }

        private string rowSeperator { get; set; } = " ";
        private string columnSeperator { get; set; } = "\n";

        public Canvas(int w, int h) : this(width:w, h){}
        public Canvas(int width, int height,
            string emptySymbol = "#",
            string rowSeperator = " ",
            string columnSeperator = "\n")
        {
            this.width = width;
            this.height = height;

            this.emptySymbol = emptySymbol;
            this.rowSeperator = rowSeperator;
            this.columnSeperator = columnSeperator;

            ResetCanvas();
        }

        private void ResetCanvas()
        {
            tiles = new Tile[width, height];
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    tiles[x, y] = new Tile(x, y, emptySymbol);
                }
            }
        }

        public int GetStringLength()
        {
            return tiles.GetLength(1) * tiles.GetLength(0) * (1+rowSeperator.Length) + tiles.GetLength(1) * (columnSeperator.Length);
        }

        public override string ToString()
        {
            string canvas = "";
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    canvas += tiles[x, y].symbol + rowSeperator;
                }
                canvas += columnSeperator;
            }
            return canvas;
        }

        public string GetProps()
        {
            return $"canvas:{{ width:{width}, height:{height}, symbol:{emptySymbol} }}";

        }


        public Tile GetTile(int x, int y)
        {
            if (x < tiles.GetLength(0) && x>=0 && y < tiles.GetLength(1) && y>=0)
            {
                return tiles[x, y];
            }
            //Tile was outside of range.

            return null;

        }

        public void SetTileSymbol(int x, int y, string symbol)
        {
            Tile tile = GetTile(x, y);
            if(tile!=null)
            {
                tile.symbol = symbol;
            }
            
        }

        public void ClearTileSymbol(int x, int y)
        {
            Tile tile = GetTile(x, y);
            if(tile!=null)
            {
                tile.symbol = emptySymbol;
            }
            
        }

        
        //Rectangle
        public SRectangle DrawRectangle(int x, int y, int width, int height, string symbol = " ")
        {
            SRectangle rect = new SRectangle(this, GetTile(x, y), symbol, width, height);
            rect.Draw();
            return rect;
        }
        public SRectangle DrawSquare(int x, int y, int side, string symbol = " ")
        {
            SRectangle rect = new SRectangle(this, GetTile(x, y), symbol, side, side);
            rect.Draw();
            return rect;
        }

        //Triangle
        public STriangle DrawTriangle(int x, int y, int basis, int height, string symbol = " ")
        {
            STriangle tri = new STriangle(this, GetTile(x,y),symbol, basis, height);
            tri.Draw();
            return tri;
        }
        public STriangle DrawBasicTriagle(int x, int y, int height, string symbol = " ")
        {
            STriangle tri = new STriangle(this, GetTile(x, y), symbol, height, height);
            tri.Draw();
            return tri;
        }

        //Circle
        public SEllipse DrawEllipse(int x, int y, int wRadius, int hRadius, string symbol = " ")
        {
            SEllipse circ = new SEllipse(this, GetTile(x, y), symbol, wRadius, hRadius);
            circ.Draw();
            return circ;
        }
        public SEllipse DrawCircle(int x, int y, int radius, string symbol = " ")
        {
            SEllipse circ = new SEllipse(this, GetTile(x, y), symbol, radius, radius);
            circ.Draw();
            return circ;
        }
    }
}
