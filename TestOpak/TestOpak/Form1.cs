using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TestOpak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader("matematika.txt");
                int pocet = 0;
                double soucet = 0;

                listBox1.Items.Clear();
                listBox2.Items.Clear();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    line = line.Trim();
                    listBox1.Items.Add(line);
                    string[] pole = line.Split(' ');
                    int c1 = int.Parse(pole[0]);
                    int c2 = int.Parse(pole[2]);
                    char operace = char.Parse(pole[1]);
                    double vysledek;

                    switch (operace)
                    {
                        case '+':
                            vysledek = c1 + c2;
                            break;
                        case '-':
                            vysledek = c1 - c2;
                            break;
                        case '/':
                            vysledek = c1 / c2;
                            break;
                        case '*':
                            vysledek = c1 * c2;
                            break;
                        default:
                            throw new Exception("Neznámí operand!");
                            break;
                    }
                    pocet++;
                    soucet += vysledek;

                    listBox2.Items.Add(line + " " + vysledek);
                }
                sr.Close();
                StreamWriter sw = new StreamWriter("matematika.txt", false);
                foreach(string s in listBox2.Items)
                {
                    sw.WriteLine(s);
                }
                sw.Close();
                FileStream fs = new FileStream("prumer.dat", FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(soucet / pocet);
                bw.Close();
                fs = new FileStream("prumer.dat", FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                MessageBox.Show(br.ReadDouble().ToString());
                br.Close();
            }
            catch (Exception ex)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
