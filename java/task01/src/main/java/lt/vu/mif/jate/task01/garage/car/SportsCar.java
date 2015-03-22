/*
 *
 */

package lt.vu.mif.jate.task01.garage.car;

import lt.vu.mif.jate.task01.garage.Car;

/**
 * @author john
 */
public class SportsCar implements Car {
    /**
     * Sring name.
     */
    private String name;

    /**
     * @param s
     *            The s
     */
    public SportsCar(final String s) {
        this.setName(s);
    }

    /*
     * String. GetName. lt.vu.mif.jate.task01.garage.Car#getName()
     */
    @Override
    public final String getName() {
        return name;
    }

    /**
     * @param newName
     *            The newName
     */
    public final void setName(final String newName) {
        this.name = newName;
    }

}
