from mpi4py import MPI


comm = MPI.COMM_WORLD

if comm.rank == 0:
	print comm.size

print 'Hi from ' + str(comm.rank)


