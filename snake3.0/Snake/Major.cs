using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Timers;

namespace Snake
{
    public class Major
    {
        public enum Direction//枚举移动方向
        {
            Up,
            Down,
            Right,
            Left
        }
        private int width;//宽度
        private int height;//高度
        private Color bgcolor;//背景色
        private Graphics Palette;//画布
        private ArrayList body;//蛇块列表
        public Direction dir;//前进方向
        private System.Timers.Timer timerblock;//更新器
        private Snakebody food;//当前食物
        private int size;//单位大小
        public Major(int width, int height, int size, Color bgColor, Graphics g)
        {
            this.width = width;
            this.height = height;
            this.bgcolor = bgColor;
            this.Palette = g;
            this.size = size;
            this.body = new ArrayList();
            this.body.Insert(0, (new Snakebody(Color.Red, this.size, new Point(width / 2, height / 2))));//在画布的正中央产生只有一节的贪吃蛇
            this.dir = Direction.Right;
        }//设定类属性值，初始化蛇块列表ArrayList
        public void Start()//开始
        {
            this.food = Getfood();//生成一个食物
            //初始化计时器
            timerblock = new System.Timers.Timer();
            timerblock.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);//注册计时器的事件
            timerblock.AutoReset = true;//设置是执行一次（false）还是一直执行(true)，默认为true
            timerblock.Interval = 500;
            timerblock.Start();
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e) //指定timer触发事件
        {
            this.Move();//前进一个单位
            if (this.CheckDead())//检测是否游戏结束
            {
                this.timerblock.Stop();
                this.timerblock.Dispose();
                MessageBox.Show("Score: " + this.body.Count, "Game Over");
                Application.Exit();
            }
        }
        private bool CheckDead()//检查游戏是否结束
        {

            Snakebody head = (Snakebody)(this.body[0]);//取蛇块列表的第一个当作蛇头
            //检查是否超出画布范围
            if (head.Point.X < 0 || head.Point.Y < 0 || head.Point.X >= this.width || head.Point.Y >= this.height)
                return true;
            for (int i = 1; i < this.body.Count; i++)//检查是否撞上自己
            {
                Snakebody s = (Snakebody)this.body[i];
                if (head.Point.X == s.Point.X && head.Point.Y == s.Point.Y)
                {
                    return true;
                }
            }
            return false;
        }
        private Snakebody Getfood()//获得食物
        {
            Snakebody food = null;
            Random r = new Random();
            bool redo = false;
            while (true)
            {
                redo = false;
                int x = r.Next(this.width);
                int y = r.Next(this.height);
                for (int i = 0; i < this.body.Count; i++)//检查食物所在坐标是否和贪吃蛇冲突
                {
                    Snakebody s = (Snakebody)(this.body[i]);
                    if (s.Point.X == x && s.Point.Y == y)    //有冲突时，在随机找一个坐标
                    {
                        redo = true;
                    }
                }
                if (redo == false)
                {
                    food = new Snakebody(Color.Black, this.size, new Point(x, y));
                    break;
                }
            }
            return food;
        }
        private void Move()//贪吃蛇移动
        {
            Point p;//下一个坐标位置
            Snakebody head = (Snakebody)(this.body[0]);
            if (this.dir == Direction.Up)
                p = new Point(head.Point.X, head.Point.Y - 1);
            else if (this.dir == Direction.Down)
                p = new Point(head.Point.X, head.Point.Y + 1);
            else if (this.dir == Direction.Left)
                p = new Point(head.Point.X - 1, head.Point.Y);
            else
                p = new Point(head.Point.X + 1, head.Point.Y);

            //生成新坐标，成为蛇头
            Snakebody s = new Snakebody(Color.Red, this.size, p);

            //如果下一个坐标不是当前食物坐标，那么删除最后一节蛇
            if (this.food.Point != p)
                this.body.RemoveAt(this.body.Count - 1);

            //如果下一个坐标和食物坐标重合，那么就生成一个新食物，即“吃到食物”
            else
                this.food = this.Getfood();

            //把下一个坐标插入到蛇块列表的第一个，使其成为蛇头
            this.body.Insert(0, s);
            this.Paint(this.Palette);//更新画板

        }
        public void Paint(Graphics p)
        {
            p.Clear(this.bgcolor);//背景色清空画布
            this.food.Paint(p);//画食物
            foreach (Snakebody s in this.body)//通过for循环将贪吃蛇的每个蛇块画在画布上
                s.Paint(p);
        }
    }
}
