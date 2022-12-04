using System;
using System.Collections.Generic;
using System.Linq;

namespace LabsLibrary
{
	public class Lab1Runner
	{
		public Lab1Runner(string firstLine, string secondLine)
		{
			_firstLine = firstLine;
			_secondLine = secondLine;
		}
		private readonly string _firstLine;
		private readonly string _secondLine;

		public string RunLab()
		{
			var inputData = new List<string>() { _firstLine, _secondLine};
			var resultCapacity = Convert.ToDecimal(inputData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1].Replace('.', ','));
			var capacitorList = inputData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(cp => Convert.ToInt32(cp)).ToList();
			return IsRealToCreateScheme(resultCapacity, capacitorList) ? "YES" : "NO";
		}

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
					if (IsRealToCreateScheme(resultCapacity, buf, currentCapacity == 0 ? notUsedCpacitorList[i] : (notUsedCpacitorList[i] * currentCapacity) / (notUsedCpacitorList[i] + currentCapacity)))
					{
						return true;
					}
				}
				return false;
			}
		}
	}
}