using System.Drawing;

namespace LabsLibrary
{
	public static class Lab3
	{
		public static List<int> X = new List<int>(), Y = new List<int>();
		public class rect
		{
			public int x0, y0;
			public int x1, y1;
			public rect(int x0, int y0, int x1, int y1)
			{
				this.x0 = x0 > x1 ? x1 : x0;
				this.y0 = y0 > y1 ? y1 : y0;
				this.x1 = x0 < x1 ? x1 : x0;
				this.y1 = y0 < y1 ? y1 : y0;
				X.Add(x0);
				X.Add(x1);
				Y.Add(y0);
				Y.Add(y1);
			}
			bool isInsideX(int x)
			{
				return x0 <= x && x <= x1;
			}
			bool isInsideY(int y)
			{
				return y0 <= y && y <= y1;
			}
			public bool isInclude(rect R)
			{
				return isInsideX(R.x0) && isInsideX(R.x1) && isInsideY(R.y0) && isInsideY(R.y1);
			}
		}

		public static List<rect> mas = new List<rect>();
		public static int N, M;
		public static List<List<bool>> used = new List<List<bool>>();
		public static List<List<List<int>>> adj = new List<List<List<int>>>();

		public static string Run(string inputFile = "INPUT.TXT")
		{
			var inputData = File.ReadLines(inputFile);
			var n = Convert.ToInt32(inputData.First().Trim());
			mas = inputData.Skip(1).Select(row =>
			{
				var numList = row.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToList();
				return new rect(numList[0], numList[1], numList[2], numList[3]);
			}).ToList();

			if (n < 1 || n > 100)
			{
				return "Out of range exception!";
			}
			else
			{
				return solve().ToString();
			}
		}

		private static int solve()
		{
			X = X.Distinct().OrderBy(x => x).ToList();
			Y = Y.Distinct().OrderBy(x => x).ToList();
			N = X.Count - 1;
			M = Y.Count - 1;
			for (int i = 0; i < N; i++)
			{
				used.Add(new List<bool>());
				for (int j = 0; j < N; j++)
				{
					used[i].Add(false);
				}
				adj.Add(new List<List<int>>());
				for (int k = 0; k < M; k++)
				{
					adj[i].Add(new List<int>());
				}
			}
			GenAdj();
			return search();
		}

		private static int search()
		{
			int areaAmount = 1;
			for (int i = 0; i < adj.Count; i++)
			{
				for (int j = 0; j < adj[i].Count; j++)
				{
					if (!used[i][j])
					{
						used[i][j] = true;
						bfs(i, j, ref areaAmount);
					}
				}
			}
			return areaAmount;
		}

		public static int[] moveX = new int[] { -1, 0, 1, 0 };
		public static int[] moveY = new int[] { 0, -1, 0, 1 };

		private static void bfs(int x, int y, ref int areaAmount)
		{
			if (adj[x][y].Any())
			{
				areaAmount++;
			}
			Queue<Point> q = new Queue<Point>();
			q.Enqueue(new Point(x, y));
			while (q.Any())
			{
				Point cur = q.Dequeue();
				for (int i = 0; i < 4; i++)
				{
					int nX = cur.X + moveX[i];
					int nY = cur.Y + moveY[i];
					if (correct(nX, nY) && !used[nX][nY])
					{
						bool eq = false;
						if (adj[x][y].Count == adj[nX][nY].Count)
						{
							eq = true;
							for (int j = 0; j < adj[x][y].Count; j++)
							{
								if (adj[x][y][j] != adj[nX][nY][j])
								{
									eq = false;
									break;
								}
							}
						}
						if (eq)
						{
							used[nX][nY] = true;
							q.Enqueue(new Point(nX, nY));
						}
					}
				}
			}
		}

		private static bool correct(int x, int y)
		{
			if (x < 0 || y < 0)
			{
				return false;
			}
			if (x == N || y == M)
			{
				return false;
			}
			return true;
		}

		private static void GenAdj()
		{
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					rect curRect = new rect(X[i], Y[j], X[i + 1], Y[j + 1]);
					for (int k = 0; k < mas.Count; k++)
					{
						if (mas[k].isInclude(curRect))
						{
							adj[i][j].Add(k);
						}
					}
				}
			}
		}
	}
}