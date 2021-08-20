using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Code written by Nicholas Heerdt
//Any unauthorized use of this code without my permission is forbidden
//not that you'd use this, my codes pretty bad

namespace Bruteforce
{
    public class Coordinate
    //Simple coordinate class for finding distacne between points
    {
        public int X;
        public int Y;
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public double get_distance(Coordinate b)
        {
            if (this == b)
            {
                return 0;
            }
            else
            {
                double tempX = (X - b.X);
                tempX *= tempX;
                double tempY = (Y - b.Y);
                tempY *= tempY;
                return Math.Sqrt(tempX + tempY);
            }
        }

    }

    public class Path
    {
        private int size = 0;
        private double best = double.MaxValue;//switch to max
        List<Coordinate> myCoords = new List<Coordinate>();//holds the coordinates
        List<double> distances = new List<double>(); //holds the distances between all the coordinates. Size n(n+1)/2
        Stack<int> order = new Stack<int>();
        List<int> bestPath = new List<int>();

        public double getBest()
        {
            return Math.Round(best * 10) / 10;
        }

        public int path_coord_x(int i)
        {
            return myCoords[bestPath[i]].X;
        }

        public int path_coord_y(int i)
        {
            return myCoords[bestPath[i]].Y;
        }

        public void Clear()
        {
            size = 0;
            best = double.MaxValue;
            myCoords.Clear();
            distances.Clear();
            order.Clear();
            bestPath.Clear();
        }
        public void add_coordinate(Coordinate b)
        {
            myCoords.Add(b);
            size++;
            for (int i = 0; i < size; i++)
            {
                distances.Add(b.get_distance(myCoords[i]));// add all the distances between b and all existing coordinates
            }
            return;
        }

        string get_cor_str(int i)
        {
            if (i < size)
            {
                return "(" + myCoords[i].X + ", " + myCoords[i].Y + ")";
            }
            else
            {
                return "invalid input see line 63";
            }
        }

        public string print_path()
        {
            if (size != 0)
            {
                string output = "";
                for (int i = 0; i < size - 1; i++)
                {
                    output += get_cor_str(bestPath[i]) + Environment.NewLine;
                }
                output += get_cor_str(bestPath[size - 1]);
                return output;
            }
            else
            {
                return "Empty Path";
            }
        }

        public double get_quick(int a, int b)//quickly get distance from list between points
        {
            if (b > a)
            {
                int temp = b;
                b = a;
                a = temp;
            }
            return distances[a * (a + 1) / 2 + b];
        }

        public int get_size()
        {
            return size;
        }

        public void find_path()
        {
            if (size != 0)
            {
                bool[] arr = new bool[size];
                recursive_path(arr, 0);
            }
        }
        private void recursive_path(bool[] is_in, double distance_so_far)
        {

            if (distance_so_far > best)//if the path in progress is already longer than the best path, it gives up this path
            {
                return;
            }

            bool visited_all = true;
            for (int i = 0; i < size; i++)
            {
                if (order.Count == 0)//sets up the first point in the path
                {
                    order.Push(i);
                    visited_all = false;
                    is_in[i] = true;
                    recursive_path(is_in, 0);
                    is_in[i] = false;
                    order.Pop();
                }


                else if (!is_in[i])//sets up the 2+th point on the path
                {

                    int temp2 = order.Peek();
                    //push order on stack
                    order.Push(i);
                    double temp = get_quick(i, temp2);
                    visited_all = false;
                    is_in[i] = true;
                    recursive_path(is_in, distance_so_far + temp);//+distance + newest line
                    is_in[i] = false;
                    //take off stack
                    order.Pop();

                }
            }

            if (visited_all)//
            {
                Stack<int> tempstk = new Stack<int>();

                if (distance_so_far < best)
                {
                    bestPath.Clear();
                    best = distance_so_far;
                    //Console.WriteLine("new best: " + best);
                    for (int i = 0; i < size; i++)
                    {
                        tempstk.Push(order.Peek());
                        order.Pop();
                    }
                    for (int i = 0; i < size; i++)
                    {
                        order.Push(tempstk.Peek());
                        bestPath.Add(tempstk.Peek());
                        tempstk.Pop();
                    }
                }

                //debugging code
                /*string strpath = "";
                for (int j = 0; j < size; j++)
                {
                    tempstk.Push(order.Peek());
                    strpath = get_cor_str(order.Peek()) + strpath;
                    order.Pop();
                }

                Console.WriteLine(strpath + distance_so_far);
                for (int j = 0; j < size; j++)
                {
                    order.Push(tempstk.Peek());
                    tempstk.Pop();
                }*/

            }
            return;
        }


