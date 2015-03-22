package lt.vu.mif.jate.task01;

import junit.framework.TestCase;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;
import lt.vu.mif.jate.task01.bank.Account;
import lt.vu.mif.jate.task01.bank.Bank;

/**
 * Create Bank and Account classes that would test.
 * @author valdo
 */
@RunWith(JUnit4.class)
public class AccountTest {

    /**
     * Test bank library
     */
    @Test
    public void bankTest() {
        
        Bank bank = Bank.get(7300);
        TestCase.assertEquals("Swedbank", bank.getName());
        TestCase.assertEquals(7300, bank.getCode());

        bank = Bank.get(7400);
        TestCase.assertEquals("SEB", bank.getName());
        
        bank = Bank.get(1234567);
        TestCase.assertNull(bank);

        TestCase.assertTrue(Bank.all().length > 2);
        
    }
    
    /**
     * Test account setup
     */
    @Test
    public void accountTest() {
        
        Account account = new Account("LT337300010077211111");
        //TestCase.assertEquals(Bank.get(7300), account.getBank());

        try {
            account = new Account("LT331234010077211111");
            TestCase.fail("Account number is not checked");
        } catch (NumberFormatException ex) { }
        
        try {
            account = new Account("337300010077211111");
            TestCase.fail("Account number is not checked");
        } catch (NumberFormatException ex) { }

        try {
            account = new Account("LT3373000100772");
            TestCase.fail("Account number is not checked");
        } catch (NumberFormatException ex) { }

    }
    
    /**
     * Test balance operations
     */
    @Test
    public void balanceTest() {
        
        Account a1 = new Account("LT337300010077211111");
        Account a2 = new Account("LT337300010077211112");
        
        a1.credit("1000.00");
        a2.credit("1000.00");
        
        TestCase.assertEquals("1000.00", a1.balance());
        TestCase.assertEquals("1000.00", a2.balance());
        
        failCredit(a1, "LT331234010077211111");
        failCredit(a1, "one hundred");
        failCredit(a1, "-100");
        failCredit(a1, "100.123");
        failCredit(a1, "1000.1a");
        failCredit(a1, "1,000.00");
        failCredit(a1, "100,10");
        failCredit(a1, "FF");

        a1.debit("120.50");
        
        TestCase.assertEquals("879.50", a1.balance());
        
        a2.debit("130.00", a1);
        
        TestCase.assertEquals("1009.50", a1.balance());
        TestCase.assertEquals("870.00", a2.balance());
    }
    
    /**
     * Utility routine to test credit operation with wrong numbers.
     * @param a account.
     * @param badValue string representing wrong number.
     */
    private static void failCredit(Account a, String badValue) {
        try {
            a.credit(badValue);
            TestCase.fail(String.format("Trying to credit/debit %s should fail with NumberFormatException", badValue));
        } catch (NumberFormatException ex) { }
    }
    
}
