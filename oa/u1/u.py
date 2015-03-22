from mpi4py import MPI
from sys import path
import re


def funk(x1, x2, x3, alpha):
	return (x1 + alpha)**4 + (x2)**2 + (x3 - 1)**2 - 1

# Nuskaitome parametrus is failo:
parametrai = []
parametru_failas = open( str(path[0]) + '/parametru_failas.txt', 'r')
lines = parametru_failas.readlines()
parametru_failas.close()
for line in lines:
	rs = re.findall(r'\d+.\d+', line)
	if len(rs) == 4: 
		parametrai.append(rs)

# Paskirstome procesus:
comm = MPI.COMM_WORLD
if comm.size == len(parametrai):
	print 'Rank ' + str(comm.rank) + ' Parametrai :' \
	+ str(parametrai[comm.rank])
elif comm.size > len(parametrai):
	if comm.rank < len(parametrai):
		print 'Rank ' + str(comm.rank) + ' Parametrai ' \
		+ str(parametrai[comm.rank])
else:
	daliklis = len(parametrai) / comm.size  
	liekana  = len(parametrai) % comm.size  
	if comm.rank < len(parametrai) % comm.size:
		for i in range(daliklis + liekana):
			print 'Rank ' + str(comm.rank) + \
			' Parametrai ' +  \
			str(parametrai[comm.rank + i * comm.size]) \
			+ '\n'
	else:
		for i in range(daliklis):
			print 'Rank ' + str(comm.rank) + \
			' Parametrai ' + \
			str(parametrai[comm.rank + i * comm.size]) \
			 + '\n'

