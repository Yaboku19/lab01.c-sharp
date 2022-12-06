namespace ComplexAlgebra
{
    using System;
    using System.Collections.Generic;
    using static System.Math;
    /// <summary>
    /// A type for representing Complex numbers.
    /// </summary>
    ///
    /// TODO: Model Complex numbers in an object-oriented way and implement this class.
    /// TODO: In other words, you must provide a means for:
    /// TODO: * instantiating complex numbers
    /// TODO: * accessing a complex number's real, and imaginary parts
    /// TODO: * accessing a complex number's modulus, and phase
    /// TODO: * complementing a complex number
    /// TODO: * summing up or subtracting two complex numbers
    /// TODO: * representing a complex number as a string or the form Re +/- iIm
    /// TODO:     - e.g. via the ToString() method
    /// TODO: * checking whether two complex numbers are equal or not
    /// TODO:     - e.g. via the Equals(object) method
    public class Complex
    {
        public double Im { get; }

        public string ImStr 
        {
            get
            {
                if (Im == 0.0)
                {
                    return "";
                }
                var a = Im > 0
                    ? "+"
                    : "-";
                return a + (Abs(Im) == 1 ? "i": Im.ToString());
            }
        }

        public double Real { get; }

        public string RealStr 
        {
            get
            {
                if (Real == 0.0)
                {
                    return "";
                }
                return Real.ToString();
            }
        }

        public Complex (double real, double im)
        {
            Real = real;
            Im = im;
        }
        
        public double Modulus => Sqrt(Pow(Im, 2) + Pow(Real, 2));

        public double Phase => Atan2(Im, Real);

        public Complex Complement => new Complex(Real, Im * -1);

        public Complex Plus(Complex b) => new Complex(Real + b.Real, Im + b.Im);

        public Complex Minus(Complex b) => Plus(new Complex(b.Real * -1, b.Im * -1));

        public override string ToString() => $"{RealStr}{ImStr}";

        public override bool Equals(object obj)
        {
            return obj is Complex complex &&
                   Im == complex.Im &&
                   Real == complex.Real;
        }

        public override int GetHashCode() => HashCode.Combine(Im, Real);
    }
}