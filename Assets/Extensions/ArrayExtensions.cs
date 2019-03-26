public static class ArrayExtensions {

    public static bool Contains <T>(this T[,] array, T item) {
        foreach (var i in array) {
            if(i.Equals(item)) {
                return true;
            }
        }
        return false;
    }
}
