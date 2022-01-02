using System;

namespace ValidInput
{
    public static class ForceInput
    {
        public static string InputStringOptions(string[] options,
            string messege = "Error! Please enter a string within options.")
        {
            string choise = "";
            bool valid = false;
            while (!valid)
            {
                choise = Console.ReadLine();
                valid = Array.IndexOf(options, choise) >= 0 ? true : false;
                if (!valid && messege != "")
                {
                    Console.WriteLine(messege);
                }
            }
            return choise;
        }

        public static int InputInt(string messege = "Error! Please enter an integer number.")
        {
            int choise = 0;
            bool valid = false;
            while (!valid)
            {
                valid = int.TryParse(Console.ReadLine(), out choise);
                if (!valid && messege != "")
                {
                    Console.WriteLine(messege);
                }
            }
            return choise;
        }

        public static int InputIntInRange(int min, int max,
            string messege = "Error! Please enter an integer number within range.")
        {
            int choise = 0;
            bool valid = false;
            while (!valid)
            {
                choise = InputInt(messege);
                valid = (choise >=min && choise<=max)?true:false;
                if (!valid && messege != "")
                {
                    Console.WriteLine(messege);
                }
            }
            return choise;
        }

        public static DateTime InputDate(string messege = "Error! please enter a valid date.")
        {
            bool valid = false;
            DateTime selectDT = new DateTime();
            while (!valid)
            {
                string choise = Console.ReadLine();
                char[] seps = new char[] { '.', '-', '/' };
                string[] dateArr = choise.Split(seps);

                int day;
                int month;
                int year;
                valid = int.TryParse(dateArr[0], out day);
                valid = int.TryParse(dateArr[1], out month);
                valid = int.TryParse(dateArr[2], out year);
                if ((day >= 1 && day <= 31) && (month >= 1 && month <= 12) && (year >= 1 && year <= 9999) && valid)
                {
                    valid = true;
                    selectDT = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[1]), int.Parse(dateArr[0]));
                }
                if (!valid && messege != "")
                {
                    Console.WriteLine(messege);
                }
            }
            return selectDT;
        }

        public static bool InputBool(string positive = "Y", string negative = "N",
            string messege = "Error! Please enter a boolean string.")
        {
            string choise;
            bool valid = false;
            bool booleanChoise = false;
            while (!valid)
            {
                choise = Console.ReadLine();
                if (choise == positive)
                {
                    booleanChoise = true;
                    valid = true;
                }
                else if (choise == negative)
                {
                    booleanChoise = false;
                    valid = true;
                }

                if (!valid && messege != "")
                {
                    Console.WriteLine(messege);
                }
            }
            return booleanChoise;
        }

        public static int[] InputIntArray(int size, string messege = "Error! Please enter an integer number.")
        {
            int[] arr = new int[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = InputInt(messege);
            }
            return arr;
        }

        public static int[] InputIntArrayInRange(int size, int min, int max,
            string messege = "Error! Please enter an integer number within range.")
        {
            int[] arr = new int[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = InputIntInRange(min, max, messege);
            }
            return arr;
        }

        public static string[] InputStringArray(int size)
        {
            string[] arr = new string[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Console.ReadLine();
            }
            return arr;
        }

        public static string[] InputStringArrayInOptions(int size, string[] options,
            string messege = "Error! Please enter a string within options.")
        {
            string[] arr = new string[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = InputStringOptions(options, messege);
            }
            return arr;
        }
    }
}
