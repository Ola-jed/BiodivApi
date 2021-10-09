using System;

namespace BiodivImageComparison
{
    /// <summary>
    /// Structure to define CIE L*a*b*.
    /// </summary>
    public struct Lab
    {
        /// <summary>
        /// Gets an empty CIELab structure.
        /// </summary>
        public static readonly Lab Empty = new();

        /// <summary>
        /// Gets or sets L component.
        /// </summary>
        public double L { get; set; }

        /// <summary>
        /// Gets or sets a component.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Gets or sets a component.
        /// </summary>
        public double B { get; set; }

        public Lab(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }

        /// <summary>
        /// Computes the delta e with an other Lab using the 1976 formula
        /// </summary>
        /// <param name="rhs">The other Lab element for the comparison</param>
        /// <returns>The color difference</returns>
        public double DeltaE76(Lab rhs)
        {
            return Math.Sqrt(
                Math.Pow(L - rhs.L, 2) + Math.Pow(A - rhs.A, 2) + Math.Pow(B - rhs.B, 2)
            );
        }

        /// <summary>
        /// Computes the delta e with an other Lab using the 1994 formula
        /// </summary>
        /// <param name="rhs">The other Lab element for the comparison</param>
        /// <returns>The color difference</returns>
        public double DeltaE94(Lab rhs)
        {
            var deltaL = rhs.L - L;
            var c1 = Math.Sqrt(rhs.A * rhs.A + rhs.B * rhs.B);
            var c2 = Math.Sqrt(A * A + B * B);
            var deltaC = c1 - c2;
            var deltaA = A - rhs.A;
            var deltaB = B - rhs.B;
            var deltaH = Math.Sqrt(Math.Pow(deltaA, 2) + Math.Pow(deltaB, 2) - Math.Pow(deltaC, 2));
            var sc = 1 + 0.045 * c1;
            var sh = 1 + 0.015 * c1;
            var result = Math.Sqrt(Math.Pow(deltaL, 2) + Math.Pow(deltaC / sc, 2) + Math.Pow(deltaH / sh, 2));
            return double.IsNaN(result) ? 0 : result;
        }
    }
}