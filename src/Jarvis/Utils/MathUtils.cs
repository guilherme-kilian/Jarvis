namespace Jarvis.Utils
{
    public static class MathUtils
    {
        public static double GetPercentage(double value, double total)
        {
            if (total is 0)
                return 0;

            return value * 100 / total;
        }
    }
}
