using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Urban_octo_funicular
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        int[] array1;
        int[,] array2;
        readonly string mydocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        FileStream file = null;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Button_Array1Generate_Click(object sender, EventArgs e)
        {
            bool parsed = int.TryParse(TextBox_Array1Range.Text, out int range);
            Random rand = new Random();

            if (parsed)
            {
                TextBox_Array1Output.Text = "";
                array1 = new int[range];
                for (int i = 0; i < range; i++)
                {
                    array1[i] = rand.Next(10);
                    TextBox_Array1Output.Text += $"[{array1[i]}] ";
                }
            }
            else
            {
                MessageBox.Show($"{TextBox_Array1Range.Text} - не является допустимым значением");
            }

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            array1 = null;
            TextBox_Array1Output.Text = "";
        }

        private void Button_Array2Generate_Click(object sender, EventArgs e)
        {
            
            bool parsedX = int.TryParse(TextBox_Array2XRangeInput.Text, out int rangeX);
            bool parsedY = int.TryParse(TextBox_Array2YRangeInput.Text, out int rangeY);
            Random rand = new Random();

            file = new FileStream(mydocs+@"\Array2.txt", FileMode.OpenOrCreate);

            if (parsedX && parsedY)
            {
                file.SetLength(0);
                //string[] tempString = new string[rangeX];

                array2 = new int[rangeX, rangeY];
                
                for(int i = 0; i<rangeX; i++)
                {
                    string tempString = "";
                    for (int j=0; j<rangeY; j++)
                    {
                        array2[i, j] = rand.Next(10);
                        tempString += $"[{array2[i,j]}] ";
                    }
                    tempString += "\n";
                    byte[] tempBytes = Encoding.Default.GetBytes(tempString);
                    file.Write(tempBytes, 0, tempBytes.Length);

                }

                Process.Start(mydocs + @"\Array2.txt");

            }
            else
            {
                MessageBox.Show($"[{TextBox_Array2XRangeInput.Text};" +
                    $"{TextBox_Array2YRangeInput.Text}] - не являются допустимыми значениями");
            }

            if (file != null)
                file.Close();

        }

        private void Button_Array2Clear_Click(object sender, EventArgs e)
        {
            file = new FileStream(mydocs + @"\Array2.txt", FileMode.OpenOrCreate);
            file.SetLength(0);
            array2 = null;
            Process.Start(mydocs + @"\Array2.txt");
            if (file != null)
                file.Close();
        }

        private void calc_problem_Click(object sender, EventArgs e)
        {

        }

        private void np_addNumber(object sender, EventArgs e)
        {
            Button numpadButton = (Button)sender; 
            calc_problem.Text += numpadButton.Text;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            calc_problem.Text += e.KeyValue;
        }

    }

}
