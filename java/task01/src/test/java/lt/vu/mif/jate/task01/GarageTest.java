package lt.vu.mif.jate.task01;

import java.util.Calendar;

import junit.framework.TestCase;
import lt.vu.mif.jate.task01.garage.Car;
import lt.vu.mif.jate.task01.garage.Garage;
import lt.vu.mif.jate.task01.garage.car.Bus;
import lt.vu.mif.jate.task01.garage.car.FamilyCar;
import lt.vu.mif.jate.task01.garage.car.SportsCar;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;

/**
 * Create class hierarchy that would compile and test.
 * 
 * @author valdo
 */
@RunWith(JUnit4.class)
public class GarageTest {

    /**
     * Some weekday date.
     */
    private static final Calendar WEEKDAY_DAY;

    /**
     * Some weekend date.
     */
    private static final Calendar WEEKEND_DAY;

    /**
     * Set the days statically
     */
    static {
        WEEKDAY_DAY = Calendar.getInstance();
        WEEKDAY_DAY.set(2014, 2, 6);
        WEEKEND_DAY = Calendar.getInstance();
        WEEKEND_DAY.set(2014, 2, 8);
    }

    /**
     * Today's weekend indicator.
     */
    private final boolean todayWeekend;

    /**
     * Constructor.
     */
    public GarageTest() {
        int dayOfWeek = Calendar.getInstance().get(Calendar.DAY_OF_WEEK);
        this.todayWeekend = (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY);
    }

    /**
     * Test garage capacity and removal test
     */
    @Test
    public void capacityTest() {

        // Create and test a parking place
        Garage parkingPlace = new Garage(25);

        TestCase.assertEquals(25, parkingPlace.getSize());
        TestCase.assertEquals(25, parkingPlace.getFreePlaces());
        TestCase.assertEquals(0, parkingPlace.getCount());

        // Add more cars than it fits...
        for (int i = 0; i < 50; i++) {
            if (parkingPlace.getFreePlaces() > 0) {
                final String name = Integer.toString(i);
                parkingPlace.add(
                        new Car() {

                            @Override
                            public String getName() {
                                return name;
                            }

                        }
                        );
            }
        }

        // Test capasity

        TestCase.assertEquals(25, parkingPlace.getSize());
        TestCase.assertEquals(0, parkingPlace.getFreePlaces());
        TestCase.assertEquals(25, parkingPlace.getCount());

        // Get a car by number and test it

        Car car10 = parkingPlace.get(10);
        TestCase.assertNotNull(car10);
        TestCase.assertEquals("9", car10.getName());

        Car car15 = parkingPlace.get(15);
        TestCase.assertNotNull(car15);
        TestCase.assertEquals("14", car15.getName());

        // Remove a car and test

        TestCase.assertSame(car10, parkingPlace.remove(10));
        TestCase.assertNull(parkingPlace.remove(10));

        TestCase.assertSame(car15, parkingPlace.remove(car15));
        TestCase.assertNull(parkingPlace.remove(car15));

        // Check the garage again

        TestCase.assertEquals(25, parkingPlace.getSize());
        TestCase.assertEquals(2, parkingPlace.getFreePlaces());
        TestCase.assertEquals(23, parkingPlace.getCount());

        // Add a car and it should land up in a free place

        parkingPlace.add(new SportsCar("Ferrari"));
        TestCase.assertEquals("Ferrari", parkingPlace.get(10).getName());

        parkingPlace.add(new SportsCar("Another Ferrari"));
        TestCase.assertEquals("Another Ferrari", parkingPlace.get(15).getName());

    }

    /**
     * Do all the tests
     */
    @Test
    public void hierarchyTest() {

        // Define cars

        Car porshe = new SportsCar("Porshe 911");
        Car toyota = new FamilyCar("Toyota Avensis");

        TestCase.assertEquals("Toyota Avensis", toyota.getName());
        TestCase.assertEquals("Porshe 911", porshe.getName());

        // Add garage and a car
        // Check what are the weekday and weekend options...

        Garage garage = new Garage(2);

        TestCase.assertEquals(2, garage.getSize());
        TestCase.assertEquals(2, garage.getFreePlaces());
        TestCase.assertEquals(0, garage.getCount());

        garage.add(toyota);

        TestCase.assertEquals(2, garage.getSize());
        TestCase.assertEquals(1, garage.getFreePlaces());
        TestCase.assertEquals(1, garage.getCount());

        Car weekdayCar = garage.pickACar(WEEKDAY_DAY);
        TestCase.assertSame(toyota, weekdayCar);

        Car weekendCar = garage.pickACar(WEEKEND_DAY);
        TestCase.assertSame(toyota, weekendCar);

        Car todayCar = garage.pickACar();
        TestCase.assertSame(toyota, todayCar);

        // Buy a new car and check again...

        garage.add(porshe);

        TestCase.assertEquals(2, garage.getSize());
        TestCase.assertEquals(0, garage.getFreePlaces());
        TestCase.assertEquals(2, garage.getCount());

        weekdayCar = garage.pickACar(WEEKDAY_DAY);
        TestCase.assertSame(toyota, weekdayCar);

        weekendCar = garage.pickACar(WEEKEND_DAY);
        TestCase.assertSame(porshe, weekendCar);

        todayCar = garage.pickACar();
        if (todayWeekend) {
            TestCase.assertSame(porshe, todayCar);
        } else {
            TestCase.assertSame(toyota, todayCar);
        }

        // All cars sold. Now what are the options...

        garage.removeAll();

        weekdayCar = garage.pickACar(WEEKDAY_DAY);
        TestCase.assertNotNull(weekdayCar);
        TestCase.assertSame(Bus.class, weekdayCar.getClass());
        TestCase.assertEquals("Bus", weekdayCar.getName());

        weekendCar = garage.pickACar(WEEKEND_DAY);
        TestCase.assertNull(weekendCar);

        todayCar = garage.pickACar();
        if (todayWeekend) {
            TestCase.assertNull(todayCar);
        } else {
            TestCase.assertSame(Bus.class, todayCar.getClass());
            TestCase.assertEquals("Bus", todayCar.getName());
        }

        // Custom Car

        Car mycar = new Car() {

            @Override
            public String getName() {
                return "myname";
            }

        };
        TestCase.assertEquals("myname", mycar.getName());

        garage.add(mycar);

        weekdayCar = garage.pickACar(WEEKDAY_DAY);
        TestCase.assertNotNull(weekdayCar);
        TestCase.assertSame(mycar, weekdayCar);

        weekendCar = garage.pickACar(WEEKEND_DAY);
        TestCase.assertSame(mycar, weekendCar);

        TestCase.assertSame(mycar, garage.pickACar());

    }

}
