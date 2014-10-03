int NuosekliPaieska(internal[] masyvas, int IeskomaVerte){
	for (int i = 0 ; i < masyvas.Lenght - 1 ; i++){
		if (masyvas[i] == IeskomaVerte)
			return i;
	}
	return -1;
}