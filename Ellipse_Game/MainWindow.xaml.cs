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

namespace Ellipse_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ellipse currentEllipse;                     // the variable of current ellipse

        private Line[] lines = new Line[9];                 // the array of lines
        private Ellipse[] ellipses = new Ellipse[6];        // the array of ellipses

        private Line[] currentLine = new Line[3];           // create an array of current lines
        private Ellipse[] currentPoints = new Ellipse[4];   // create an array of current points of lines

        public MainWindow()
        {
            InitializeComponent();
            lbl_Win.Visibility = Visibility.Hidden;
            createEllipsesArray(ellipses);
            randomArrangementEllipses(ellipses);
            createLinesArray(lines);
            lineCoordinates(lines, ellipses);
        }

        // row of menu
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нужно перетащить эллипсы так, чтобы ни одна линия не пересекалась.");
        }
        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            randomArrangementEllipses(ellipses);
            lineCoordinates(lines, ellipses);
            lbl_Win.Visibility = Visibility.Hidden;
        }

        public void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            move_Ellipse(sender, e);
        }
        // change the position of ellipses
        public void el_MouseMoveEllispe(object sender, MouseEventArgs e)
        {
            currentEllipse = (Ellipse)sender;
        }
        // move ellipse and check of winning
        public void move_Ellipse(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePoint = e.GetPosition(this);
                coondinates.Text = "Coordinates: " + mousePoint.ToString();    // Show the coordinates
                if (currentEllipse != null)
                {
                    double posY = mousePoint.Y - 30;             // Vertical
                    double posX = mousePoint.X - 15;             // Horisontal
                    currentEllipse.Margin = new Thickness(posX, posY, posX, posY); // Change the position of Ellipse
                    changeLinePosition();                                          // Change the position of lines
                    if (ifWin(lines))
                        lbl_Win.Visibility = Visibility.Visible;
                }
            }
        }

        // set the initial coordinates of lines
        public void lineCoordinates(Line[] lines, Ellipse[] ellipses)
        {
            for (int i = 0, linesCounter = 0; i < 5; i++, linesCounter++)
            {
                setLineCoordinates(lines[linesCounter], ellipses[i], ellipses[i + 1]);
                if (i <= 2 && i > 0)
                    setLineCoordinates(lines[++linesCounter], ellipses[i], ellipses[5]);
                else if (i >= 3)
                    setLineCoordinates(lines[++linesCounter], ellipses[i], ellipses[0]);
            }
        }
        public void setLineCoordinates(Line line, Ellipse ellipse1, Ellipse ellipse2)
        {
            line.X1 = ellipse1.Margin.Left + 15;
            line.Y1 = ellipse1.Margin.Top + 15;
            line.X2 = ellipse2.Margin.Left + 15;
            line.Y2 = ellipse2.Margin.Top + 15;
        }

        // create arrays of ellipses and lines
        public void createEllipsesArray(Ellipse[] ellipses)
        {
            ellipses[0] = first;
            ellipses[1] = second;
            ellipses[2] = third;
            ellipses[3] = fourth;
            ellipses[4] = fifth;
            ellipses[5] = sixth;
        }
        public void createLinesArray(Line[] lines)
        {
            lines[0] = line0;
            lines[1] = line1;
            lines[2] = line2;
            lines[3] = line3;
            lines[4] = line4;
            lines[5] = line5;
            lines[6] = line6;
            lines[7] = line7;
            lines[8] = line8;
        }
        // random placement of ellipses 
        public void randomArrangementEllipses(Ellipse[] ellipses)
        {
            Random rand = new Random();
            for (int i = 0; i < ellipses.Length; i++)
            {
                int row = rand.Next(450);
                int col = rand.Next(200);
                ellipses[i].Margin = new Thickness(row, col, row, col);
            }
        }

        // auxiliary method of changing positions of current lines
        public void changePositionOfCurrentLines(Line[] line, Ellipse[] ellipses)
        {
            for (int i = 0; i < 3; i++)
            {
                line[i].X1 = ellipses[0].Margin.Left + 15;
                line[i].Y1 = ellipses[0].Margin.Top + 15;
                line[i].X2 = ellipses[i + 1].Margin.Left + 15;
                line[i].Y2 = ellipses[i + 1].Margin.Top + 15;
            }
        }
        public void setCurrentLinesAndPoints(Line l1, Line l2, Line l3, Ellipse p1, Ellipse p2, Ellipse p3, Ellipse p4)
        {
            // fill the current array of lines
            currentLine[0] = l1;
            currentLine[1] = l2;
            currentLine[2] = l3;
            // fill the current array of points of lines
            currentPoints[0] = p1;
            currentPoints[1] = p2;
            currentPoints[2] = p3;
            currentPoints[3] = p4;
            changePositionOfCurrentLines(currentLine, currentPoints);
        }
        // define the current points of lines
        public void changeLinePosition()
        {
            if (currentEllipse == first)
                setCurrentLinesAndPoints(lines[0], lines[6], lines[8], first, second, fourth, fifth);

            else if (currentEllipse == second)
                setCurrentLinesAndPoints(lines[0], lines[1], lines[2], second, first, third, sixth);

            else if (currentEllipse == third)
                setCurrentLinesAndPoints(lines[1], lines[3], lines[4], third, second, fourth, sixth);

            else if (currentEllipse == fourth)
                setCurrentLinesAndPoints(lines[3], lines[5], lines[6], fourth, third, fifth, first);

            else if (currentEllipse == fifth)
                setCurrentLinesAndPoints(lines[5], lines[7], lines[8], fifth, fourth, sixth, first);

            else if (currentEllipse == sixth)
                setCurrentLinesAndPoints(lines[7], lines[2], lines[4], sixth, fifth, second, third);
        }

        // check of crossing lines and winning
        private Point p1, p2, p3, p4;
        public bool ifWin(Line[] lines)
        {
            int length = lines.Length;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    p1 = new Point(Convert.ToInt32(lines[i].X1), Convert.ToInt32(lines[i].Y1));
                    p2 = new Point(Convert.ToInt32(lines[i].X2), Convert.ToInt32(lines[i].Y2));
                    p3 = new Point(Convert.ToInt32(lines[j].X1), Convert.ToInt32(lines[j].Y1));
                    p4 = new Point(Convert.ToInt32(lines[j].X2), Convert.ToInt32(lines[j].Y2));
                    if (areCrossing(p1, p2, p3, p4))
                        return false;
                    else if (i == 7 && !areCrossing(p1, p2, p3, p4))
                        return true;
                }
            }
            return false;
        }
        private double vector_mult(double ax, double ay, double bx, double by)  // cross product
        {
            return ax * by - bx * ay;
        }
        public bool areCrossing(Point p1, Point p2, Point p3, Point p4)         // check are crossing
        {
            double v1 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p1.X - p3.X, p1.Y - p3.Y);
            double v2 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p2.X - p3.X, p2.Y - p3.Y);
            double v3 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p3.X - p1.X, p3.Y - p1.Y);
            double v4 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p4.X - p1.X, p4.Y - p1.Y);
            if ((v1 * v2) < 0 && (v3 * v4) < 0)
                return true;
            return false;
        }
    }
}
