import threading as tdn
import numpy.random as rnd
import time


def first_tread_run():
    i = 0
    while True:
        if time.clock() > i:
            r_val = float(rnd.rand(1))
            print("The random value from [0;1] is: %f" % r_val)
            i += 2


def second_tread_run():
    i = 0
    while True:
        if time.clock() > i:
            local_time_val = time.ctime()
            print("The local time is: %s" % local_time_val)
            i += 11

first_thread = tdn.Thread(target=first_tread_run)
second_thread = tdn.Thread(target=second_tread_run)
first_thread.start()
second_thread.start()
