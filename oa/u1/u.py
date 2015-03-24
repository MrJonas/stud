from mpi4py import MPI
from sys import path

# Argumentai:
max_zingsnis = 1.0
min_zingsnis = 0.01
direktorija = '/Rezultatai/'

# Minimizuojama funkcija:
def funkcija(p):
	return (p[0] + p[3])**4 + (p[1])**2 + (p[2] - 1)**2 - 1

# Nuolidzio pakoordinaciui metodo viena iteracija:
def nuolidis(p):
	kinta = False
	for i in range(3):
		h = 1.0
		while (h >= 0.01):
			p_minus = list(p)
			p_plius = list(p)
			p_minus[i] = p_minus[i] - h
			p_plius[i] = p_plius[i] + h
			if ( funkcija(p) > funkcija(p_plius) ):
				while( funkcija(p) > funkcija(p_plius) ):
					p = list(p_plius)
					p_plius[i] = p_plius[i] + h 
				kinta = True
			elif ( funkcija(p) > funkcija(p_minus) ):
				while( funkcija(p) > funkcija(p_minus) ):
					p = list(p_minus)
					p_minus[i] = p_minus[i] - h
				kinta = True
			h = h / 10
	return list(p), kinta

def save_results(id_r, rezultatai):
	failas = str(path[0]) + direktorija + str(id_r) + '_rezultatai.txt'
	f = open(failas, 'w')
	for item in rezultatai:
		f.write(str(item) + ' ')
	f.close
	

comm = MPI.COMM_WORLD
		
def main(p): 
	kinta = True
	while kinta:
		p, kinta = nuolidis(p)
	print str(comm.rank) + ' ' + str(p) + '\n'
	return p

			
# Nuskaitome parametrus is failo:
parametrai = []
parametru_failas = open( str(path[0]) + '/parametru_failas.txt', 'r')
lines = parametru_failas.readlines()
parametru_failas.close()
for line in lines:
	rs = map(float, line.split())
	if len(rs) == 4: 
		parametrai.append(rs)

# Paskirstome procesus:
if comm.size == len(parametrai):
	minimumas = main(parametrai[comm.rank])
	save_results(comm.rank, minimumas)
elif comm.size > len(parametrai):
	if comm.rank < len(parametrai):
		minimumas = main(parametrai[comm.rank])
		save_results(comm.rank, minimumas)
else:
	daliklis = len(parametrai) / comm.size  
	liekana  = len(parametrai) % comm.size  
	if comm.rank < len(parametrai) % comm.size:
		for i in range(daliklis + liekana):
			minimumas = main(parametrai[comm.rank + i * comm.size])
			save_results(comm.rank + i * comm.size, minimumas)
	else:
		for i in range(daliklis):
			minimumas = main(parametrai[comm.rank + i * comm.size])
			save_results(comm.rank + i * comm.size, minimumas)
