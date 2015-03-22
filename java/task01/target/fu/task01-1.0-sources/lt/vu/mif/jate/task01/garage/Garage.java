/*
 *
 */
package lt.vu.mif.jate.task01.garage;

import java.util.Calendar;

import lt.vu.mif.jate.task01.garage.car.Bus;
import lt.vu.mif.jate.task01.garage.car.FamilyCar;
import lt.vu.mif.jate.task01.garage.car.SportsCar;

/**
 * @author john Class Garrage
 */
public final class Garage {

    /**
     * final int size.
     */
    private final int size;
    /**
     * int cnt.
     */
    private int cnt = 0;
    /**
     * final Car[] cars.
     */
    private final Car[] cars;

    /**
     * @param i
     *            the i
     */
    public Garage(final int i) {
        this.size = i;
        cars = new Car[i];
    }

    /**
     * @param newCar
     *            the newCar
     */
    public void add(final Car newCar) {
        for (int i = 0; i < size; i++) {
            if (cars[i] == null) {
                cars[i] = newCar;
                cnt++;
                break;
            }
        }
    }

    /**
     * @param i
     *            the i
     * @return the return
     */
    public Car get(final int i) {
        return cars[i - 1];
    }

    /**
     * @return the return
     */
    public int getCount() {
        return cnt;
    }

    /**
     * @return the return
     */
    public int getFreePlaces() {
        return size - cnt;
    }

    /**
     * @return the return
     */
    public int getSize() {
        return this.size;
    }

    /**
     * @return the return
     */
    public Car pickACar() {
        return pickACar(Calendar.getInstance());
    }

    /**
     * @param day
     *            the day
     * @return the return
     */
    public Car pickACar(final Calendar day) {

        int dayOfWeek = day.get(Calendar.DAY_OF_WEEK);
        if (dayOfWeek == Calendar.SATURDAY
                || dayOfWeek == Calendar.SUNDAY) {
            for (int i = 0; i < size; i++) {
                if (cars[i] instanceof SportsCar) {
                    return cars[i];
                }
            }
            for (int i = 0; i < size; i++) {
                if (cars[i] instanceof Car) {
                    return cars[i];
                }
            }
            if (dayOfWeek == Calendar.SATURDAY
                    || dayOfWeek == Calendar.SUNDAY) {
                return null;
            } else {
                return new Bus("Bus");
            }
        } else {
            for (int i = 0; i < size; i++) {
                if (cars[i] instanceof FamilyCar) {
                    return cars[i];
                }
            }
            for (int i = 0; i < size; i++) {
                if (cars[i] instanceof Car) {
                    return cars[i];
                }
            }
            if (dayOfWeek == Calendar.SATURDAY
                    || dayOfWeek == Calendar.SUNDAY) {
                return null;
            } else {
                return new Bus("Bus");
            }
        }
    }

    /**
     * @param car
     *            the car
     * @return the return
     */
    public Object remove(final Car car) {
        try {
            for (int i = 0; i < size; i++) {
                if (car == cars[i]) {
                    return cars[i];
                }
            }
            return null;
        } finally {
            for (int i = 0; i < size; i++) {
                if (car == cars[i]) {
                    cars[i] = null;
                    cnt--;
                    break;
                }
            }
        }
    }

    /**
     * @param i
     *            the i
     * @return the return
     */
    public Object remove(final int i) {
        try {
            return cars[i - 1];
        } finally {
            if (cars[i - 1] != null) {
                cnt--;
            }
            cars[i - 1] = null;
        }
    }

    /**
     *
     */
    public void removeAll() {
        this.cnt = 0;
        for (int i = 0; i < size; i++) {
            this.cars[i] = null;
        }
    }

}