        double path_size(int[] path)
        {
            if (path.Length != size)
            {
                return 0;
            }
            else
            {
                double to_return = 0;
                for (int i = 1; i < size; i++)
                {
                    to_return += get_quick(path[i - 1], path[i]);
                }
                return to_return;
            }
        }


        public void simulated_annealing()
        {

            if (size == 0)
            {
                return;
            }
            //generate order
            //im sure theres a math paper that explains this permutation generation algorithm
            //its also pretty inefficent but I am not sure what else I could do
            //but I call it Factorial-iary
            //basically if you have n number of items, theres n! permutations
            //so what I do is I randomly generate a number between 0 and n!-1
            //and I take x = random % n and put it in the xth open spot for an n sized array
            //then random = random/n
            //and repeat with (n-1)
            //except for the array which is still n sized, and if it skips counting the filled spots as if it were an n-1 sized array
            //but if I put an i into each spot(where i is from 0 to n) then it creates a unique path
            //its very similar to converting decimal numbers to binary
            //to be honest I didn't think it would work, it was just an experent but in all cases I've tested, it seems to work

            best = Double.MaxValue;

            //generate number
            Random rnd = new Random();
                int key = 1;
                for (int i = size; i > 0; i--)
                {
                    key *= i;
                }
                key = rnd.Next(0, key);
                int[] order = new int[size];
                bool[] visited = new bool[size];
                int offset = 0;
                int spot = 0;
            //generate path with number
            for (int i = size; i > 0; i--)
            {

                //calculate the offset
                offset = key % i;

                int counter = 0;
                for (int j = 0; j < size; j++)
                {
                    if (!visited[j] && counter == offset)
                    {
                        visited[j] = true;
                        spot = j;
                        j = size;
                    }
                    else if (!visited[j])
                    {
                        counter++;
                    }
                }

                order[i - 1] = spot;
                key = key / i;
            }

            //actually simulating annealing
            double tempature = 95;
            double current = path_size(order); ;
            double alpha = 0.98;//cooling rate

            int swap1;
            int swap2;
            int temp;
            double delta;

            //loop begin
            for (int i = 0; i < 1000000; i++ ) {
                //find an element to swap and its neighbor
                swap1 = rnd.Next(0, order.Length - 1);
                swap2 = rnd.Next(0, order.Length - 2);
                if (swap1 == swap2)
                {
                    swap1++;
                }
                //swap the elements
                temp = order[swap1];
                order[swap1] = order[swap2];
                order[swap2] = temp;

                //find delta
                delta = path_size(order) - current ;
                if (path_size(order) < current)
                {
                    //if delta is less than 0 then the new path is shorter than the current on
                    current = path_size(order);

                    if (current < best)
                    {
                        best = current;
                        bestPath.Clear();
                        for (int p = 0; p < size; p++)
                        {
                            bestPath.Add(order[p]);
                        }
                    }
                }
                
               else if (Math.Exp(0 - (delta / tempature)) > rnd.NextDouble())
                {   //else if theres a chance that this might be the next path anyways
                    
                    current = path_size(order);
                }
               
                else
                {
                    //if it turns out that the next path is not chosen then restore the previous path
                    temp = order[swap1];
                    order[swap1] = order[swap2];
                    order[swap2] = temp;
                }

                if (i % 1000 == 0)
                {
                    tempature *= alpha;
                }

            }
            return;
        }

    }



}
/*namespace Brute_force_console
{
    using Bruteforce;
    class Program
    {
        static void Main(string[] args)
        {
            Bruteforce.Coordinate ayy = new Coordinate(834, 707);
            Coordinate bee = new Coordinate(843, 626);
            Coordinate cee = new Coordinate(140, 733);
            Coordinate dee = new Coordinate(109, 723);
            Coordinate e = new Coordinate(600, 747);
            Coordinate f = new Coordinate(341, 94);
            Coordinate g = new Coordinate(657, 197);
            Coordinate h = new Coordinate(842, 123);
            Coordinate eye = new Coordinate(531, 194);
            Coordinate jay = new Coordinate(286, 336);

            Path myPath = new Path();
            myPath.add_coordinate(ayy);
            myPath.add_coordinate(bee);
            myPath.add_coordinate(cee);
            myPath.add_coordinate(dee);
            myPath.add_coordinate(e);
            myPath.add_coordinate(f);
            myPath.add_coordinate(g);
            myPath.add_coordinate(h);
            myPath.add_coordinate(eye);
            myPath.add_coordinate(jay);
            myPath.find_path();
            myPath.print_path();

        }
    }
}*/

