using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace lab_1
{
    public partial class Form1 : Form
    {

        public class Cell
        {
            int Color;
            char Type;

            public Cell()
            {

            }

            public void setColor(int color)
            {
                Color = color;
            }
            public void setType(char type)
            {
                Type = type;
            }
            public int getColor()
            {
                return Color;
            }
            public char getType()
            {
                return Type;
            }

        }

        Bitmap bitmap;
        Graphics graphics;
        int cellSize, nX, nY;
        int pX = 0, pY = 0;
        int vX = 1, vY = 0;
        Cell[,] field1;
        Thread stepThread;
        bool runned = false;
        bool clicked = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void set(object sender, EventArgs e)
        {
            button2.Text = "run";
            stepThread = new Thread(stepThr);
            runned = false;
            clicked = false;
            if(textBox1.Text == "")
            {
                textBox1.Text = "10";
                nX = 11;
                nY = nX;
            }
            else
            {
                nX = 1 + Convert.ToInt32(textBox1.Text);
                nY = nX;
            }
            
            pX = 0;
            pY = 0;
            vX = 1;
            vY = 0;
            cellSize = pictureBox.Width / nX;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            field1 = new Cell[nX, nY];
            for (int i = 0; i < nX; i++)
            {
                for (int j = 0; j < nY; j++)
                {
                    field1[i, j] = new Cell();
                    field1[i, j].setColor(-1);
                }
            }
            graphics = Graphics.FromImage(bitmap);
            Pen p = new Pen(Color.Black);
            for (int y = 0; y < nY; ++y)
            {
                graphics.DrawLine(p, 0, y * cellSize, (nX - 1) * cellSize, y * cellSize);
            }

            for (int x = 0; x < nX; ++x)
            {
                graphics.DrawLine(p, x * cellSize, 0, x * cellSize, (nY - 1) * cellSize);
            }
            pictureBox.Image = bitmap;
        }

        private void stepThr()
        {
            while (runned)
            {
                step();
                Thread.Sleep(10);
            }
        }

        private void run(object sender, EventArgs e)
        {
            if (runned)
            {
                button2.Text = "run";
                stepThread = new Thread(stepThr);
            }
            else
            {
                stepThread = new Thread(stepThr);
                stepThread.Start();
                button2.Text = "stop";
            }
            runned = !runned;
        }

        private void Step(object sender, EventArgs e)
        {
            //textBox1.Text = pX.ToString() + " " + pY.ToString();
            step();

        }

        private void step()
        {
            Cell[,] field2 = new Cell[nX, nY];
            for(int i = 0; i < nX; i++)
            {
                for(int j = 0; j < nY; j++)
                {
                    field2[i, j] = field1[i, j];
                }
            }
            
            if(field2[pX, pY].getColor() % 3 == 0)
            {
                if(field2[pX, pY].getType() == 'A')
                {
                    Turn('r');
                }
                else
                {
                    Turn('l');
                }
            }
            else
            {
                if (field2[pX, pY].getColor() % 3 == 1)
                {
                    if (field2[pX, pY].getType() == 'A')
                    {
                        Turn('f');
                    }
                    else
                    {
                        Turn('f');
                    }
                }
                else
                {
                    if (field2[pX, pY].getType() == 'A')
                    {
                        Turn('l');
                    }
                    else
                    {
                        Turn('r');
                    }
                }
            }
            //textBox1.Text = pX.ToString() + " " + pY.ToString();
            if(pX + vX >= nX - 1 || pX + vX < 0 || pY + vY >= nY - 1 || pY + vY < 0)
            {
                vX *= -1;
                vY *= -1;
            }

            pX += vX;
            pY += vY;
            
            switch (field2[pX, pY].getColor())
            {
                case -1:
                    field2[pX, pY].setColor(0);
                    field2[pX, pY].setType('A');
                    graphics.FillRectangle(new SolidBrush(Color.Black), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 0:
                    field2[pX, pY].setColor(1);
                    graphics.FillRectangle(new SolidBrush(Color.Silver), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 1:
                    field2[pX, pY].setColor(2);
                    graphics.FillRectangle(new SolidBrush(Color.Gray), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 2:
                    field2[pX, pY].setColor(3);
                    graphics.FillRectangle(new SolidBrush(Color.Maroon), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 3:
                    field2[pX, pY].setColor(4);
                    graphics.FillRectangle(new SolidBrush(Color.Red), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 4:
                    field2[pX, pY].setColor(5);
                    graphics.FillRectangle(new SolidBrush(Color.Orange), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 5:
                    field2[pX, pY].setColor(6);
                    graphics.FillRectangle(new SolidBrush(Color.Yellow), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 6:
                    field2[pX, pY].setColor(7);
                    graphics.FillRectangle(new SolidBrush(Color.Olive), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 7:
                    field2[pX, pY].setColor(8);
                    field2[pX, pY].setType('B');
                    graphics.FillRectangle(new SolidBrush(Color.Fuchsia), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 8:
                    field2[pX, pY].setColor(9);
                    graphics.FillRectangle(new SolidBrush(Color.Purple), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 9:
                    field2[pX, pY].setColor(10);
                    graphics.FillRectangle(new SolidBrush(Color.Navy), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 10:
                    field2[pX, pY].setColor(11);
                    graphics.FillRectangle(new SolidBrush(Color.Blue), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 11:
                    field2[pX, pY].setColor(12);
                    graphics.FillRectangle(new SolidBrush(Color.Teal), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 12:
                    field2[pX, pY].setColor(13);
                    graphics.FillRectangle(new SolidBrush(Color.Aqua), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 13:
                    field2[pX, pY].setColor(14);
                    graphics.FillRectangle(new SolidBrush(Color.Lime), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 14:
                    field2[pX, pY].setColor(15);
                    graphics.FillRectangle(new SolidBrush(Color.Green), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;
                case 15:
                    field2[pX, pY].setColor(0);
                    graphics.FillRectangle(new SolidBrush(Color.Black), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                    break;


            }
            field1 = field2;
            pictureBox.Image = bitmap;

        }

        private void Turn(char d)
        {
            switch (vX)
            {
                case 1:
                    switch (d)
                    {
                        case 'r':
                            vX = 0;
                            vY = 1;
                            break;
                        case 'f':
                            break;
                        case 'l':
                            vX = 0;
                            vY = -1;
                            break;
                    }
                    break;
                case -1:
                    switch (d)
                    {
                        case 'r':
                            vX = 0;
                            vY = -1;
                            break;
                        case 'f':
                            break;
                        case 'l':
                            vX = 0;
                            vY = 1;
                            break;
                    }
                    break;
                case 0:
                    switch (vY)
                    {
                        case 1:
                            switch (d)
                            {
                                case 'r':
                                    vX = -1;
                                    vY = 0;
                                    break;
                                case 'f':
                                    break;
                                case 'l':
                                    vX = 1;
                                    vY = 0;
                                    break;
                            }
                            break;
                        case -1:
                            switch (d)
                            {
                                case 'r':
                                    vX = 1;
                                    vY = 0;
                                    break;
                                case 'f':
                                    break;
                                case 'l':
                                    vX = -1;
                                    vY = 0;
                                    break;
                            }
                            break;

                    }
                    break;
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && !(char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!clicked)
            {
                clicked = true;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                pX = e.X / cellSize;
                pY = e.Y / cellSize;
                if (pX < nX - 1 && pY < nY - 1)
                {
                    field1[pX, pY].setType('A');
                    field1[pX, pY].setColor(0);
                    graphics.FillRectangle(new SolidBrush(Color.Black), pX * cellSize, pY * cellSize, cellSize - 1, cellSize - 1);
                }

                pictureBox.Image = bitmap;

            }
        }

        private void clear(object sender, EventArgs e)
        {
            button2.Text = "run";
            stepThread = new Thread(stepThr);
            runned = false;
            clicked = false;
            pX = 0;
            pY = 0;
            vX = 1;
            vY = 0;
            cellSize = pictureBox.Width / nX;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            field1 = new Cell[nX, nY];
            for (int i = 0; i < nX; i++)
            {
                for (int j = 0; j < nY; j++)
                {
                    field1[i, j] = new Cell();
                    field1[i, j].setColor(-1);
                }
            }
            graphics = Graphics.FromImage(bitmap);
            Pen p = new Pen(Color.Black);
            for (int y = 0; y < nY; ++y)
            {
                graphics.DrawLine(p, 0, y * cellSize, (nX - 1) * cellSize, y * cellSize);
            }

            for (int x = 0; x < nX; ++x)
            {
                graphics.DrawLine(p, x * cellSize, 0, x * cellSize, (nY - 1) * cellSize);
            }
            pictureBox.Image = bitmap;
        }

        private void save(object sender, EventArgs e)
        {
            bitmap.Save("Bitmap.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }

    }
}
