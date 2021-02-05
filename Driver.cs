// ---------------------------------------------------------------------------
// File:		Driver.cs
// Project:		Project 1 - Travelling UPS Truck
// Author:		Jeffrey Richards, richardsjm@etsu.edu
// Course:		CSCI 3230-901 Algorithms
// Creation:	01/22/2021
// ---------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project1
{
	/// <summary>
	/// Driver class which Brute Forces the Travelling Salesman problem from text input.
	/// </summary>
	class Driver
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{

			Console.WriteLine("Please input the points preceded by a line containing the number of points:");

			int inputSize = Int32.Parse(Console.ReadLine());    //how long our input is (first line)
			Stopwatch sw = new Stopwatch();                     //creates sw, starts stopwatch after first line is read

			Point[] inputPoints = new Point[inputSize];         //array of (x,y) points representing our input
			int[] optimalRoute = new int[inputSize];			//stores indexes of points in optimal route order
			int optimalDistance;                                //stores a running optimal route distance


			for (int i = 0; i < inputSize; i++)
			{
				string instring = Console.ReadLine();
				string[] values = instring.Split(' ');

				inputPoints[i].X = Int32.Parse(values[0]);
				inputPoints[i].Y = Int32.Parse(values[1]);
			}

			//Find distances between each point and put in a 2d array (table)
			//eg. distances[3,7] is the distance from point 3 to point 7 and is equal to distances[7,3]
			for (int i = 0; i < inputSize; i++)
			{

			}

			int numOfRoutes = FindFactorial(inputSize);
			int numOfPermutations = 0;							//should end up being equal to numOfRoutes;
			int[] currentPermutation = new int[inputSize];		//Find first permutation to use NextPerm on it
			for (int i = 0; i < inputSize; i++)
				currentPermutation[i] = i;

			//Generate permutations using a method that permutes based on int values
			for (int i = 0; i < numOfRoutes; i++)
			{
				currentPermutation = NextPermutation(currentPermutation);

				//Instead of writeline, calculate distance for the permutation, then compare to optimalDistance
				string sRoute = "";
				for (int j = 0; j < inputSize; j++)
				{
					sRoute += currentPermutation[j];
				}
				Console.WriteLine(sRoute);
				//Console.ReadKey();
				numOfPermutations++;
			}

			sw.Stop();      //stops stopwatch

			Console.ReadLine();
			Console.WriteLine($"Number of routes: {numOfRoutes}\nNumber of permutations: {numOfPermutations}");
			//Console.WriteLine($"\nShortest route: {d.ToString("N6")}");
			//string sOptimalRoute = "";
			//foreach (int i in optimalRoute)
				//sOptimalRoute += i.ToString() + " ";
			//Console.WriteLine($"Optimal route: {sOptimalRoute}");
			//Console.WriteLine($"Time elapsed: {sw.Elapsed.TotalMilliseconds.ToString("N")} milliseconds");
			Console.ReadKey();
		}


		/// <summary>
		/// Takes a specific route and permutes it to the next iteration
		/// Method doesn't need a cutoff at the last permutation because it should be called in a for loop
		/// </summary>
		/// <param name="route">The route permutation as an array</param>
		public static int[] NextPermutation(int[] route)
		{
			int length = route.Length;
			int numNeedsToChange = route[length - 1];
			for (int i = length - 2; i >= 0; i--)
			{
				if (route[i] < route[i + 1])		//once we find the number that is smaller than the number to the right of it:
				{
					//OLD VERSION
					//numNeedsToChange = i;
					//for (int j = numNeedsToChange + 1; j <= length - 1; j++) //Find the number just larger than numNeedsToChange
					//{
					//	if (route[numNeedsToChange] < route[j])	//Swap numNeedsToChange with the number just larger than it
					//	{
					//		int temp = route[numNeedsToChange];
					//		route[numNeedsToChange] = route[j];
					//		route[j] = temp;
					//		break;
					//	}
					//}//end swapping for loop
					//break;

					//NEW VERSION
					numNeedsToChange = i;
					for (int j = length - 1; j > numNeedsToChange; j--) //Find the number just larger than numNeedsToChange
					{
						if (route[numNeedsToChange] < route[j]) //Swap numNeedsToChange with the number just larger than it
						{
							Swap(route, numNeedsToChange, route[j]);
							break;
						}
					}//end swapping for loop
					break;
				}
			}//end for loop which scans in reverse for numNeedsToChange

			QuickMedSort(route, numNeedsToChange + 1, route.Length - 1);
			return route;
		}//end NextPermutation


		/// <summary>
		/// Sorting algorithm which counts the occurences for each value to recreate the list
		/// </summary>
		/// <param name="list">The list to be sorted, containing only non-negative integers.</param>
		private static int[] CountingSort(int[] list, int startingIndex)
		{
			int[] newList = new int[list.Length];
			Array.Copy(list, newList, 10);

			int max = list.Max();
			int[] counts = new int[max + 1];

			for (int i = startingIndex; i <= max; i++)
				counts[i] = 0;                       //set all values for counts[] to 0

			for (int j = startingIndex; j < list.Length; j++)
				counts[list[j]]++;                  //put the counts of occurrences into counts[]

			for (int j = startingIndex; j < max; j++)
				counts[j] += counts[j - 1];         //add sum of previous items to each item

			for (int j = startingIndex; j < newList.Length; j++) //Place the items in the correct place
			{
				newList[counts[list[j]] + startingIndex - 1] = list[j];
				counts[list[j]]--;                  //After putting an item in the list, reduce its value's count
			}

			return newList;
		}//end CountingSort

		/// <summary>
		/// Calculates and returns distance between two points using Pythagorean Theorem.
		/// </summary>
		/// <param name="a">The first point.</param>
		/// <param name="b">The second point.</param>
		public static double DistanceFrom(Point a, Point b)
		{
			return Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
		}

		/// <summary>
		/// Calculates given factorial.
		/// </summary>
		/// <param name="n">The number to use to calculate the factorial.</param>
		public static int FindFactorial(int n)
		{
			int factorial = 1;
			for (int i = 1; i <= n; i++)
				factorial *= i;
			return factorial;
		}

		/// <summary>
		/// A variation of Quick Sort that chooses a pivot based on the median of the first, middle, and last numbers. Cutoff of 10
		/// </summary>
		/// <param name="list">The list to be sorted.</param>
		/// <param name="start">The beginning of the partition to be sorted</param>
		/// <param name="end">The last index of the range to be sorted</param>
		private static void QuickMedSort(int[] list, int start, int end)
		{
			const int cutoff = 10;

			if (start + cutoff > end)
				InsertionForQuicksort(list, start, end);
			else
			{
				int middle = (start + end) / 2;
				if (list[middle] < list[start])
					Swap(list, start, middle);
				if (list[end] < list[start])
					Swap(list, start, end);
				if (list[end] < list[middle])
					Swap(list, middle, end);

				//Put the pivot at index (end - 1) because we assured list[middle] < list[end]
				int pivot = list[middle];
				Swap(list, middle, end - 1);

				//Partition
				int leftIndex, rightIndex;

				for (leftIndex = start, rightIndex = end - 1; ;)
				{
					while (list[++leftIndex] < pivot)
						;
					while (pivot < list[--rightIndex])
						;
					if (leftIndex < rightIndex)
						Swap(list, leftIndex, rightIndex);
					else
						break;
				}
				Swap(list, leftIndex, end - 1);             //Restore pivot to the correct location

				QuickMedSort(list, start, leftIndex - 1);
				QuickMedSort(list, leftIndex + 1, end);
			}
		}

		/// <summary>
		/// A variation of the insertion sort algorithm that only sorts a specified range in a list
		/// </summary>
		/// <param name="list">The list to be sorted.</param>
		/// <param name="start">The start of the range of elements to be sorted.</param>
		/// <param name="end">The end of the range, ie the index of the last element to be sorted.</param>
		private static void InsertionForQuicksort(int[] list, int start, int end)
		{
			int temp, j;
			for (int i = start + 1; i <= end; i++)
			{
				temp = list[i];                                 //holds the value being inserted

				for (j = i; j > start && temp < list[j - 1]; j--)   //Finds where to insert element
				{
					list[j] = list[j - 1];                      //Moves the other elements to make room
				}

				list[j] = temp;                                 //Inserts value
			}
		}

		/// <summary>
		/// Swaps two elements in a specified list.
		/// </summary>
		/// <param name="list">The list containing the elements.</param>
		/// <param name="x">First value to be swapped.</param>
		/// <param name="y">Second value to be swapped.</param>
		private static void Swap(int[] list, int x, int y)
		{
			int temp = list[x];
			list[x] = list[y];
			list[y] = temp;
		}
	}
}
