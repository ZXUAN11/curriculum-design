using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Snake
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private Major p;
        private void MainForm_Load(object sender, EventArgs e)
        {
            //定义画布长宽，以及每个蛇块的大小
            int width, height, size;
            width = height = 30;
            size = 15;
            //设定游戏窗口大小
            this.pictureBox1.Width = width * size;
            this.pictureBox1.Height = height * size;
            this.Width = pictureBox1.Width + 30;
            this.Height = pictureBox1.Height + 60;
            //定义一个新画布（宽度，高度，单位大小，背景色，绘图句柄
            p = new Major(width, height, size, this.pictureBox1.BackColor, Graphics.FromHwnd(this.pictureBox1.Handle));
            p.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {

        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode==Keys.W|| e.KeyCode==Keys.Up)&& p.dir!=Major.Direction.Down)
            {
                p.dir = Major.Direction.Up;
                return;
            }
            if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right) && p.dir != Major.Direction.Left)
            {
                p.dir = Major.Direction.Right;
                return;
            }
            if ((e.KeyCode == Keys.S || e.KeyCode == Keys.Down) && p.dir != Major.Direction.Up)
            {
                p.dir = Major.Direction.Down;
                return;
            }
            if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left) && p.dir != Major.Direction.Right)
            {
                p.dir = Major.Direction.Left;
                return;
            }
            if (e.KeyValue == 27)
            {
                Application.Exit();
            }     
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if(p!=null)
            {
                p.Paint(e.Graphics);
            }
        } 
        
    }
}
