/*
 *
 */
package lt.vu.mif.jate.task01.utility;

/**
 * @author john class StringUtility
 */
public final class StringUtility {

    /**
     * @param s
     *            The s
     * @return The return
     */
    public static String compress(final String s) {
        if (s != null) {
            String newStr = new String(s.substring(0, 1));
            int cnt = 1;
            for (int i = 0; i < (s.length() - 1); i++) {
                if (s.substring(i, i + 1).equals(s.substring(i + 1, i + 2))) {
                    if (i == (s.length() - 2)) {
                        if (cnt > 2) {
                            newStr += String.valueOf(cnt);
                            newStr += s.substring(i + 1, i + 2);
                        } else if (cnt == 2) {
                            newStr += s.substring(i, i + 1);
                            newStr += s.substring(i + 1, i + 2);
                        } else {
                            newStr += s.substring(i + 1, i + 2);
                        }
                    }
                    cnt++;
                } else {
                    if (cnt > 2) {
                        newStr += String.valueOf(cnt);
                        newStr += s.substring(i + 1, i + 2);
                    } else if (cnt == 2) {
                        newStr += s.substring(i, i + 1);
                        newStr += s.substring(i + 1, i + 2);
                    } else {
                        newStr += s.substring(i + 1, i + 2);
                    }
                    cnt = 1;
                }
            }
            return newStr;
        } else {
            return null;
        }
    }

    /**
     * @param s
     *            The s
     * @return The return
     */
    public static String decompress(final String s) {
        if (s != null) {
            String newStr = s.substring(0, 1);
            int cnt = 1;
            for (int i = 1; i < (s.length()); i += cnt) {
                if (isParsable(s.substring(i, i + 1))) {
                    while (isParsable(s.substring(i + cnt, i + 1 + cnt))) {
                        cnt++;
                    }
                    for (int j = 0; j < Integer.parseInt(s
                            .substring(i, i + cnt)) - 1; j++) {
                        newStr += s.substring(i - 1, i);
                    }
                } else {
                    cnt = 1;
                    newStr += s.substring(i, i + 1);
                }
            }
            return newStr;
        } else {
            return null;
        }
    }

    /**
     * @param input
     *            The input
     * @return the return
     */
    static boolean isParsable(final String input) {
        boolean parsable = true;
        try {
            Integer.parseInt(input);
        } catch (NumberFormatException e) {
            parsable = false;
        }
        return parsable;
    }

    /**
     * @param s
     *            The s
     * @return The return
     */
    public static String reverse(final String s) {
        if (s != null) {
            int i, len = s.length();
            StringBuilder dest = new StringBuilder(len);
            for (i = (len - 1); i >= 0; i--) {
                dest.append(s.charAt(i));
            }
            return dest.toString();
        } else {
            return null;
        }
    }

    /**
     * StringUtility().
     */
    private StringUtility() {
        // wont be called
    }

}
