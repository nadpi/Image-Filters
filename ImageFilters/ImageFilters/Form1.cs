using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;
        int Wmax = 1;
        int T = 1;
        int SelectedFilterID = 0;
        int UsedAlgorithm = 0;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }

        private void btnZGraph_Click(object sender, EventArgs e)
        {

            if (Wmax == 1)
            {
                Wmax = 3;
            }

            double[] x_values = new double[Wmax];
            double[] y_values_CountingAlpha = new double[Wmax];
            double[] y_values_kthAlpha = new double[Wmax];
            double[] y_values_CountingMed = new double[Wmax];
            double[] y_values_QuickMed = new double[Wmax];
            int windsizes = 3;
            for (int i = 0; windsizes <= Wmax; i++)
            {
                x_values[i] = windsizes;
                windsizes += 2;

            }
            int StartTimeCountingAlpha;
            int EndTimeCountingAlpha;
            int AllTimeCountingAlpha;
            int StartTimeKthAlpha;
            int EndTimeKthAlpha;
            int AllTimeKthAlpha;
            int k = 0;
            for (windsizes = 3; windsizes <= Wmax; windsizes += 2)
            {
                if (UsedAlgorithm == 0)
                {
                    StartTimeCountingAlpha = Environment.TickCount;
                    AlphaTrimFilter.ApplyFilter(ImageMatrix, Wmax, 0, T);
                    EndTimeCountingAlpha = Environment.TickCount;
                    AllTimeCountingAlpha = AlphaTrimFilter.GetTimeofAlpha(StartTimeCountingAlpha, EndTimeCountingAlpha);
                    y_values_CountingAlpha[k] = AllTimeCountingAlpha;
                }
                else if (UsedAlgorithm == 1)
                {

                    StartTimeKthAlpha = Environment.TickCount;
                    AlphaTrimFilter.ApplyFilter(ImageMatrix, Wmax, 1, T);
                    EndTimeKthAlpha = Environment.TickCount;
                    AllTimeKthAlpha = AlphaTrimFilter.GetTimeofAlpha(StartTimeKthAlpha, EndTimeKthAlpha);
                    y_values_kthAlpha[k] = AllTimeKthAlpha;
                }
                k++;
            }
            int StartTimeCountingMed;
            int EndTimeCountingMed;
            int AllTimeCountingMed;
            int StartTimeQuickMed;
            int EndTimeQuickMed;
            int AllTimeQuickMed;
            int j = 0;
            for (windsizes = 3; windsizes <= Wmax; windsizes += 2)
            {
                if (UsedAlgorithm == 0)
                {
                    StartTimeCountingMed = Environment.TickCount;
                    AlphaTrimFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm, T);
                    EndTimeCountingMed = Environment.TickCount;
                    AllTimeCountingMed = AlphaTrimFilter.GetTimeofAlpha(StartTimeCountingMed, EndTimeCountingMed);
                    y_values_CountingMed[j] = AllTimeCountingMed;
                }
                else
                {
                    StartTimeQuickMed = Environment.TickCount;
                    AlphaTrimFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm, T);
                    EndTimeQuickMed = Environment.TickCount;
                    AllTimeQuickMed = AlphaTrimFilter.GetTimeofAlpha(StartTimeQuickMed, EndTimeQuickMed);
                    y_values_QuickMed[j] = AllTimeQuickMed;
                }
                j++;
            }



            //Create a graph and add two curves to it
            ZGraphForm ZGF = new ZGraphForm("Time Complexity Graph", "Window Size", "Time");

            if (SelectedFilterID == 0)
            {
                if (UsedAlgorithm == 0)
                {
                    ZGF.add_curve("Counting Algorithm Graph", x_values, y_values_CountingAlpha, Color.Red);
                }
                else
                {
                    ZGF.add_curve("Kth ALgorithm Graph", x_values, y_values_kthAlpha, Color.Red);

                }

            }
            else
            {
                if (UsedAlgorithm == 0)
                {
                    ZGF.add_curve("Counting Algorithm Graph", x_values, y_values_CountingMed, Color.BlueViolet);
                }
                else
                {
                    ZGF.add_curve("Quick ALgorithm Graph", x_values, y_values_QuickMed, Color.BlueViolet);

                }
            }
            ZGF.Show();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            if (SelectedFilterID == 0)
            {
                ImageOperations.DisplayImage(AlphaTrimFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm, T), pictureBox2);
            }
            else
            {
                ImageOperations.DisplayImage(AdaptiveMedianFilter.ApplyFilter(ImageMatrix, Wmax, UsedAlgorithm), pictureBox2);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAlgorithm.Visible = true;
            lbl_algorithm.Visible = true;
            if (cbFilter.SelectedIndex == 0)
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = true;
                trimmingValue.Visible = true;
                SelectedFilterID = 0;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Counting Sort");
                cbAlgorithm.Items.Add("Kth Smallest/Largest");
            }
            else
            {
                label1.Visible = true;
                maxWindowSize.Visible = true;
                label2.Visible = false;
                trimmingValue.Visible = false;
                SelectedFilterID = 1;

                cbAlgorithm.Items.Clear();

                cbAlgorithm.Items.Add("Quick Sort");
                cbAlgorithm.Items.Add("Counting Sort");
            }
        }

        private void cbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsedAlgorithm = cbAlgorithm.SelectedIndex;
        }

        private void maxWindowSize_ValueChanged(object sender, EventArgs e)
        {
            Wmax = (int)maxWindowSize.Value;
        }

        private void trimmingValue_ValueChanged(object sender, EventArgs e)
        {
            T = (int)trimmingValue.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Time" + AllTimeAlpha);
        }
    }
}