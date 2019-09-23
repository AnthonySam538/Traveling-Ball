// Author: Anthony Sam
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #3
// Program Name: Traveling Ball

//Name of this file: TravelingBallMain.cs
//Purpose of this file: Launch the window showing the form where the traveling ball will be displayed.
//Purpose of this entire program: This program shows a ball traveling around the screen. This program contains two clocks.

//Source files in this program: TravelingBallForm.cs, TravelingBallMain.cs
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. TravelingBallForm.cs compiles into library file TravelingBallForm.dll
//  2. TravelingBallMain.cs compiles and links with the dll file above to create TravelingBall.exe
//Compile (and link) this file:
//mcs -r:System.Windows.Forms.dll -r:TravelingBallForm.dll -out:TravelingBall.exe TravelingBallMain.cs
//Execute (Linux shell): ./TravelingBall.exe

using System;
using System.Windows.Forms;  //Needed for "Application.Run" near the end of Main function.

public class TravelingBallMain
{
  public static void Main()
  {
    System.Console.WriteLine("The traveling ball program will begin now.");
    TravelingBallForm TravelingBall_App = new TravelingBallForm();
    Application.Run(TravelingBall_App);
    System.Console.WriteLine("The traveling ball program has ended.");
  }
}
