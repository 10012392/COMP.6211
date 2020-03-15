using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlgorithmsStage1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Movement flags   
        bool testFlag1X = true;
        bool testFlag1Y = true;
        #endregion

        #region Key Pressed Flags
        bool flagA = false;
        bool flagD = false;
        bool flagW = false;
        bool flagS = false;
        #endregion

        #region Object location
        double leftMargin;
        double topMargin;
        double rightMargin;
        double bottomMargin;
        #endregion

        #region Random Number

        // Add code here

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region Random Number

            // Add code here

            #endregion

            // Set game loop timer
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(10000); // 10000 ticks = 1 milisecond
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Our time event that fires the movement
        /// </summary>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            #region Move using a key press
            //•	Find the location of the object
            leftMargin = testImage1.Margin.Left;
            topMargin = testImage1.Margin.Top;
            rightMargin = testImage1.Margin.Right;
            bottomMargin = testImage1.Margin.Bottom;

            if (flagA == true)  leftMargin = leftMargin - 5.00;
            if (flagD == true)  leftMargin = leftMargin + 5.0; //•	To move an object right increment the X-axis
            if (flagW == true)  topMargin = topMargin - 5.00;
            if (flagS == true)  topMargin = topMargin + 5.00;
            //•	Reset the location of the object
            testImage1.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);

            #endregion

            #region Lock_To_Grid
            //            •	Find the location of the object
            leftMargin = testImage1.Margin.Left;
            topMargin = testImage1.Margin.Top;
            rightMargin = testImage1.Margin.Right;
            bottomMargin = testImage1.Margin.Bottom;

            //•	The Right edge of the rectangular object does not pass the Right side of the domain
            if ((leftMargin + testImage1.Width) > TestGrid.Width)
            //leader image left margim      >     follow image left margin
            {
                leftMargin = TestGrid.Width - testImage1.Width;
                flagD = false;
                flagA = true;
                // follow image left margin += speed

            }

            //•	The Left edge of the rectangular object does not pass the Left side of the domain
            if (leftMargin < 0)
            {
                leftMargin = 0;
                flagA = false;
                flagD = true;
            }

            //•	The Bottom edge of the rectangular object does not pass the Bottom side of the domain
            if ((topMargin + testImage1.Height) > TestGrid.Height)
            {
                topMargin = TestGrid.Height - testImage1.Height;
                flagS = false;
                flagW = true;
            }

            //•	The Top edge of the rectangular object does not pass the Top side of the domain
            if (topMargin < 0)
            {
                topMargin = 0;
                flagW = false;
                flagS = true;
            }


            //•	Reset the location of the object
            testImage1.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);

            #endregion


            #region Move_Lock_To_Grid

            // Code is above - is included in the lock to grid code

            #endregion

            #region Follow

            Follow(testImage2, testImage1, 1.50);
            Follow(testImage3, testImage1, 1.50);

            #endregion

            #region Runaway

            // Add code here

            Runaway(testImage4, testImage1, 0.50);
            Lock_To_Grid(testImage4, TestGrid);

            #endregion

            #region Collide

            // Add code here

            #endregion

            #region Random Number

            // Add code here

            #endregion

        }


        /// <summary>
        /// Resizes the grid to the screen size
        /// </summary>
        private void TestWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TestGrid.Width = TestWindow.Width - 30;
            TestGrid.Height = TestWindow.Height - 50;
        }

        #region Key Pressed test

        private void TestWindow_KeyDown(object sender, KeyEventArgs e)
        {

            flagA = false;
            flagD = false;
            flagW = false;
            flagS = false;

            if (e.Key == Key.A) flagA = true;
            if (e.Key == Key.D) flagD = true;
            if (e.Key == Key.W) flagW = true;
            if (e.Key == Key.S) flagS = true;
        }

        #endregion

        //Follow Method - Allows the other two images follow testImage1
        private void Follow(Image Follower, Image Leader, double Speed)
        {
            double Left = Follower.Margin.Left;
            double Top = Follower.Margin.Top;

            if(Left < Leader.Margin.Left)
            {
                Left += Speed;

            } else
            {
                Left -= Speed;
            }

            if(Top < Leader.Margin.Top)
            {
                Top += Speed;
            } else
            {
                Top -= Speed;
            }
            Follower.Margin = new Thickness(Left, Top, 0, 0);
        }

        private void Runaway(Image Runner, Image Leader, double Speed)
        {
            double Left = Runner.Margin.Left;
            double Top = Runner.Margin.Top;

            if(Left < Leader.Margin.Left)
            {
                Left -= Speed;
            }
            else
            {
                Left += Speed;
            }

            if(Top < Leader.Margin.Left)
            {
                Top -= Speed;
            }
            else
            {
                Top += Speed;
            }
            Runner.Margin = new Thickness(Left, Top, 0, 0);
        }

        private void Lock_To_Grid(Image image, Grid mainGrid)
        {
            #region Lock_To_Grid
            //•	Find the location of the object
            leftMargin = image.Margin.Left;
            topMargin = image.Margin.Top;
            rightMargin = image.Margin.Right;
            bottomMargin = image.Margin.Bottom;

            //•	The Right edge of the rectangular object does not pass the Right side of the domain
            if ((leftMargin + image.Width) > mainGrid.Width)
            {
                leftMargin = mainGrid.Width - image.Width;

            }

            //•	The Left edge of the rectangular object does not pass the Left side of the domain
            if (leftMargin < 0)
            {
                leftMargin = 0;
            }

            //•	The Bottom edge of the rectangular object does not pass the Bottom side of the domain
            if ((topMargin + image.Height) > mainGrid.Height)
            {
                topMargin = mainGrid.Height - image.Height;
            }

            //•	The Top edge of the rectangular object does not pass the Top side of the domain
            if (topMargin < 0)
            {
                topMargin = 0;
            }


            //•	Reset the location of the object
            image.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);

            #endregion
        }
    }
}
