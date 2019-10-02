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
  private const short distance = 2; //the amount of pixels that the ball travels per tick
  private const double animationRate = 1000/60; //60 updates per second (should be at least 60)
  private const double refreshRate = 1000/60;   //60 frames per second (should be at least 60)

  private short direction = 0; // 0=left | 1=down | 2=right | 3=up

  private SolidBrush ballBrush = new SolidBrush(Color.Magenta);

  // Create points
  private Point upperRight;
  private Point upperLeft;
  private Point bottomLeft;
  private Point bottomRight;
  private Point ball;

  // Create Controls
  private Label title = new Label();
  private Label ballInfo = new Label();
  private Button startButton = new Button();
  private Button resetButton = new Button();
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
    resetButton.Text = "Reset";
    exitButton.Text = "Exit";
    ballInfo.Text = "X:\nY:\nDirection:";
    ballInfo.TextAlign = ContentAlignment.MiddleCenter;

    // Set up sizes
    Size = new Size(formWidth, formHeight);
    title.Size = new Size(formWidth, formHeight/10);
    ballInfo.Size = new Size(formWidth/4, title.Height);

    // Set up locations
    ballInfo.Location = new Point(0, formHeight*9/10);
    startButton.Location = new Point(formWidth/4, formHeight*19/20-startButton.Height/2);
    resetButton.Location = new Point(formWidth/2, startButton.Top);
    exitButton.Location = new Point(formWidth*3/4, startButton.Top);
    upperRight = ball = new Point(radius*4 + formWidth-8*radius - radius, formHeight/10 + radius*4 - radius);
    upperLeft = new Point(radius*4 - radius, upperRight.Y);
    bottomLeft = new Point(upperLeft.X, upperRight.Y + formHeight*8/10 - 8*radius - radius);
    bottomRight = new Point(upperRight.X, bottomLeft.Y);

    // Set up colors
    BackColor = Color.Orange;
    title.BackColor = Color.Cyan;
    ballInfo.BackColor = Color.Transparent;
    startButton.BackColor = Color.DarkOrchid;
    resetButton.BackColor = startButton.BackColor;
    exitButton.BackColor = startButton.BackColor;

    // Add the controls to the form
    Controls.Add(title);
    Controls.Add(ballInfo);
    Controls.Add(startButton);
    Controls.Add(resetButton);
    Controls.Add(exitButton);

    // Tell the events which method to call (Each method is defined below)
    animationClock.Elapsed += new ElapsedEventHandler(updateBall);
    refreshClock.Elapsed += new ElapsedEventHandler(refresh);
    startButton.Click += new EventHandler(start);
    resetButton.Click += new EventHandler(reset);
    exitButton.Click += new EventHandler(exit);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;

    graphics.FillRectangle(Brushes.LawnGreen, ballInfo.Left, ballInfo.Top, title.Width, title.Height); //draws a green bar at the bottom

    graphics.DrawRectangle(Pens.Black, upperLeft.X+radius, upperLeft.Y+radius, upperRight.X-upperLeft.X, bottomLeft.Y-upperLeft.Y); //draws the path

    graphics.FillEllipse(ballBrush, ball.X, ball.Y, radius*2, radius*2); //draws the ball

    base.OnPaint(e);
  }

  protected void updateBall(Object sender, ElapsedEventArgs evt)
  {
    // Check if ball needs to change direction
    if(direction == 0 && ball.X <= upperLeft.X) //ball has finished going left
    {
      ball.X = upperLeft.X; //make sure it's on the path
      direction++;
    }
    else if(direction == 1 && ball.Y >= bottomLeft.Y) //ball has finished going down
    {
      ball.Y = bottomLeft.Y;
      direction++;
    }
    else if(direction == 2 && ball.X >= bottomRight.X) //ball has finished going right
    {
      ball.X = bottomRight.X;
      direction++;
    }
    else if(direction == 3 && ball.Y <= upperRight.Y) //ball has finished going up
    {
      direction++; //increment to 4 so that refresh() can't move the ball
      ball.Y = upperRight.Y;
      ballBrush.Color = Color.Gold;
      animationClock.Stop();
      refreshClock.Stop();
      Invalidate();
    }
    // Update ball
    switch(direction)
    {
      case 0: //ball is going left
        ball.X -= distance;
        break;
      case 1: //ball is going down
        ball.Y += distance;
        break;
      case 2: //ball is going right
        ball.X += distance;
        break;
      case 3: //ball is going up
        ball.Y -= distance;
        break;
    }
  }

  protected void refresh(Object sender, ElapsedEventArgs evt)
  {
    ballInfo.Text = "X: " + ball.X + "\nY: " + ball.Y + "\nDirection: ";
    switch(direction)
    {
      case 0:
        ballInfo.Text += "Left";
        break;
      case 1:
        ballInfo.Text += "Down";
        break;
      case 2:
        ballInfo.Text += "Right";
        break;
      case 3:
        ballInfo.Text += "Up";
        break;
    }
    Invalidate();
  }

  protected void start(Object sender, EventArgs events)
  {
    if(startButton.Text == "Pause")
    {
      animationClock.Stop();
      refreshClock.Stop();
      startButton.Text = "Resume";
    }
    else
    {
      animationClock.Start();
      refreshClock.Start();
      startButton.Text = "Pause";
    }
  }

  protected void reset(Object sender, EventArgs events)
  {
    animationClock.Stop();
    refreshClock.Stop();
    ball = upperRight;
    direction = 0;
    ballBrush.Color = Color.Magenta;
    ballInfo.Text = "X:\nY:\nDirection:";
    startButton.Text = "Go";
    Invalidate();
  }

  protected void exit(Object sender, EventArgs events)
  {
    System.Console.WriteLine("You clicked on the Exit button.");
    Close();
  }
}
