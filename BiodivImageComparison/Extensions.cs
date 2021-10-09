using System;

namespace BiodivImageComparison
{
    public static class Extensions
    {
        public static bool IsBetween<T>(this T self,T lower, T upper) where T : IComparable
        {
            return self.CompareTo(lower) >= 0  && self.CompareTo(upper) <= 0;
        }
    }
}