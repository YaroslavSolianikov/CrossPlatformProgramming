namespace Lab2_76
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";
        private static int[] days = new int[] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            var inputDate = File.ReadLines(InputFilePath).First().Trim().Split(' ', StringSplitOptions.TrimEntries).Select(n => Convert.ToInt32(n)).ToList();
            
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                if (!isCorrectDate(inputDate) || inputDate.SequenceEqual(new List<int>() { 31, 12}))
                {
                    streamWriter.WriteLine("Date is incorrect!");
                }
                else
                {
                    streamWriter.WriteLine(GetWinner(inputDate));
                }
            }
        }

        /// <summary>
        /// Знайти номер гравця, що переможе
        /// </summary>
        /// <param name="beginDate">Задана дата початку</param>
        public static int GetWinner(List<int> beginDate)
        {
            bool[,] isWin = new bool[32, 13];
            isWin[31, 12] = true;
            List<int> curDate = new List<int>() { 31, 12 };

            do
            {
                DecDay(ref curDate);
                bool isCurWin = false;
                for (int i = 0; i < 2; i++)
                {
                    List<int> nextDate = new List<int>(curDate);
                    for (int j = 1; j <= 2; j++)
                    {
                        nextDate[i] = curDate[i] + j;
                        if (isCorrectDate(nextDate))
                        {
                            if (!isWin[nextDate[0], nextDate[1]])
                            {
                                isCurWin = true;
                            }
                        }
                    }
                }
                isWin[curDate[0], curDate[1]] = isCurWin;
            } while (!beginDate.SequenceEqual(curDate));

            return isWin[beginDate[0], beginDate[1]] ? 1 : 2;
        }

        static bool isCorrectDate(List<int> date)
        {
            return date[1] < 13 && days[date[1]] >= date[0];
        }

        static void DecDay(ref List<int> cur)
        {
            cur[0]--;
            if(cur[0] == 0)
            {
                cur[1]--;
                cur[0] = days[cur[1]];
            }
        }
    }
}
