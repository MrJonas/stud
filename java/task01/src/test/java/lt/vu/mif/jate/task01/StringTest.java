package lt.vu.mif.jate.task01;

import java.util.Random;
import junit.framework.TestCase;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;
import lt.vu.mif.jate.task01.utility.StringUtility;
import org.apache.commons.lang3.RandomStringUtils;

/**
 * Create StringUtility class that provides various string manipulation routines.
 * @author valdo
 */
@RunWith(JUnit4.class)
public class StringTest {

    private static final Random RANDOM = new Random();
    private static final int SOME_INT = 111;
    
    /**
     * Reverse a string.
     */
    @Test
    public void reverseTest() {
        
        TestCase.assertNull(StringUtility.reverse(null));
        TestCase.assertEquals("alus", StringUtility.reverse("sula"));
        TestCase.assertEquals("arbadakarba", StringUtility.reverse("abrakadabra"));

        String s = "The only way to do great work is to love what you do.";
        TestCase.assertEquals(s, StringUtility.reverse(StringUtility.reverse(s)));
        
        TestCase.assertSame(String.class, StringUtility.reverse("").getClass());
    }

    /**
     * Compress and de-compress an alphabetic string in such a way that 
     * all the same consequent characters are encoded in the form
     * "laaaabaasss" => "la4baas3"
     * If encoded string length is the same as the coded one - return
     * the original string. Create decoding routine.
     */
    @Test
    public void compressTest() {
        
        TestCase.assertNull(StringUtility.compress(null));
        TestCase.assertEquals("soomethi5ng", StringUtility.compress("soomethiiiiing"));
        TestCase.assertEquals("smo4th 3wa3y too", StringUtility.compress("smooooth   waaay too"));
        TestCase.assertEquals("soomethiiiiing", StringUtility.decompress("soomethi5ng"));
        
        for (int i = 0; i < SOME_INT; i++) {
            String randomString = generateRandomString();
            TestCase.assertEquals(randomString, StringUtility.decompress(StringUtility.compress(randomString)));
        }
        
    }
    
    /**
     * Generates a random string with random number of equal consequent characters.
     * @return random string.
     */
    private static String generateRandomString() {
        StringBuilder sb = new StringBuilder();
        for (char c: RandomStringUtils.randomAlphabetic(SOME_INT).toCharArray()) {
            sb.append(c);
            if (RANDOM.nextBoolean()) {
                for (int i = 0; i < RANDOM.nextInt(SOME_INT); i++) {
                    sb.append(c);
                }
            }
        }
        return sb.toString();
    }
    
}
