using UnityEngine;
using System.Collections;

public static class FloatExtensions {

    public static bool IsOdd(this float myfloat) {
        if (Mathf.Abs(myfloat) % 2 == 1) {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool IsEven(this float myfloat) {
        if (Mathf.Abs(myfloat) % 2 == 0) {
            return true;
        }
        else {
            return false;
        }
    }
    public static int Sign(this float myfloat) {
        if (myfloat < 0)
            return -1;
        else if (myfloat > 0)
            return 1;
        else
            return 0;
    }
}
