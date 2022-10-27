namespace Lab1_76
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            var inputData = File.ReadLines(InputFilePath).ToList();
            var resultCapacity = Convert.ToDecimal(inputData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1].Replace('.', ','));
            var capacitorList = inputData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(cp => Convert.ToInt32(cp)).ToList();
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                streamWriter.WriteLine(IsRealToCreateScheme(resultCapacity, capacitorList) ? "YES" : "NO");
            }
        }

        /// <summary>
        /// Отримати результат, чи можна скласти потрібну схему з наявних конденсаторів
        /// </summary>
        /// <param name="resultCapacity">Бажана ємність</param>
        /// <param name="notUsedCpacitorList">Список конденсаторів</param>
        public static bool IsRealToCreateScheme(decimal resultCapacity, List<int> notUsedCpacitorList, decimal currentCapacity = 0)
        {
            if (Math.Abs(resultCapacity - currentCapacity) <= 0.1m)
            {
                return true;
            }
            else if (!notUsedCpacitorList.Any())
            {
                return false;
            }
            else
            {
                for (int i = 0; i < notUsedCpacitorList.Count; i++)
                {
                    List<int> buf = new List<int>(notUsedCpacitorList);
                    buf.RemoveAt(i);
                    if (IsRealToCreateScheme(resultCapacity, buf, notUsedCpacitorList[i] + currentCapacity))
                    {
                        return true;
                    }
                    if (IsRealToCreateScheme(resultCapacity, buf, currentCapacity == 0 ? notUsedCpacitorList[i] : (notUsedCpacitorList[i] * currentCapacity) / (notUsedCpacitorList[i] + currentCapacity) ))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}