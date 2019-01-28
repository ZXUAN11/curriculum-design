using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Snakebody
    {
        private Color color;//颜色
        private int size;//单位大小
        private Point point;//坐标
        public Snakebody(Color color,int size,Point point)
        {
            this.color = color;
            this.size = size;
            this.point = point;
        }
        public Point Point  //绘制自身到画布
        {
            get{ return this.point;}
        }
        public virtual void Paint(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            lock(g)
            {
                try
                {
                    g.FillRectangle(brush, this.Point.X * this.size, this.Point.Y * this.size, this.size - 1, this.size - 1);
                }
                catch { }
            }
        }
    }
}
