using System;

namespace BiodivImageComparison
{
    /// <summary>
    /// Structure to define CIE XYZ.
    /// </summary>
    public struct Xyz
    {
        /// <summary>
        /// Gets an empty CIE XYZ structure.
        /// </summary>
        public static readonly Xyz Empty = new();

        /// <summary>
        /// Gets the CIE D65 (white) structure.
        /// </summary>
        private static readonly Xyz D65 = new(0.9505, 1.0, 1.0890);

        private double _x;
        private double _y;
        private double _z;

        /// <summary>
        /// Gets or sets X component.
        /// </summary>
        public double X
        {
            get => _x;
            set => _x = value > 0.9505 ? 0.9505 : value < 0 ? 0 : value;
        }

        /// <summary>
        /// Gets or sets Y component.
        /// </summary>
        public double Y
        {
            get => _y;
            set => _y = value > 1.0 ? 1.0 : value < 0 ? 0 : value;
        }

        /// <summary>
        /// Gets or sets Z component.
        /// </summary>
        public double Z
        {
            get => _z;
            set => _z = value > 1.089 ? 1.089 : value < 0 ? 0 : value;
        }

        public Xyz(double x, double y, double z)
        {
            _x = x > 0.9505 ? 0.9505 : x < 0 ? 0 : x;
            _y = y > 1.0 ? 1.0 : y < 0 ? 0 : y;
            _z = z > 1.089 ? 1.089 : z < 0 ? 0 : z;
        }

        /// <summary>
        /// Convert the current xyz to lab format
        /// </summary>
        /// <returns>The Lab corresponding</returns>
        public Lab ToLab()
        {
            return new Lab
            {
                L = 116.0 * FXyz(_y / D65.Y) - 16,
                A = 500.0 * (FXyz(_x / D65.X) - FXyz(_y / D65.Y)),
                B = 200.0 * (FXyz(_y / D65.Y) - FXyz(_z / D65.Z))
            };
        }

        private static double FXyz(double t)
        {
            return t > 0.008856 ? Math.Pow(t, 1.0/3.0) : 7.787*t + 16.0/116.0;
        }
    }
}