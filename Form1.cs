using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS489_Pathfinding_GUI
{
    using Bruteforce;
    public partial class Form1 : Form
    {
        Path myPath = new Path();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(834, 707)" + Environment.NewLine + "(843,626)" + Environment.NewLine + "(140,733)" + Environment.NewLine +
                "(109, 723)" + Environment.NewLine + "(600, 747)" + Environment.NewLine + "(341,94)" + Environment.NewLine +
                "(657,197)" + Environment.NewLine + "(842,123)" + Environment.NewLine + "(531, 194)" + Environment.NewLine + "(286,336)";
            int[] set1x = { 834, 843, 140, 109, 600, 341, 657, 842, 531, 286 };
            int[] set1y = { 707, 626, 733, 723, 747, 94, 197, 123, 194, 336 };
            myPath.Clear();
            for (int i = 0; i < 10; i++)
            {
                Coordinate temp = new Coordinate(set1x[i], set1y[i]);
                myPath.add_coordinate(temp);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(8,377)" + Environment.NewLine + "(450, 352)" + Environment.NewLine + "(519, 290)" + Environment.NewLine +
                "(398, 604)" + Environment.NewLine + "(417,496)" + Environment.NewLine + "(57,607)" + Environment.NewLine +
                "(119,4)" + Environment.NewLine + "(166,663)" + Environment.NewLine + "(280,622)" + Environment.NewLine + "(531,571)";
            int[] set2x = { 8, 450, 519, 398, 417, 57, 119, 166, 280, 531};
            int[] set2y = { 377, 352, 290, 604, 496, 607, 4, 663, 622, 571 };
            myPath.Clear();
            for (int i = 0; i < 10; i++)
            {
                Coordinate temp = new Coordinate(set2x[i], set2y[i]);
                myPath.add_coordinate(temp);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(518, 995)" + Environment.NewLine + "(590, 935)" + Environment.NewLine + "(600, 985)" + Environment.NewLine +
                "(151, 225)" + Environment.NewLine + "(168, 657)" + Environment.NewLine + "(202, 454)" + Environment.NewLine +
                "(310, 717)" + Environment.NewLine + "(425,802)" + Environment.NewLine + "(480,940)" + Environment.NewLine + "(300, 1035)";
            int[] set3x = { 518, 590, 600, 151, 168, 202, 310, 425, 480, 300 };
            int[] set3y = { 995, 935, 985, 225, 657, 454, 717, 802, 940, 1035 };
            myPath.Clear();
            for (int i = 0; i < 10; i++)
            {
                Coordinate temp = new Coordinate(set3x[i], set3y[i]);
                myPath.add_coordinate(temp);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //I was going to have a system where it would read a custom input, but something came up in my schedule so I did not have time to impliment it
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            myPath.find_path();
            textBox2.Text = myPath.print_path();
            chart1.Series["Distance"].Points.Clear();
            for (int i = 0; i < myPath.get_size(); i++)
            {
                chart1.Series["Distance"].Points.AddXY(myPath.path_coord_x(i), myPath.path_coord_y(i));
            }
            label3.Text = "Path distance = " + myPath.getBest();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            myPath.simulated_annealing();
            textBox2.Text = myPath.print_path();
            chart1.Series["Distance"].Points.Clear();
            for (int i = 0; i < myPath.get_size(); i++)
            {
                chart1.Series["Distance"].Points.AddXY(myPath.path_coord_x(i), myPath.path_coord_y(i));
            }
            label3.Text = "Path distance = " + myPath.getBest();
        }
    }
}
