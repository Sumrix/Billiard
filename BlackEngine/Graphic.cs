using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace BlackEngine
{
    public class Graphic
    {
        private float red, green, blue;

        public void SetColor(float red, float green, float blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public void DrawFillCircle(double cx, double cy, double r, int num_segments)
        {
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            Gl.glColor3f(red, green, blue);

            for (int ii = 0; ii < num_segments; ii++)
            {
                double theta = 2.0f * 3.1415926f * (double)ii / (double)(num_segments);//get the current angle 

                double x = r * Math.Cos(theta);//calculate the x component 
                double y = r * Math.Sin(theta);//calculate the y component 

                Gl.glVertex2d(x + cx, y + cy);//output vertex 

            }
            Gl.glEnd();
        }

        public void DrawCircle(double cx, double cy, double r, int num_segments)
        {
            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glColor3f(red, green, blue);

            for (int ii = 0; ii < num_segments; ii++)
            {
                double theta = 2.0f * 3.1415926f * (double)ii / (double)(num_segments);//get the current angle 

                double x = r * Math.Cos(theta);//calculate the x component 
                double y = r * Math.Sin(theta);//calculate the y component 

                Gl.glVertex2d(x + cx, y + cy);//output vertex 

            }
            Gl.glEnd();
        }

        public void DrawRectangle(double top, double bottom, double left, double right)
        {
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glColor3f(red, green, blue);

            Gl.glVertex2d(left, top);
            Gl.glVertex2d(right, top);
            Gl.glVertex2d(right, bottom);
            Gl.glVertex2d(left, bottom);

            Gl.glEnd();
        }

        // фукнция визуализации текста 
        public void PrintText2D(double x, double y, string text)
        {
            // устанавливаем позицию вывода растровых символов 
            // в переданных координатах x и y. 
            Gl.glColor3f(red, green, blue);
            Gl.glRasterPos2d(x, y);

            // в цикле foreach перебираем значения из массива text, 
            // который содержит значение строки для визуализации 
            foreach (char char_for_draw in text)
            {
                // визуализируем символ c, с помощью функции glutBitmapCharacter, используя шрифт GLUT_BITMAP_9_BY_15. 
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, char_for_draw);
            }
            //Gl.glEnd();
        }

        public void DrawDashedLine(List<Point2d> polygon)
        {
            Gl.glLineStipple(5, 0xAAAA);
            Gl.glEnable(Gl.GL_LINE_STIPPLE);
            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glColor3f(red, green, blue);

            foreach (var p in polygon)
                Gl.glVertex2d(p.x, p.y);
            Gl.glEnd();
            Gl.glDisable(Gl.GL_LINE_STIPPLE);
        }
    }
}
