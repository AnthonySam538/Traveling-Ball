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
  private const short formHeight = 900;
  private const short formWidth = formHeight * 16/9;
  private const short radius = 13;
  private const short distance = 1; //the amount of pixels that the ball travels per tick
  private const double animationRate = 1000/60;
  private const double refreshRate = 1000/30;

  // Create points
  private Point upperRight;
  private Point upperLeft;
  private Point bottomLeft;
  private Point bottomRight;
  private Point ball;

  // Create Controls
  private Label title = new Label();
  private Label x = new Label();
  private Label y = new Label();
  private Label d = new Label();
  private TextBox xCoordinateDisplay = new TextBox();
  private TextBox yCoordinateDisplay = new TextBox();
  private TextBox direction = new TextBox();
  private Button startButton = new Button();
  private Button exitButton = new Button();

  // Create Timers
  private static System.Timers.Timer animationClock = new System.Timers.Timer(animationRate);
  private static System.Timers.Timer refreshClock = new System.Timers.Timer(refreshRate);

  public TravelingBallForm() //The constructor of this class
  {
    // Set up the texts
    Text = "Traveling Ball";
    title.Text = "Traveling Ball by Anthony Sam";
    title.TextAlign = ContentAlignment.MiddleCenter;
    startButton.Text = "Go";
    exitButton.Text = "Exit";
    x.Text = "X";
    y.Text = "Y";
    d.Text = "Direction";

    // Set up sizes
    Size = new Size(formWidth, formHeight);
    title.Size = new Size(formWidth, formHeight/10);
    startButton.Size = new Size(formWidth/3, Height/10);
    exitButton.Size = startButton.Size;
    x.AutoSize = true;
    y.AutoSize = true;
    d.AutoSize = true;
    xCoordinateDisplay.Size = new Size(d.Width, d.Height);
    yCoordinateDisplay.Size = xCoordinateDisplay.Size;
    direction.Size = xCoordinateDisplay.Size;

    // Set up locations
    y.Location = new Point(formWidth/6, formHeight*9/10 + y.Height);
    yCoordinateDisplay.Location = new Point(y.Right, y.Top);
    xCoordinateDisplay.Location = new Point(y.Left-xCoordinateDisplay.Width, y.Top);
    x.Location = new Point(xCoordinateDisplay.Left-x.Width, y.Top);
    d.Location = new Point(x.Right, x.Bottom);
    direction.Location = new Point(d.Right, x.Bottom);
    startButton.Location = new Point(formWidth/3, formHeight * 9/10);
    exitButton.Location = new Point(startButton.Right, startButton.Top);
    ball = upperRight = new Point(radius*4 + formWidth-8*radius, formHeight/10 + radius*4);
    upperLeft = new Point(radius*4, upperRight.Y);
    bottomLeft = new Point(upperLeft.X, upperRight.Y + formHeight*8/10-8*radius);
    bottomRight = new Point(upperRight.X, bottomLeft.Y);

    // Set up colors
    title.BackColor = Color.Cyan;
    x.BackColor = Color.Transparent;
    y.BackColor = x.BackColor;
    d.BackColor = x.BackColor;
    startButton.BackColor = Color.Magenta;
    exitButton.BackColor = startButton.BackColor;

    // Prevent the user from typing into the TextBoxes
    xCoordinateDisplay.Enabled = false;
    yCoordinateDisplay.Enabled = false;
    direction.Enabled = false;

    // Add the controls to the form
    Controls.Add(title);
    Controls.Add(x);
    Controls.Add(y);
    Controls.Add(d);
    Controls.Add(xCoordinateDisplay);
    Controls.Add(yCoordinateDisplay);
    Controls.Add(direction);
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

    graphics.FillRectangle(Brushes.LawnGreen, 0, formHeight * 9/10, formWidth, formHeight/10);

    graphics.DrawRectangle(Pens.Black, upperLeft.X, upperLeft.Y, upperRight.X-upperLeft.X, bottomLeft.Y-upperLeft.Y);
    graphics.FillEllipse(Brushes.DarkOrchid, ball.X-radius, ball.Y-radius, radius*2, radius*2);

    base.OnPaint(e);
  }

  protected void updateBall(Object sender, ElapsedEventArgs evt){}

  protected void refresh(Object sender, ElapsedEventArgs evt){}

  protected void start(Object sender, EventArgs events)
  {
    startButton.Text = "Pause";
    // startButton.Text = "Resume";
  }

  protected void exit(Object sender, EventArgs events)
  {
    System.Console.WriteLine("You clicked on the Exit button.");
    Close();
  }
}
