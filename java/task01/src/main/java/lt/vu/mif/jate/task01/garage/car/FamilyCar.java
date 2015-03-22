/*
 *
 */
package lt.vu.mif.jate.task01.garage.car;

import lt.vu.mif.jate.task01.garage.Car;

/**
 * @author john
 */

public class FamilyCar implements Car {

    /**
     * String name.
     */
    private String name;

    /**
     * @param s
     *            The s
     */
    public FamilyCar(final String s) {
        this.setName(s);
    }

    /*
     * String. GetName. lt.vu.mif.jate.task01.garage.Car#getName()
     */
    @Override
    public final String getName() {
        return this.name;
    }

    /**
     * @param newName
     *            the newName
     */
    public final void setName(final String newName) {
        this.name = newName;
    }
}
