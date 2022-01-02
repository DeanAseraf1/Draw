using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Draw
{
    class Animation : IEnumerable, IEnumerator
    {
        private List<string> frames;
        public Canvas canvas { get; set; }

        public Animation(int width, int height, string background = "#")
        {
            frames = new List<string>();
            canvas = new Canvas(width, height, background);
        }

        public void AddCurrent()
        {
            frames.Add(canvas.ToString());
        }
        public string GetCurrent()
        {
            return canvas.ToString();
        }
        public string GetLast(int last = 1)
        {
            if(last <= 1)
            {
                return $"frame{Count-1}: \n" + frames[frames.Count - 1];
            }
            string str = "";
            int firstIndex = Count > last ? Count - last : 0;
            for (int i = firstIndex; i < Count; i++)
            {
                str += $"frame{i}: \n{this[i]}";
            }
            return str;
        }

        public void PlayLast(int fps = 2, int last = 10)
        {
            Console.Clear();
            int firstIndex = Count > last ? Count-last : 0;
            for (int i = firstIndex; i < Count; i++)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(frames[i]);
                Thread.Sleep(1000 / fps);
            }
        }

        public void Play(int fps=12)
        {
            PlayLast(fps, Count);
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < this.Count; i++)
            {
                str += $"frame{i}: \n{this[i]}";
            }
            return str;
        }

        #region Indexer & Itturator
        private int counter = -1;
        public string this[int i]
        {
            get
            {
                return frames[i];
            }
        }

        public int Count
        {
            get
            {
                return frames.Count;
            }
        }

        public object Current
        {
            get
            {
                return frames[counter];
            }
        }

        public IEnumerable GetEnumerable()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            counter++;
            if (counter < Count)
            {
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            counter = -1;
        }
        #endregion
    }
}
