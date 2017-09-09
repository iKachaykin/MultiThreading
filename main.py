import threading as tdn
import numpy.random as rnd
import time


def first_tread_run():
    for j in range(10):
        i = float(rnd.rand(1))
        print("The random value from [0;1] is: %f" % i)
    return 0


def second_tread_run():
    for j in range(10):
        res = time.ctime()
        print("The time is: %s" % res)
    return 0

first_thread = tdn.Thread(target=first_tread_run)
second_thread = tdn.Thread(target=second_tread_run)
first_thread.start()
second_thread.start()
