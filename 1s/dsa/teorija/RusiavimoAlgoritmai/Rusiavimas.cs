using System;

public class Rikiavimas{
	public static void Main(){
		//Išrinkimo algoritmas:
		int[] rinkinys = new int[5] { 7, 4, 9, 16, 42 };
		rinkinys = Isrinkimo (rinkinys);
		for (int j = 0; j < rinkinys.Length; j++) {
			Console.WriteLine ("Isrinkimo: " + rinkinys[j]);
		}
	}

//Elementarus rusiavimo algoritmai.
	//Išrinkimo algoritmas. Selection sort.
	static int[] Isrinkimo(int[] masyvas){
		int max, temp, id;
		for (int i = 0; i < masyvas.Length; i++) {
			max = masyvas [i];
			id = i;
			for (int j = i; j < masyvas.Length; j++) {
				if (max < masyvas [j]) {
					max = masyvas [j];
					id = j;
				}
			}
			temp = masyvas[i];
			masyvas[i] = max;
			masyvas[id] = temp;
		}
		return masyvas;
	}
	//Burbulo algoritmas:

	//Iterpimo algoritmas:

	//Šelo algoritmas:


}
