from mpi4py import MPI
from sys import path

def funkcija(p):
	return (p[0] + p[3])**4 + (p[1])**2 + (p[2] - 1)**2 - 1

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
		
def main(p): 
	kinta = True
	while kinta:
		p, kinta = nuolidis(p)
	print p

p0 = [4.01, 4.02, 4.04, 4.01]
main(p0)
			
# Nuskaitome parametrus is failo:
parametrai = []
parametru_failas = open( str(path[0]) + '/parametru_failas.txt', 'r')
lines = parametru_failas.readlines()
parametru_failas.close()
for line in lines:
	rs = map(float, line.split())
	if len(rs) == 4: 
		parametrai.append(rs)
print parametrai

# Paskirstome procesus:
comm = MPI.COMM_WORLD
if comm.size == len(parametrai):
	main(parametrai[comm.rank])
	#print 'Rank ' + str(comm.rank) + ' Parametrai :' \
	#+ str(parametrai[comm.rank])
elif comm.size > len(parametrai):
	if comm.rank < len(parametrai):
		#print 'Rank ' + str(comm.rank) + ' Parametrai ' \
		#+ str(parametrai[comm.rank])
		main(parametrai[comm.rank])
else:
	daliklis = len(parametrai) / comm.size  
	liekana  = len(parametrai) % comm.size  
	if comm.rank < len(parametrai) % comm.size:
		for i in range(daliklis + liekana):
			#print 'Rank ' + str(comm.rank) + \
			#' Parametrai ' +  \
			#str(parametrai[comm.rank + i * comm.size]) \
			#+ '\n'
			main(parametrai[comm.rank + i * comm.size])
	else:
		for i in range(daliklis):
			#print 'Rank ' + str(comm.rank) + \
			#' Parametrai ' + \
			#str(parametrai[comm.rank + i * comm.size]) \
			# + '\n'
			main(parametrai[comm.rank + i * comm.size])
