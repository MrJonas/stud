using System;

public class Paieska
{
	//main
	static void Main(){
		int[] rinkinys = new int[5]{1, 5, 9, 16, 32};
		int nr =  Nuosekli(rinkinys, 16);
		Console.WriteLine("Nuosekli: " + nr);
		nr =  Dvejataine(rinkinys, 32);
		Console.WriteLine ("Dvejatainė: " + nr);
		nr =  Rekursivi(rinkinys, 5, rinkinys.Length -1, 0 );
		Console.WriteLine ("Rekursivi " + nr);
	}
	
    //Dvejataine paieska.
	static int Dvejataine(int[] masyvas, int IeskomaVerte){
		int virsutineRiba = masyvas.Length - 1;
		int apatineRiba = 0;
		while (virsutineRiba >= 1) {
			int indeksas = (apatineRiba + virsutineRiba) / 2;
			if (IeskomaVerte == masyvas[indeksas]) 
				return indeksas;
			if (IeskomaVerte < masyvas [indeksas])
				virsutineRiba = indeksas - 1;
			else
				apatineRiba = indeksas + 1;
		}
		return -1;
	}

	//Rekursivi dvejatainė paieška:
	static int Rekursivi(int[] masyvas, int IeskomaVerte, int Virsus, int Apacia){
		if (Apacia > Virsus)
			return -1;
		else {
			int Viduris = (int)(Virsus + Apacia) / 2;
			if (IeskomaVerte < masyvas [Viduris])
				return Rekursivi(masyvas, IeskomaVerte, Viduris - 1, Apacia);
			else if (IeskomaVerte > masyvas [Viduris])
				return Rekursivi(masyvas, IeskomaVerte, Virsus, Viduris + 1);
			else
				return Viduris;
		}
	}

	//Nuoseklioji paieska
	//Galimi patobulinimai:
	//Galima dažniausiai ieškomus elementus sudėti į masyvo pradžia.
	static int Nuosekli(int[] masyvas, int IeskomaVerte){
		for (int i = 0 ; i < masyvas.Length-1 ; i++){
			if (masyvas[i] == IeskomaVerte)
				return i;
		}
		return -1;
	}
}


