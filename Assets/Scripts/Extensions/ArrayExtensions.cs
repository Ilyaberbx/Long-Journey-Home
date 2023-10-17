namespace Extensions
{
    public static class ArrayExtensions
    {
        public static T Random<T>(this T[] array) 
            => array[UnityEngine.Random.Range(0, array.Length)];
    }
}