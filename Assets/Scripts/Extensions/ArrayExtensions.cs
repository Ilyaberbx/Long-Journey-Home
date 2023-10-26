using Logic.Level;

namespace Extensions
{
    public static class ArrayExtensions
    {
        public static T Random<T>(this T[] array) 
            => array[UnityEngine.Random.Range(0, array.Length - 1)];

        public static void ExecuteAll(this IAction[] array)
        {
            foreach (var element in array) 
                element.Execute();
        }
    }
}