using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kelias
{
    public partial class Form1 : Form
    {   

        List<List<int>> medis = new List<List<int>>();
        List<List<int>> pagalbine = new List<List<int>>();
        int pradinis_atstumas = -10;

        public Form1()
        {
            InitializeComponent();
            //Pradinės medžio reikšmės:
            this.medis.Add(new List<int>(new int[] { -1, -1, -1, -1 }));
            //Pagalbinės matricos reikšmės:
            this.pagalbine.Add(new List<int>(new int[] { 0, 0 }));
        }

        private void iterpimas_Click(object sender, EventArgs e)
        {
            if (!((Convert.ToInt32(numericUpDown1.Value) == 0) && (Convert.ToInt32(numericUpDown2.Value) == 0)))
            {
                Kliutis nauja_kliutis = new Kliutis(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), 1);
                Algoritmai.Iterpimas(medis, nauja_kliutis, 0, pagalbine);
            }
            Piesti_medi(medis, 0, pagalbine);
        }

        private void reset_Click(object sender, EventArgs e)
        {   
            //Disable 'Terpti kliuti' mygtuka:
            iterpimas.Enabled = true;
            this.CreateGraphics().Clear(Color.Silver);
            //Resetinam Paramettrus
            medis = new List<List<int>>();
            medis.Add(new List<int>(new int[] { -1, -1, -1, -1 }));
            pagalbine = new List<List<int>>();
            this.pagalbine.Add(new List<int>(new int[] { 0, 0 }));
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            iterpimas.Enabled = false;
            this.CreateGraphics().Clear(Color.Silver);

            //Uzpildome kelia atstumais ir piesiame atstumus:
            List<List<int>> PradinePagalbine = Algoritmai.Pradine(medis, pradinis_atstumas);
            Algoritmai.AtstumuUzpildymas(PradinePagalbine, medis, 0, pagalbine, pradinis_atstumas);
            Piesti_medi(medis, 0, pagalbine);

            //Galutine kelio koordinate:
            int x = Convert.ToInt32(numericUpDown3.Value);
            int y = Convert.ToInt32(numericUpDown4.Value);
            int min = 0;

            List<List<int>> GalutinioTaskoPagalbine = new List<List<int>>();
            GalutinioTaskoPagalbine.Add(new List<int>(new int[] { 0, 0 }));
            Algoritmai.GalutinioPagalbine(medis, GalutinioTaskoPagalbine, 0, x, y, ref min);

            List<double> taskas = new List<double>();
            List<List<int>> minKaimynas = new List<List<int>>();

            //Randame kelia:
            List<List<double>> kelias = new List<List<double>>(); 
            kelias.Add(new List<double>(new double[] { x, y }));

            while (!Algoritmai.Palyginti(PradinePagalbine, GalutinioTaskoPagalbine))
            {
                Algoritmai.RastiMinKaimyna(medis, pagalbine, 0, ref minKaimynas, GalutinioTaskoPagalbine, ref taskas, ref min);
                if ((min == 0)||(min == -1))
                {
                    break;
                }
                kelias.Add(taskas);
                GalutinioTaskoPagalbine = new List<List<int>>(minKaimynas);
            }
            kelias.Add(new List<double>(new double[] { 0, 0 }));

            if (!(min == 0) || (min == -1))
            {
                //Piesiame kelia:
                for (int k = 0; k < kelias.Count - 1; k++)
                {
                    Pen blackPen = new Pen(Color.Red, 3);
                    CreateGraphics().DrawLine( blackPen,
                        Convert.ToInt32(30 * kelias[k][0] + 25),
                        Convert.ToInt32(30 * kelias[k][1] + 25),
                        Convert.ToInt32(30 * kelias[k + 1][0] + 25),
                        Convert.ToInt32(30 * kelias[k + 1][1] + 25));
                }
            }

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Piesiame medi su win forms:
        void Piesti_medi(List<List<int>> medis, int id, List<List<int>> pagalbine)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 0) { pagalbine.Add(new List<int>(new int[] { 0, 0 })); }
                if (i == 1) { pagalbine.Add(new List<int>(new int[] { 1, 0 })); }
                if (i == 2) { pagalbine.Add(new List<int>(new int[] { 0, 1 })); }
                if (i == 3) { pagalbine.Add(new List<int>(new int[] { 1, 1 })); }

                Blokas blokas = new Blokas(pagalbine);
               
                int dydis = 30;
                int poslinkis = 25;

                if (medis[id][i] > 0)
                {
                    Piesti_medi(medis, medis[id][i], pagalbine);
                }
                else if (medis[id][i] == 0)
                {
                    SolidBrush blueBrush = new SolidBrush(Color.Black);
                    this.CreateGraphics().FillRectangle(blueBrush, dydis * blokas.getX() + poslinkis, dydis * blokas.getY() + poslinkis, dydis * blokas.getD(), dydis * blokas.getD());
                }
                else
                {
                    Pen blackPen = new Pen(Color.Black, 3);
                    this.CreateGraphics().DrawRectangle(blackPen, dydis * blokas.getX() + poslinkis, dydis * blokas.getY() + poslinkis, dydis * blokas.getD(), dydis * blokas.getD());
                    RectangleF drawRect = new RectangleF(dydis * blokas.getX() + poslinkis, dydis * blokas.getY() + poslinkis, dydis * blokas.getD(), dydis * blokas.getD());
                    Font drawFont = new Font("Arial", 12);
                    String drawString = Convert.ToString(medis[id][i]);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    this.CreateGraphics().DrawString(drawString, drawFont, drawBrush, drawRect);
                }
                pagalbine.RemoveAt(pagalbine.Count - 1);
            }
        }

        
    }


}
