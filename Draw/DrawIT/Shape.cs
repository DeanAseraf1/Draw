using System;
using System.Collections.Generic;
using System.Text;

namespace Draw
{
    abstract class Shape
    {
        protected Tile startTile { get; set; }
        private List<Tile> tiles { get; set; }
        private string symbol { get; set; }
        private Canvas canvas { get; set; }

        protected int width { get; set; }
        protected int height { get; set; }


        public Shape(Canvas canvas, Tile startTile, string symbol, int width, int height)
        {
            this.startTile = startTile;
            this.symbol = symbol;

            this.canvas = canvas;

            this.width = width;
            this.height = height;

            tiles = new List<Tile>();
        }

        public string GetProps()
        {
            return $"{GetType().Name}:{{ width:{width}, height:{height} , x:{startTile.x}, y:{startTile.y}, symbol:{symbol} }}";
        }

        public static void MoveShapes(Shape[] shapes, int dx, int dy)
        {
            foreach(Shape shape in shapes)
            {
                shape.Move(dx, dy);
            }
        }
        public static void MoveShapesTo(Shape[] shapes , int x, int y, int inRelTo=0)
        {
            if(inRelTo >= 0)
            {
                int[] xDistnaces = new int[shapes.Length];
                int[] yDistances = new int[shapes.Length];
                for (int i = 0; i < shapes.Length; i++)
                {
                    if(i==inRelTo)
                    {
                        continue;
                    }
                    xDistnaces[i] = shapes[i].startTile.x - shapes[inRelTo].startTile.x;
                    yDistances[i] = shapes[i].startTile.y - shapes[inRelTo].startTile.y;
                }
                
                for (int i = 0; i < shapes.Length; i++)
                {
                    if (i == inRelTo)
                    {
                        shapes[i].MoveTo(x, y);
                    }
                    shapes[i].MoveTo(x+xDistnaces[i],y+ yDistances[i]);
                }
            }
            else
            {
                foreach(Shape shape in shapes)
                {
                    shape.MoveTo(x, y);
                }
            }
        }

        public void MoveTo(int x, int y)
        {
            EmptyAllTiles();
            tiles = new List<Tile>();
            Tile sTile = canvas.GetTile(x, y);
            if(sTile!=null)
            {
                startTile = sTile;
            }
            Draw();
            //TODO: Setting the closest tile if outside of border?
        }
        public void Move(int dx, int dy)
        {
            MoveTo(startTile.x + dx, startTile.y + dy);
        }

        public void ResizeTo(int x, int y)
        {
            EmptyAllTiles();
            tiles = new List<Tile>();
            width = x;
            height = y;
            Draw();
        }
        public void ResizeTo(int size)
        {
            ResizeTo(size, size);
        }
        public void Resize(int dx, int dy)
        {
            ResizeTo(width + dx, height + dy);
        }
        public void Resize(int d)
        {
            ResizeTo(width + d, height + d);
        }

        public void Delete()
        {
            EmptyAllTiles();
        }

        public void Paint(string symbol)
        {
            EmptyAllTiles();
            this.symbol = symbol;
            Draw();
        }

        public abstract void Draw();

        private void EmptyAllTiles()
        {
            foreach (Tile tile in tiles)
            {
                canvas.ClearTileSymbol(tile.x, tile.y);
            }
        }

        protected void AddTile(int x, int y)
        {
            canvas.SetTileSymbol(x, y, symbol);
            Tile tile = canvas.GetTile(x, y);
            if(tile!=null && !tiles.Contains(tile))
            {
                tiles.Add(tile);
            }
            
        }
    }
}
