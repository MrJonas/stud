using System;

public class Rikiavimas{
	public static void Main(){
		//Išrinkimo algoritmas:
		int[] rinkinys = new int[5] { 7, 4, 9, 16, 42 };
		rinkinys = Isrinkimo (rinkinys);
		Console.WriteLine ("Isrinkimo: " );
		for (int j = 0; j < rinkinys.Length; j++) {
			Console.WriteLine (rinkinys[j] + " ");
		}
		//Iterpimo algoritmas:
		int[]rinkinys2 = new int[5] { 7, 4, 9, 16, 42 };
		rinkinys2 = Iterpimo (rinkinys2);
		Console.WriteLine ("Iterpimo: ");
		for (int j = 0; j < rinkinys2.Length; j++) {
			Console.WriteLine (rinkinys2[j] + " ");
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

	//Iterpimo algoritmas:
	static int[] Iterpimo(int[] masyvas){
		for (int i = 0; i < masyvas.Length; i++) {
			int j = i;
			int temp = masyvas [i];
			while (j < masyvas.Length - 1 && temp > masyvas[j+1]){
				masyvas [j] = masyvas [j + 1];
				j++;
				}
			masyvas [j] = temp;
			}
		return masyvas;
		}	
	//Šelo algoritmas:
	//Burbulo algoritmas:



}
