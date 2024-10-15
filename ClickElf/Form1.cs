using ClickElf.Mouse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickElf
{
    public partial class Form1 : Form
    {
        private MouseHandler mouseHandler;
        public Form1()
        {
            InitializeComponent();
            mouseHandler = new MouseHandler();
            //mouseTracker = new MouseTracker();
            mouseHandler.mouseTracker.MouseMoved += OnMouseMoved;
            listBox1.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (psender, pe) => { pe.Graphics.DrawString("123456", new Font("Arial", 20), Brushes.Black, new PointF(100f, 100f)); };
            PrintDialog printDialog = new PrintDialog() { Document = printDocument };
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }*/


        }

        private void OnMouseMoved(int x, int y)
        {
            lblMouse.Text = $"x:{x}, y:{y}";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(lblMouse.Text);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                //button2.PerformClick();
                mouseHandler.MouseClick(MouseHandler.MouseType.Right);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x, y;
            if (!int.TryParse(textBox1.Text, out x) || !int.TryParse(textBox2.Text, out y))
            {
                MessageBox.Show("Invalid Input!", "foo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mouseHandler.MouseMove(x,y);
        }
    }
}
