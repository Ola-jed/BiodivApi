using System;
using SixLabors.ImageSharp.PixelFormats;

namespace BiodivImageComparison
{
    public struct Rgb
    {
        public double R
        {
            get => _red;
            set
            {
                if (! value.IsBetween(0,255))
                {
                    throw new ArgumentException("The values should be between 0 and 255");
                }

                _red = value;
            }
        }
        public double G
        {
            get => _green;
            set
            {
                if (! value.IsBetween(0,255))
                {
                    throw new ArgumentException("The values should be between 0 and 255");
                }

                _green = value;
            }
        }

        public double B
        {
            get => _blue;
            set
            {
                if (! value.IsBetween(0,255))
                {
                    throw new ArgumentException("The values should be between 0 and 255");
                }

                _blue = value;
            }
        }

        private double _red;
        private double _green;
        private double _blue;

        public Rgb(double red, double green, double blue)
        {
            if (!red.IsBetween(0, 255) || !green.IsBetween(0, 255) || !blue.IsBetween(0, 255))
            {
                throw new ArgumentException("The values should be between 0 and 255");
            }
            _red = red;
            _green = green;
            _blue = blue;
        }

        /// <summary>
        /// Converts the current rgb object to XYZ
        /// </summary>
        /// <returns>A Xyz struct</returns>
        public Xyz ToXyz()
        {
            var rLinear = _red / 255.0;
            var gLinear = _green / 255.0;
            var bLinear = _blue / 255.0;

            // convert to a sRGB form
            var r = rLinear > 0.04045
                ? Math.Pow((rLinear + 0.055) / (
                    1 + 0.055), 2.2)
                : rLinear / 12.92;
            var g = gLinear > 0.04045
                ? Math.Pow((gLinear + 0.055) / (
                    1 + 0.055), 2.2)
                : gLinear / 12.92;
            var b = bLinear > 0.04045
                ? Math.Pow((bLinear + 0.055) / (
                    1 + 0.055), 2.2)
                : bLinear / 12.92;
            return new Xyz(
                r * 0.4124 + g * 0.3576 + b * 0.1805,
                r * 0.2126 + g * 0.7152 + b * 0.0722,
                r * 0.0193 + g * 0.1192 + b * 0.9505
            );
        }

        /// <summary>
        /// Converts the current rgb to lab
        /// </summary>
        /// <returns>The lab corresponding to the actual object</returns>
        public Lab ToLab()
        {
            return ToXyz().ToLab();
        }
    }

    public static class RgbUtils
    {
        public static Rgb Rgb24ToRgb(this Rgb24 self)
        {
            return new Rgb
            {
                R = self.R,
                G = self.G,
                B = self.B
            };
        }
    }
}