﻿namespace DiceStg_Online
{
    /// <summary>
    /// 場所を表す構造体
    /// </summary>
    class Point
    {
        /// <summary>
        /// X座標の位置
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y座標の位置
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 座標の指定して初期化する
        /// </summary>
        /// <param name="x">x座標の初期値</param>
        /// <param name="y">y座標の初期値</param>
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public static Point operator-- (Point p)
        {
            p.X--;
            p.Y--;

            return p;
        }

        public static Point operator++ (Point p)
        {
            p.X++;
            p.Y++;

            return p;
        }

        public static Point operator+ (Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator+ (Point p, int v)
        {
            return new Point(p.X + v, p.Y + v);
        }

        public static Point operator- (Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator-(Point p, int v)
        {
            return new Point(p.X - v, p.Y - v);
        }
    }
}
