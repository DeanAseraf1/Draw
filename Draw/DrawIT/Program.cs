using System;
using System.Collections.Generic;
using ValidInput;
using System.Runtime.InteropServices;

namespace Draw
{
    class Program
    {
        //Properties for managing the console window.
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static void Main(string[] args)
        {
            //Maximizimg the window
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);

            Animation anim = null;
            Canvas canvas = null;
            List<Shape> shapes = new List<Shape>();
            BrowsMode();

            //TODO: Creating 2 strings[] for saving the canvases and the shapes.



            void BrowsMode()
            {
                string choise = "";
                while (choise != "q")
                {
                    Console.Clear();
                    Console.WriteLine("\n---Browse Mode---");
                    string question = "c-Create animation, l-Load animation*";
                    if (anim != null)
                    {
                        Console.WriteLine($"Current animation: {anim.Count} frames in total.");
                        question += ", s-Save animation* , i-Edit animation, p-Play animation";
                    }
                    question += ", q-Quit";
                    Console.WriteLine(question);
                    choise = ForceInput.InputStringOptions(new string[] { "c", "s", "l", "i", "p", "q" });
                    switch (choise)
                    {

                        case "s":
                            break;

                        case "l":
                            break;

                        case "p":
                            if (anim != null)
                            {
                                anim.Play();
                            }
                            break;

                        case "c":
                            int w = 0;
                            int h = 0;
                            GetTwoInputs(out w, out h, "wh");
                            anim = new Animation(w, h);
                            canvas = anim.canvas;
                            EditMode();
                            break;

                        case "i":
                            if (anim == null)
                            {
                                break;
                            }
                            EditMode();
                            break;
                    }

                }
            }

            void EditMode()
            {
                Console.Clear();
                Console.WriteLine("\n---Edit Mode---");
                string c = "";
                while (c != "q")
                {
                    Console.WriteLine($"Current animation: {anim.Count} frames in total.");
                    string q = "f-Add frame, e-Add elipse, r-Add rectangle, t-Add triangle, d-Draw current, c-Clear screen";
                    if (shapes.Count > 0)
                    {
                        for (int i = 0; i < shapes.Count; i++)
                        {
                            Console.WriteLine($"{i}. {shapes[i].GetType().Name}");
                        }
                        q += ", index-Edit shape";
                    }
                    if (anim.Count > 0)
                    {
                        q += ", l-Log, p-Play";
                    }
                    q += ", q-Quit edit mode";
                    Console.WriteLine(q);
                    c = Console.ReadLine();
                    int shapeX = 0;
                    int shapeY = 0;
                    int shapeXsize = 0;
                    int shapeYsize = 0;
                    switch (c)
                    {
                        case "f":
                            anim.AddCurrent();
                            Console.WriteLine("Frame added");
                            break;

                        case "e":
                            {
                                GetTwoInputs(out shapeX, out shapeY, "xy");
                                GetTwoInputs(out shapeXsize, out shapeYsize, "xryr");

                                shapes.Add(canvas.DrawEllipse(shapeX, shapeY, shapeXsize, shapeYsize));
                                Console.WriteLine("Ellipse added");
                                break;
                            }

                        case "r":
                            {
                                GetTwoInputs(out shapeX, out shapeY, "xy");
                                GetTwoInputs(out shapeXsize, out shapeYsize, "wh");

                                shapes.Add(canvas.DrawRectangle(shapeX, shapeY, shapeXsize, shapeYsize));
                                Console.WriteLine("Reqtangle added");
                                break;
                            }

                        case "t":
                            GetTwoInputs(out shapeX, out shapeY, "xy");
                            GetTwoInputs(out shapeXsize, out shapeYsize, "hb");

                            shapes.Add(canvas.DrawTriangle(shapeX, shapeY, shapeXsize, shapeYsize));
                            Console.WriteLine("Triangle added");
                            break;

                        case "l":
                            Console.WriteLine("Enter count: ");
                            int pCount = ForceInput.InputInt();

                            Console.WriteLine(anim.GetLast(pCount));
                            break;

                        case "p":
                            Console.WriteLine("Enter count: ");
                            int lCount = ForceInput.InputInt();
                            anim.PlayLast(last: lCount);
                            break;

                        case "d":
                            Console.WriteLine(anim.GetCurrent());
                            break;

                        case "c":
                            Console.Clear();
                            break;

                        case "q":
                            break;



                        default:
                            int index = 0;
                            bool valid = false;
                            valid = int.TryParse(c, out index);
                            if (valid && index >= 0 && index < shapes.Count)
                            {
                                Console.WriteLine("m-Move shape, r-Resize shape, mt-Move shape to, rt-Rotate shape to");
                                Console.WriteLine(shapes[index].GetProps());
                                string ch = ForceInput.InputStringOptions(new string[] { "m", "r" });
                                int x = 0;
                                int y = 0;
                                int w = 0;
                                int h = 0;
                                switch (ch)
                                {
                                    case "mt":
                                        GetTwoInputs(out x, out y, "xy");
                                        shapes[index].MoveTo(x, y);
                                        Console.WriteLine("Shape moved.");
                                        break;

                                    case "rt":
                                        GetTwoInputs(out w, out h, "wh");
                                        shapes[index].ResizeTo(w, h);
                                        Console.WriteLine("Shape resized");
                                        break;

                                    case "m":
                                        GetTwoInputs(out x, out y, "xy");
                                        shapes[index].Move(x, y);
                                        Console.WriteLine("Shape moved.");
                                        break;

                                    case "r":
                                        GetTwoInputs(out w, out h, "wh");
                                        shapes[index].Resize(w, h);
                                        Console.WriteLine("Shape resized");
                                        break;
                                }
                            }
                            break;
                    }
                }
            }

        }
        static void GetTwoInputs(out int num1, out int num2, string xy = "xy")
        {
            num1 = 0;
            num2 = 0;
            switch(xy)
            {
                case "xy":
                    Console.WriteLine("Enter x: ");
                    num1 = ForceInput.InputInt();
                    Console.WriteLine("Enter y");
                    num2 = ForceInput.InputInt();
                    break;

                case "wh":
                    Console.WriteLine("Enter width: ");
                    num1 = ForceInput.InputInt();
                    Console.WriteLine("Enter height");
                    num2 = ForceInput.InputInt();
                    break;

                case "xryr":
                    Console.WriteLine("Enter xRadius: ");
                    num1 = ForceInput.InputInt();
                    Console.WriteLine("Enter yRadius: ");
                    num2 = ForceInput.InputInt();
                    break;

                case "hb":
                    Console.WriteLine("Enter height: ");
                    num1 = ForceInput.InputInt();
                    Console.WriteLine("Enter base: ");
                    num2 = ForceInput.InputInt();
                    break;
            }
        }
    }
}
