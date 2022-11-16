using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tree
{
    public partial class Form1 : Form
    {
        private List<List<Point>> points = new List<List<Point>>();
        private List<char[]> all_symbols;
        private string str;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            str = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (str != null)
            {
                ClearScreen();
                Build();
                FindValue();
            }
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            Build();
        }

        private void Build()
        {
            Label head = new Label
            {
                Text = "Tree Start",
                Width = 200,
                Location = new Point(this.Size.Width / 2, this.Size.Height / 12)
            };
            all_symbols = new List<char[]>();
            all_symbols.Add("T E".Split(' ').SelectMany(x => x.ToCharArray()).ToArray());
            all_symbols.Add("M N A I".Split(' ').SelectMany(x => x.ToCharArray()).ToArray());
            all_symbols.Add("O G K D W R U S".Split(' ').SelectMany(x => x.ToCharArray()).ToArray());
            all_symbols.Add("S O Q Z Y C X B J P A L U F V H".Split(' ').SelectMany(x => x.ToCharArray()).ToArray());
            points.Add(new List<Point>());
            points.Add(new List<Point>());
            points.Add(new List<Point>());
            points.Add(new List<Point>());
            int m = 0;
            for (int i = 2; i <= 16; i *= 2)
            {
                for (int j = 0; j < i; j++)
                {
                    Point tmp_point = new Point(
                        Convert.ToInt32(this.Size.Width / (i * 2) + this.Size.Width / (i) * j),
                        Convert.ToInt32(this.Size.Height / 13 * (i * 0.4)) + 100);
                    Label tmp = new Label
                    {
                        Text = all_symbols[m][j].ToString(),
                        Width = 15,
                        Location = tmp_point
                    };
                    points[m].Add(tmp_point);
                    panel1.Controls.Add(tmp);
                }
                m++;
            }
            panel1.Controls.Add(head);
            Pen pen = new Pen(Brushes.Green, 3.0f);
            foreach (var point_layer in points)
            {
                foreach (var point in point_layer)
                {
                    Graphics krug = panel1.CreateGraphics();
                    krug.DrawEllipse(pen, point.X - 19, point.Y - 19, 50, 50);
                }
            }
        }

        private void ClearScreen()
        {
            panel1.Controls.Clear();
        }

        private void FindValue()
        {
            Label name_str = new Label
            {
                Text = str,
                Font = new Font(FontFamily.GenericMonospace, 20.0f),
                Width = str.Length * 30,
                Location = new Point(this.Width / 2 - this.Width * str.Length / 100, 80)
            };
            panel1.Controls.Add(name_str);

            string[] signs = str.Split(' ');
            string final_str = "";
            for (int i = 0; i < signs.Length; i++)
            {
                int initial_layer = 0;
                int target_position = 0;
                char taget_char = ' ';
                for (int j = 0; j < signs[i].Length; j++)
                {
                    WaitSeconds(0.4);
                    if (signs[i][j] == '-')
                    {
                        taget_char = all_symbols[initial_layer][target_position];
                        if (initial_layer + 1 == signs[i].Length)
                        {
                            Pen pen = new Pen(Brushes.Red, 3.0f);
                            Graphics krug = panel1.CreateGraphics();
                            krug.DrawEllipse(pen, points[initial_layer][target_position].X - 19, points[initial_layer][target_position].Y - 19, 50, 50);
                        }
                        else
                        {
                            Pen pen = new Pen(Brushes.Yellow, 3.0f);
                            Graphics krug = panel1.CreateGraphics();
                            krug.DrawEllipse(pen, points[initial_layer][target_position].X - 19, points[initial_layer][target_position].Y - 19, 50, 50);
                        }
                        target_position *= 2;
                        initial_layer++;
                    }
                    else if (signs[i][j] == '.')
                    {
                        taget_char = all_symbols[initial_layer][target_position + 1];
                        if (initial_layer + 1 == signs[i].Length)
                        {
                            Pen pen = new Pen(Brushes.Red, 3.0f);
                            Graphics krug = panel1.CreateGraphics();
                            krug.DrawEllipse(pen, points[initial_layer][target_position + 1].X - 19, points[initial_layer][target_position + 1].Y - 19, 50, 50);
                        }
                        else
                        {
                            Pen pen = new Pen(Brushes.Yellow, 3.0f);
                            Graphics krug = panel1.CreateGraphics();
                            krug.DrawEllipse(pen, points[initial_layer][target_position + 1].X - 19, points[initial_layer][target_position + 1].Y - 19, 50, 50);
                        }
                        target_position = (target_position + 1) * 2;
                        initial_layer++;
                    }
                    else if (signs[i][j] == ' ')
                    {
                        final_str += "  ";
                        label1.Text += "  ";
                    }
                    else
                    {
                        final_str += "Error";
                        label1.Text += "Error";
                        break;
                    }
                }
                WaitSeconds(0.8);
                Pen pen1 = new Pen(Brushes.Green, 3.0f);
                foreach (var point_layer in points)
                {
                    foreach (var point in point_layer)
                    {
                        Graphics krug = panel1.CreateGraphics();
                        krug.DrawEllipse(pen1, point.X - 19, point.Y - 19, 50, 50);
                    }
                }
                final_str += taget_char;
                label1.Text += taget_char;
            }
        }

        private void WaitSeconds(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }
    }
}