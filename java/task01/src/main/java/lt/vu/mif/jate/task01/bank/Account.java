/*
 *
 */
package lt.vu.mif.jate.task01.bank;

import java.util.Arrays;

/**
 * @author john Class Account
 */
public final class Account {

    /**
     * String id.
     */
    private String id;
    /**
     * float balance.
     */
    private float balance = 0;
    /**
     * static final int C04.
     */
    private static final int C04 = 4;
    /**
     * static final int C08.
     */
    private static final int C08 = 8;
    /**
     * static final int C20.
     */
    private static final int C20 = 20;

    /**
     * @param newID
     *            the newId
     */
    public Account(final String newID) {
        if (newID.substring(0, 2) == "LT") {
            throw new NumberFormatException();
        } else if (newID.length() != C20) {
            throw new NumberFormatException();
        } else if (!(Arrays.asList(Bank.all()).contains(Integer.parseInt(newID
                .substring(C04, C08))))) {
            throw new NumberFormatException();
        } else {
            this.id = newID;
        }
    }

    /**
     * @return the return
     */
    public String balance() {
        return String.format("%.2f", this.balance);
    }

    /**
     * @param s
     *            the s
     */
    public void credit(final String s) {
        if (s.matches("^\\d+\\.\\d{2}")) {
            this.balance = this.balance + Float.parseFloat(s);
        } else {
            throw new NumberFormatException();
        }
    }

    /**
     * @param s
     *            the s
     */
    public void debit(final String s) {
        this.balance = this.balance - Float.parseFloat(s);
    }

    /**
     * @param s
     *            the s
     * @param a1
     *            the a1
     */
    public void debit(final String s, final Account a1) {
        a1.credit(s);
        this.debit(s);
    }

    /**
     * @return the retrun
     */
    public Bank getBank() {
        System.out.println(this.id.substring(C04, C08));
        int code = Integer.parseInt(this.id.substring(C04, C08));
        Bank bank0 = Bank.get(code);
        return bank0;
    }
}
