using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrix_calculator
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
           // Close();
       // }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void count_Click(object sender, EventArgs e)
        {
            Form1.fmm.Show();
            try
            {
                string input = textBox1.Text;
                string[] rows = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int n = rows.Length;
                double[,] matrix = new double[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] cols = rows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (cols.Length != n)
                    {
                        throw new Exception("Invalid matrix format. Please enter a valid square matrix.");
                    }
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = Convert.ToDouble(cols[j]);
                    }
                }

                Matrix mat = new Matrix(matrix);
                double determinant = mat.CalculateDeterminant();
                MessageBox.Show("Determinant: " + determinant, "Result");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }
    }
    public class Matrix
    {
        private double[,] matrix;
        private int size;

        public Matrix(double[,] inputMatrix)
        {
            matrix = inputMatrix;
            size = (int)Math.Sqrt(matrix.Length);
        }

        public double CalculateDeterminant()
        {
            if (size == 1)
            {
                return matrix[0, 0];
            }
            else if (size == 2)
            {
                return (matrix[0, 0] * matrix[1, 1]) - (matrix[1, 0] * matrix[0, 1]);
            }
            else
            {
                double det = 0;
                for (int k = 0; k < size; k++)
                {
                    Matrix subMatrix = CreateSubMatrix(matrix, size, k);
                    det += (k % 2 == 1 ? -1 : 1) * matrix[0, k] * subMatrix.CalculateDeterminant();
                }
                return det;
            }
        }

        private Matrix CreateSubMatrix(double[,] matrix, int n, int k)
        {
            double[,] subMatrix = new double[n - 1, n - 1];
            int p = 0;
            for (int i = 1; i < n; i++)
            {
                int q = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == k) continue;
                    subMatrix[p, q] = matrix[i, j];
                    q++;
                }
                p++;
            }
            return new Matrix(subMatrix);
        }
    }
}
