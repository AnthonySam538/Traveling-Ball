// Author: Anthony Sam
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #3
// Program Name: Traveling Ball

//Name of this file: TravelingBallForm.cs
//Purpose of this file: Define the layout of the user interface (UI) window.
//Purpose of this entire program: This program shows a ball traveling around the screen. This program contains two clocks.

//Source files in this program: TravelingBallForm.cs, TravelingBallMain.cs
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. TravelingBallForm.cs compiles into library file TravelingBallForm.dll
//  2. TravelingBallMain.cs compiles and links with the dll file above to create TravelingBall.exe
//Compile this file:
//mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:TravelingBallForm.dll TravelingBallForm.cs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

public class TravelingBallForm : Form
{
  private const short radius = 13;
  private const short distance = 1; //the amount of pixels that the ball travels per tick
  private const double animationRate = 1000/60;
  private const double refreshRate = 1000/30;

  // Create the Rectangles
  private static Rectangle path;
  private static Rectangle ball;

  // Create Controls
  private Button startButton = new Button();
  private Button exitButton = new Button();
  private Label info = new Label();

  // Create Timers
  private static System.Timers.Timer animationClock = new System.Timers.Timer(animationRate);
  private static System.Timers.Timer refreshClock = new System.Timers.Timer(refreshRate);

  public TravelingBallForm() //The constructor of this class
  {
    // Set up the texts
    Text = "Traveling Ball";
    startButton.Text = "Go";
    exitButton.Text = "Exit";
    info.Text = "X: Y: Direction: Left";

    // Set up sizes
    Height = 900;
    Width = Height * 16/9;
    info.Size = new Size(Width, Height/10);

    // Set up locations
    startButton.Location = new Point(Width/2, Height/2);
    exitButton.Location = new Point(startButton.Right, startButton.Bottom);
    exitButton.Bottom = Height;

    // Add the controls to the form
    Controls.Add(info);
    Controls.Add(startButton);
    Controls.Add(exitButton);

    // Tell the events which method to call (Each method is defined below)
    animationClock.Elapsed += new ElapsedEventHandler(updateBall);
    refreshClock.Elapsed += new ElapsedEventHandler(refresh);
    startButton.Click += new EventHandler(start);
    exitButton.Click += new EventHandler(exit);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;

    graphics.DrawRectangle(Pens.Black, 200, 100, Width * 4/5, Height * 4/5);
    graphics.FillEllipse(Brushes.Purple, 500, 500, radius*2, radius*2);

    base.OnPaint(e);
  }

  protected void updateBall(Object sender, ElapsedEventArgs evt){}

  protected void refresh(Object sender, ElapsedEventArgs evt){}

  protected void start(Object sender, EventArgs events)
  {
    startButton.Text = "Pause";
  }

  protected void exit(Object sender, EventArgs events)
  {
    System.Console.WriteLine("You clicked on the Exit button.");
    Close();
  }
}
