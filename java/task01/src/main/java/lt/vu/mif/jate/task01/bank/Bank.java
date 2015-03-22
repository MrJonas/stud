/*
 *
 */
package lt.vu.mif.jate.task01.bank;

import java.util.HashMap;

/**
 * @author john ClassBank
 */
public final class Bank {

    /**
     * final int code.
     */
    private final int code;
    /**
     * final String name.
     */
    private final String name;
    /**
     * static HashMap.
     */
    private static HashMap<Integer, String> banks =
            new HashMap<Integer, String>();
    /**
     * static final int SWE.
     */
    private static final int SWE = 7300;
    /**
     * static final int SEB.
     */
    private static final int SEB = 7400;
    /**
     * static final int SNO.
     */
    private static final int SNO = 7500;

    static {
        banks.put(SWE, "Swedbank");
        banks.put(SEB, "SEB");
        banks.put(SNO, "Snoras");
    }

    /**
     * @return the return
     */
    public static Object[] all() {
        return banks.keySet().toArray();
    }

    /**
     * @param key
     *            the key
     * @return the return
     */
    public static Bank get(final int key) {
        String value = banks.get(key);
        if (value != null) {
            Bank bank = new Bank(key, value);
            return bank;
        } else {
            return null;
        }
    }

    /**
     * @param i
     *            the i
     * @param n
     *            the n
     */
    Bank(final int i, final String n) {
        this.code = i;
        this.name = n;
    }

    /**
     * @return the return
     */
    public Object getCode() {
        return this.code;
    }

    /**
     * @return the return
     */
    public String getName() {
        return this.name;
    }

}
