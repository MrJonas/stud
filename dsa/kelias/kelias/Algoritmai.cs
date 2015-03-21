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
    public class Algoritmai
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Medodas, skirtas įterpti kliutį į ketvirtainį medį:
        public static void Iterpimas(List<List<int>> medis, Kliutis kliutis, int id, List<List<int>> pagalbine)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 0) { pagalbine.Add(new List<int>(new int[] { 0, 0 })); }
                if (i == 1) { pagalbine.Add(new List<int>(new int[] { 1, 0 })); }
                if (i == 2) { pagalbine.Add(new List<int>(new int[] { 0, 1 })); }
                if (i == 3) { pagalbine.Add(new List<int>(new int[] { 1, 1 })); }


                Blokas blokas = new Blokas(pagalbine);

                if (blokas.ArSutampa(kliutis))
                {
                    medis[id][i] = 0;
                }
                else if (blokas.ArViduje(kliutis))
                {
                    if (medis[id][i] != 0)
                    {
                        if (medis[id][i] < 0)
                        {
                            medis[id][i] = medis.Count;
                            id = medis.Count;
                            List<int> tuscias = new List<int>(new int[] { -1, -1, -1, -1 });
                            medis.Add(tuscias);
                            Iterpimas(medis, kliutis, id, pagalbine);
                        }
                        else
                        {
                            id = medis[id][i];
                            Iterpimas(medis, kliutis, id, pagalbine);
                        }
                    }
                }
                pagalbine.RemoveAt(pagalbine.Count - 1);
            }
        }

       
       
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Uzpildome atstumus:
        public static void AtstumuUzpildymas(List<List<int>> BlokoPagalbine, List<List<int>> medis, int id, List<List<int>> pagalbine, int atstumas)
        {

            Blokas blokas = new Blokas(BlokoPagalbine);

            for (int i = 0; i < 4; i++)
            {

                if (i == 0) { pagalbine.Add(new List<int>(new int[] { 0, 0 })); }
                if (i == 1) { pagalbine.Add(new List<int>(new int[] { 1, 0 })); }
                if (i == 2) { pagalbine.Add(new List<int>(new int[] { 0, 1 })); }
                if (i == 3) { pagalbine.Add(new List<int>(new int[] { 1, 1 })); }

                Blokas kitasBlokas = new Blokas(pagalbine);

                if (blokas.ArKaimynas(kitasBlokas))
                {
                    if (((medis[id][i] < 0) && (medis[id][i] <= atstumas - kitasBlokas.getD())) || (medis[id][i] == -1))
                    {
                        medis[id][i] = atstumas - kitasBlokas.getD();

                        //Kvieciame f-ja naujam kaimynui:
                        List<List<int>> nauja = new List<List<int>>();
                        List<int> subNauja = new List<int>(new int[] { 0, 0 });
                        nauja.Add(subNauja);

                        AtstumuUzpildymas(pagalbine, medis, 0, nauja, medis[id][i]);

                    }
                    else if (medis[id][i] > 0)
                    {
                        AtstumuUzpildymas(BlokoPagalbine, medis, medis[id][i], pagalbine, atstumas);
                    }
                }
                else if ((kitasBlokas.ArViduje(blokas)) && (medis[id][i] > 0) && !(kitasBlokas.ArSutampa(blokas)))
                {
                    AtstumuUzpildymas(BlokoPagalbine, medis, medis[id][i], pagalbine, atstumas);
                }
                
                pagalbine.RemoveAt(pagalbine.Count - 1);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Randame pradinio bloko (0,0) pagalbine matrica:
        public static List<List<int>> Pradine(List<List<int>> medis, int pradinis_atstumas)
        {
            List<List<int>> pagalbine = new List<List<int>>();
            List<int> subPagalbine = new List<int>(new int[] { 0, 0 });
            pagalbine.Add(subPagalbine);
            int id = medis[0][0];
            int id_pask = 0;
            if (medis[0][0] < 0)
            {
                id_pask = 0;
            }
            else
            {
                id_pask = medis[0][0];
            }
            while (id > 0)
            {
                pagalbine.Add(subPagalbine);
                id = medis[id][0];
                if (id > 0)
                {
                    id_pask = id;
                }
            }
            pagalbine.Add(subPagalbine);
            medis[id_pask][0] = pradinis_atstumas;
            return pagalbine;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Galutinio tasko pagalbine matrica:
        public static void GalutinioPagalbine(List<List<int>> medis,List<List<int>> pagalbine,int id, int x, int y)
        {
            Blokas blokas = new Blokas(pagalbine);

            for (int i = 0; i < 4; i++)
            {

                if (i == 0) { pagalbine.Add(new List<int>(new int[] { 0, 0 })); }
                if (i == 1) { pagalbine.Add(new List<int>(new int[] { 1, 0 })); }
                if (i == 2) { pagalbine.Add(new List<int>(new int[] { 0, 1 })); }
                if (i == 3) { pagalbine.Add(new List<int>(new int[] { 1, 1 })); } 

                if (blokas.ArTaskasViduje(x, y)) {
                    if (medis[id][i] > 0) {
                        GalutinioPagalbine(medis, pagalbine, medis[id][i], x, y);
                    }
                    break;
                }
                pagalbine.RemoveAt(pagalbine.Count - 1);
            }
        }
        

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Paliginti dviem sarasams
        public static bool Palyginti(List<List<int>> a, List<List<int>> b)
        {
            if (a.Count == b.Count)
            {
                for (int k = 0; k < a.Count; k++)
                {
                    if (a[k].Count == b[k].Count)
                    {
                        for (int l = 0; l < a[k].Count; l++)
                        {
                            if (a[k][l] != b[k][l])
                            {
                                return false;
                            }
                        }
                    }
                    else { return false; }
                }

            }
            else { return false; }
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Randame minimalu bloko kaimyna taskus:
        public static void RastiMinKaimyna(List<List<int>> medis, List<List<int>> pagalbine, int id, ref List<List<int>> minKaimynas, List<List<int>> galutine, ref List<double> taskas, ref int min)
        {
            Blokas blokas = new Blokas(galutine);

            for (int i = 0; i < 4; i++)
            {
                if (i == 0) { pagalbine.Add(new List<int>(new int[] { 0, 0 })); }
                if (i == 1) { pagalbine.Add(new List<int>(new int[] { 1, 0 })); }
                if (i == 2) { pagalbine.Add(new List<int>(new int[] { 0, 1 })); }
                if (i == 3) { pagalbine.Add(new List<int>(new int[] { 1, 1 })); }

                Blokas kitasblokas = new Blokas(pagalbine);

                if (blokas.ArKaimynas(kitasblokas))
                {
                    if (medis[id][i] > 0)
                    {
                        RastiMinKaimyna(medis, pagalbine, medis[id][i], ref minKaimynas, galutine, ref taskas, ref min);

                    }
                    else if ((medis[id][i] < 0) && (medis[id][i] > min)&&(min !=  -1 ))
                    {
                        min = medis[id][i];
                        taskas = new List<double>(blokas.RastiTaska(kitasblokas));
                        minKaimynas = new List<List<int>>(pagalbine);
                    }
                }
                else if ((kitasblokas.ArViduje(blokas)) && (medis[id][i] > 0) && !(kitasblokas.ArSutampa(blokas)))
                {
                    RastiMinKaimyna(medis, pagalbine, medis[id][i], ref minKaimynas, galutine, ref taskas, ref min);
                }
                pagalbine.RemoveAt(pagalbine.Count - 1);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Galutinio tasko pagalbine matrica:
        public static void GalutinioPagalbine(List<List<int>> medis, List<List<int>> pagalbine, int id, int x, int y, ref int min)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 0) { pagalbine.Add(new List<int>(new int[] { 0, 0 })); }
                if (i == 1) { pagalbine.Add(new List<int>(new int[] { 1, 0 })); }
                if (i == 2) { pagalbine.Add(new List<int>(new int[] { 0, 1 })); }
                if (i == 3) { pagalbine.Add(new List<int>(new int[] { 1, 1 })); }

                Blokas blokas = new Blokas(pagalbine);

                if (blokas.ArTaskasViduje(x, y))
                {
                    if (medis[id][i] > 0)
                    {
                        GalutinioPagalbine(medis, pagalbine, medis[id][i], x, y, ref min);
                    }
                    else
                    {
                        min = medis[id][i];
                    }
                    break;
                }
                pagalbine.RemoveAt(pagalbine.Count - 1);
            }
        }


   
    }
}