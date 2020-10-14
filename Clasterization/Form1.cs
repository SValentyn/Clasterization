using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Clasterization {
    public partial class Form1 : Form {
        private ClasterCreator2D clasterCreator;
        private Graphics graphics;
        private Random random;
        private List<Color> colors;

        public Form1() {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
            random = new Random();
            CreateColorsList();
        }

        private void CreateColorsList() {
            colors = new List<Color> {
                Color.Red,
                Color.Yellow,
                Color.Green,
                Color.Navy,
                Color.Pink,
                Color.Orange,
                Color.RoyalBlue
            };
        }

        private void AddAdditionColorsToList(int count) {
            for (int i = 0; i < count; i++) {
                int red = random.Next(100, 250);
                int green = random.Next(100, 250);
                int blue = random.Next(100, 250);
                colors.Add(Color.FromArgb(red, green, blue));
            }
        }

        private void ShowClasters() {
            List<Claster> clasters = PreparationOutput.ChangeClastersToOutput(
                new Point2D(pictureBox1.Size.Width, pictureBox1.Size.Height),
                clasterCreator.MinPoint2D, clasterCreator.MaxPoint2D,
                clasterCreator.GetClasters);

            pictureBox1.Image = null;
            pictureBox1.Update();
            graphics = pictureBox1.CreateGraphics();

            for (var i = 0; i < clasters.Count; i++) {
                Pen penB = new Pen(colors[i], 2.65f);
                Pen penL = new Pen(colors[i], 1.65f);

                foreach (Point2D point in clasters[i].GetPoints) {
                    graphics.DrawEllipse(penB, (int) point.X, (int) point.Y, 1.05f, 1.05f);
                   //  graphics.DrawLine(penL,
                   //      (float) point.X, (float) point.Y,
                   //      (float) clasters[i].GetCenter.X, (float) clasters[i].GetCenter.Y);
                }
            }
        }

        private void DoNextStepInClasters_Click(object sender, EventArgs e) {
            if (clasterCreator != null) {
                clasterCreator.DoOneStepCreateClaster();
                ShowClasters();
            }
        }

        private void DoUploadData_Click(object sender, EventArgs e) {
            int countClasters;

            /*
             * Checking if the file is specified
             */
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked) {
                MessageBox.Show("You must choose a file!");
                return;
            }

            /*
             * Checking for correct number of clusters
             */
            if (IfStringIsInteger(countClasterBox.Text)) {
                countClasters = int.Parse(countClasterBox.Text);
                if (countClasters <= 0) {
                    MessageBox.Show("You must enter the number of clusters greater than 0!");
                    return;
                }
            }
            else {
                MessageBox.Show("You must enter the correct number of clusters!");
                return;
            }

            doNextStepInClasters.Enabled = true;
            UploadData(countClasters);
            ShowClasters();
        }

        private void UploadData(int countClasters) {
            if (countClasters > colors.Count) {
                AddAdditionColorsToList(countClasters - colors.Count);
            }

            string path;
            StreamReader reader;
            List<Point2D> points = new List<Point2D>();

            /*
             * File Choice
             */
            if (radioButton1.Checked) {
                path = "../../../datasets/birch1.txt";
                reader = new StreamReader(path);
                countElementBox.Text = File.ReadAllLines(path).Length + "";
                countElementBox.Refresh();
            }
            else if (radioButton2.Checked) {
                path = "../../../datasets/birch2.txt";
                reader = new StreamReader(path);
                countElementBox.Text = File.ReadAllLines(path).Length + "";
                countElementBox.Refresh();
            }
            else if (radioButton3.Checked) {
                path = "../../../datasets/birch3.txt";
                reader = new StreamReader(path);
                countElementBox.Text = File.ReadAllLines(path).Length + "";
                countElementBox.Refresh();
            }
            else if (radioButton4.Checked) {
                path = "../../../datasets/s1.txt";
                reader = new StreamReader(path);
                countElementBox.Text = File.ReadAllLines(path).Length + "";
                countElementBox.Refresh();
            }
            else {
                MessageBox.Show("You must choose a file!");
                return;
            }

            /*
             * Filling an array of points with data from a file
             */
            if (reader != null) {
                while (!reader.EndOfStream) {
                    string[] lines = reader.ReadLine().Split(new char[] {' '});
                    double[] data = new double[2];
                    foreach (var line in lines) {
                        if (!line.Equals("")) {
                            if (data[0] == 0) data[0] = Convert.ToDouble(line);
                            else if (data[0] != 0) data[1] = Convert.ToDouble(line);
                        }
                    }

                    points.Add(new Point2D(data[0], data[1]));
                }

                clasterCreator = new ClasterCreator2D(points, countClasters);
            }
        }

        private static bool IfStringIsInteger(string str) {
            if (str.Count() < 1 || str.Count() > 13 || !str.All(char.IsDigit)) return false;
            if (str.Count() < 13 || (str.Count() == 13 && (str[0] == 1 || str[0] == 2))) return true;
            return false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            doNextStepInClasters.Enabled = false;
            countElementBox.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            doNextStepInClasters.Enabled = false;
            countElementBox.Clear();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            doNextStepInClasters.Enabled = false;
            countElementBox.Clear();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) {
            doNextStepInClasters.Enabled = false;
            countElementBox.Clear();
        }

        private void countClasterBox_TextChanged(object sender, EventArgs e) {
            doNextStepInClasters.Enabled = false;
        }
    }
}