using System;
using System.Collections.Generic;

namespace kelias
{

    public class Blokas
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Blokas is pagalbines medžio matricos sudaromas kvadratinis blokas
        // x, y - viršutinio kairiojo bloko kampo kordinates
        // d - bloko krastines ilgis
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        int x, y, d;

        public int getX() { return this.x; }
        public int getY() { return this.y; }
        public int getD() { return this.d; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Metodas tikrina ar taškas yra bloko viduje:
        public bool ArTaskasViduje(double x0, double y0)
        {
            if (((this.x <= x0) && (this.x + this.d > x0)) && ((this.y <= y0) && (this.y + this.d > y0)))
            {
                return true;
            }
            else { return false; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Metodas tikrina ar kliutis sutampa su bloko objektu:
        public bool ArSutampa(Kliutis A)
        {
            if ((this.x == A.x) && (this.y == A.y) && (this.d == A.d))
            {
                return true;
            }
            else { return false; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Metodas tikrina ar kliutis bloko viduje:
        public bool ArViduje(Kliutis A)
        {
            if ((this.x <= A.x) && (this.x + this.d > A.x) && (this.y <= A.y) && (this.y + this.d > A.y)
                && (this.d - (A.y - this.y) >= A.d) && (this.d - (A.x - this.x) >= A.d))
            {
                return true;
            }
            else { return false; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Metodas tikrina ar kliutis sutampa su bloko objektu:
        public bool ArSutampa(Blokas A)
        {
            if ((this.x == A.x) && (this.y == A.y) && (this.d == A.d))
            {
                return true;
            }
            else { return false; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Metodas tikrina ar kitas blokas yra bloko viduje:
        public bool ArViduje(Blokas A)
        {
            if ((this.x <= A.x) && (this.x + this.d > A.x) && (this.y <= A.y) && (this.y + this.d > A.y)
                && (this.d - (A.y - this.y) >= A.d) && (this.d - (A.x - this.x) >= A.d))
            {
                return true;
            }
            else { return false; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Konstruktorius bloko x, y, d nariams gauti is pagalbines matricos
        // (N + 1) - pirminio kvadrato krastine 
        public Blokas(List<List<int>> pagalbine)
        {

            int N = 4; int x = 0; int y = 0; int d = 0;

            for (int j = 0; j < pagalbine.Count; j++)
            {
                d = Convert.ToInt32(Math.Pow(2, (N - j - 1)));
                x = x + (pagalbine[j][0]) * d;
                y = y + (pagalbine[j][1]) * d;
            }
            d = Convert.ToInt32(Math.Pow(2, (N - pagalbine.Count)));

            this.x = x; this.y = y; this.d = d;
        }


        //Metodas tikrina ar kitas blokas yra sio bloko kaimynas: 
        public bool ArKaimynas(Blokas A)
        {
            // Kaimynas virsuje
            if ((this.y == A.y + A.d) &&
               (((this.x - A.x < A.d) && (this.x - A.x >= 0)) ||
                ((A.x - this.x < this.d) && (A.x - this.x >= 0))))
            {
                return true;
            }
            // Kaimynas apacioje
            else if ((this.y + this.d == A.y) &&
                    (((this.x - A.x < A.d) && (this.x - A.x >= 0)) ||
                     ((A.x - this.x < this.d) && (A.x - this.x >= 0))))
            {
                return true;
            }
            // Kaiminas desineje
            else if ((this.x + this.d == A.x) &&
                    (((this.y - A.y < A.d) && (this.y - A.y >= 0)) ||
                     ((A.y - this.y < this.d) && (A.y - this.y >= 0))))
            {
                return true;
            }
            // Kaimynas kaireje
            else if ((this.x == A.x + A.d) &&
                    (((this.y - A.y < A.d) && (this.y - A.y >= 0)) ||
                     ((A.y - this.y < this.d) && (A.y - this.y >= 0))))
            {
                return true;
            }
            else
            {
                //Nekaimynas
                return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Metodas, kuris randa dviejų bloku lietimosi linijos vidurio taška: 
        public List<double> RastiTaska(Blokas A)
        {
            double X = 0.0;
            double Y = 0.0;
            // Kaimynas virsuje
            if ((this.y == A.y + A.d) &&
               (((this.x - A.x < A.d) && (this.x - A.x >= 0)) ||
                ((A.x - this.x < this.d) && (A.x - this.x >= 0))))
            {
                Y = this.y;
                if ((A.x <= this.x) && (A.x + A.d <= this.x + this.d))
                {
                    X = this.x + (A.x + A.d - this.x) / 2.0;
                }
                if ((A.x <= this.x) && (A.x + A.d > this.x + this.d))
                {
                    X = this.x + (this.d) / 2.0;
                }
                if ((A.x > this.x) && (A.x + A.d <= this.x + this.d))
                {
                    X = A.x + (A.d) / 2.0;
                }
                if ((A.x > this.x) && (A.x + A.d > this.x + this.d))
                {
                    X = A.x  + (this.x + this.d - A.x) / 2.0;
                }
            }
            // Kaimynas apacioje
            else if ((this.y + this.d == A.y) &&
                    (((this.x - A.x < A.d) && (this.x - A.x >= 0)) ||
                     ((A.x - this.x < this.d) && (A.x - this.x >= 0))))
            {
                Y = A.y;
                if ((A.x <= this.x) && (A.x + A.d <= this.x + this.d))
                {
                    X = this.x + (A.x + A.d - this.x) / 2.0;
                }
                if ((A.x <= this.x) && (A.x + A.d > this.x + this.d))
                {
                    X = this.x + (this.d) / 2.0;
                }
                if ((A.x > this.x) && (A.x + A.d <= this.x + this.d))
                {
                    X = A.x + (A.d) / 2.0;
                }
                if ((A.x > this.x) && (A.x + A.d > this.x + this.d))
                {
                    X = A.x + (this.x + this.d - A.x) / 2.0;
                }
            }
            // Kaiminas desineje
            else if ((this.x + this.d == A.x) &&
                    (((this.y - A.y < A.d) && (this.y - A.y >= 0)) ||
                     ((A.y - this.y < this.d) && (A.y - this.y >= 0))))
            {
                X = A.x;
                if ((A.y <= this.y)&&(A.y + A.d <= this.y + this.d))
                {
                    Y = this.y + (A.y + A.d - this.y) / 2.0;
                }
                if ((A.y <= this.y)&&(A.y + A.d > this.y + this.d))
                {
                    Y = this.y + (this.d) / 2.0;
                }
                if ((A.y > this.y)&&(A.y + A.d <= this.y + this.d))
                {
                    Y = A.y + ( A.d) / 2.0;
                }
                if ((A.y > this.y) && (A.y + A.d > this.y + this.d))
                {
                    Y = A.y + (this.y + this.d - A.y) / 2.0;
                }
            }
            // Kaimynas kaireje
            else if ((this.x == A.x + A.d) &&
                    (((this.y - A.y < A.d) && (this.y - A.y >= 0)) ||
                     ((A.y - this.y < this.d) && (A.y - this.y >= 0))))
            {
                X = this.x;
                if ((A.y <= this.y) && (A.y + A.d <= this.y + this.d))
                {
                    Y = this.y + (A.y + A.d - this.y) / 2.0;
                }
                if ((A.y <= this.y) && (A.y + A.d > this.y + this.d))
                {
                    Y = this.y + (this.d) / 2.0;
                }
                if ((A.y > this.y) && (A.y + A.d <= this.y + this.d))
                {
                    Y = A.y + (A.d) / 2.0;
                }
                if ((A.y > this.y) && (A.y + A.d > this.y + this.d))
                {
                    Y = A.y + (this.y + this.d - A.y) / 2.0;
                }
            }

            return new List<double>(new double[] { X, Y });
        }
    }

    public class Kliutis
    {
        // Kliuti aprasanti klase:
        public int x, y, d;

        public Kliutis(int x, int y, int d)
        {
            this.x = x;
            this.y = y;
            this.d = d;
        }
    }
}