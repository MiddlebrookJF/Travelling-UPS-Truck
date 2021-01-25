// ---------------------------------------------------------------------------
// File:		Driver.cs
// Project:		Project 1 - Travelling UPS Truck
// Author:		Jeffrey Richards, richardsjm@etsu.edu
// Course:		CSCI 3230-901, Algorithms
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
			Stopwatch sw = new Stopwatch();                     //creates sw, starts stopwatch
			double d;                                           //stores  min total distance for each iteration

			int inputSize = Int32.Parse(Console.ReadLine());    //how long our input is (first line)
			Point[] inputPoints = new Point[inputSize];			//array of (x,y) points representing our input
			List<int> optimalRoute = new List<int>();			//stores indexes of points in optimal route order


			for (int i = 0; i < inputSize; i++)
			{
				string instring = Console.ReadLine();
				string[] values = instring.Split(' ');

				inputPoints[i].X = Int32.Parse(values[0]);
				inputPoints[i].Y = Int32.Parse(values[1]);
			}

			//d = Math.Sqrt(Math.Pow((x[1] - x[0]), 2) + Math.Pow((y[1] - y[0]), 2));
			////brute force to find longest distance d
			//for (int i = 0; i < inputSize; i++)                 //For every point in our input
			//{
			//	for (int j = i + 1; j < inputSize; ++j)         //Compare to every other point in our input
			//	{
			//		double difference = Math.Sqrt(Math.Pow((x[j] - x[i]), 2) + Math.Pow((y[j] - y[i]), 2));
			//		if (difference < d)     //If the difference between two comparing points is smaller than d, assign to d
			//			d = difference;
			//	}
			//}//end for loops


			sw.Stop();      //stops stopwatch

			Console.WriteLine($"\nShortest route: {d.ToString("N6")}");
			string sOptimalRoute = "";
			foreach (int i in optimalRoute)
				sOptimalRoute += i.ToString() + " ";
			Console.WriteLine($"Optimal route: {sOptimalRoute}");
			Console.WriteLine($"Time elapsed: {sw.Elapsed.TotalMilliseconds.ToString("N")} milliseconds");
			Console.ReadKey();
		}

		/// <summary>
		/// Finds the route.
		/// </summary>
		public static double FindRoute()
		{

			return 0.0;
		}
	}
}
