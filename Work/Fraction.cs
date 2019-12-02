using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work
{
    // можливо краще використати структуру?
    public class Fraction
    {
        protected long Number;
        protected long Denominator;
        public long PropertyDenominator
        {
            get => Denominator;
            set
            {
                if (value == 0)
                {
                    throw new Exception("Denominator is zero!");
                }
                if (value < 0)
                {
                    value = -value;
                    Number = -Number;
                }
                Denominator = value;
                Simply();
            }
        }
        public long PropertyNumber
        {
            get => Number;
            set => Number = value;
        }
        public Fraction(long number, long denominator)
        {
            Number = number;
            Denominator = denominator;
            Fix();
            Simply();
        }
        public Fraction(long number)
        {
            Number = number;
            Denominator = 1;
        }
        public Fraction(double number)
        {
            Denominator = 1;
            for (int i = 0; i < 10 && number % 1 != 0; i++)
            {
                number *= 10;
                Denominator *= 10;
            }
            Number = (long)number;
            Simply();
        }
        public Fraction(string value)
        {
            value.Replace(" ", "");
            StringBuilder builder = new StringBuilder();
            if (value.Length == 0)
            {
                throw new Exception("String length is null");
            }
            int i = 0;
            for (; i < value.Length; i++)
            {
                if (value[i] == '/')
                {
                    i++;
                    break;
                } else
                {
                    builder.Append(value[i]);
                }

            }
            Number = long.Parse(builder.ToString());
            if (i < value.Length)
            {
                builder = new StringBuilder();
                for (; i < value.Length; i++)
                {
                    builder.Append(value[i]);
                }
                Denominator = long.Parse(builder.ToString());
            } else
            {
                Denominator = 1;
            }
            Fix();
            Simply();
        }
        public override string ToString()
        {
            return $"{Number}/{Denominator}";
        }
        public static List<long> GetFactors(long number)
        {
            List<long> factors = new List<long>();
            long cache = number;
            for (int i = 2; i <= Math.Sqrt(cache); i++)
            {
                if (cache % i == 0)
                {
                    factors.Add(i);
                    cache /= i;
                }
            }
            factors.Add(cache);
            return factors;
        }
        public static long GetNSK(long a, long b)
        {
            List<long> aL = GetFactors(a);
            List<long> bl = GetFactors(b);
            for (int i = 0; i < aL.Count; i++)
            {
                if (bl.Count == 0)
                {
                    break;
                }
                for (int j = 0; j < bl.Count; j++)
                {
                    if (aL[i] == bl[j])
                    {
                        bl.RemoveAt(j);
                        break;
                    }
                }
            }
            long result = 1;
            for (int i = 0; i < aL.Count; i++)
            {
                result *= aL[i];
            }
            if (bl.Count > 0)
            {
                for (int i = 0; i < bl.Count; i++)
                {
                    result *= bl[i];
                }
            }
            return result;
        }
        public static long GetMaxDiv(Fraction fraction)
        {
            long a = Math.Abs(fraction.Number);
            long b = fraction.Denominator;
            while (a != b)
            {
                if (a > b) a -= b;
                else b -= a;
            }
            return a;
        }
        public void Fix()
        {
            if ((Number < 0 && Denominator < 0) || (Number > 0 && Denominator < 0))
            {
                Number = -Number;
                Denominator = -Denominator;
            }
            if (Denominator == 0)
            {
                throw new Exception("Denominator is zero!");
            }
        }
        public void Simply()
        {
            long maxDiv = GetMaxDiv(this);
            Number /= maxDiv;
            Denominator /= maxDiv;
        }
        // унарні
        public static Fraction operator +(Fraction a)
        {
            return a;
        }
        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.Number, a.Denominator);
        }
        public static Fraction operator !(Fraction a)
        {
            Fraction result = new Fraction(a.Denominator, a.Number);
            result.Fix();
            return result;
        }
        // бінарні
        public static Fraction operator +(Fraction a, Fraction b)
        {
            long nsk = GetNSK(a.Denominator, b.Denominator);
            Fraction result = new Fraction(a.Number * (nsk / a.Denominator) + b.Number * (nsk / b.Denominator), nsk);
            result.Fix();
            result.Simply();
            return result;
        }
        public static Fraction operator -(Fraction a, Fraction b)
        {
            return a + (-b);
        }
        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction result = new Fraction(a.Number * b.Number, a.Denominator * b.Denominator);
            result.Fix();
            result.Simply();
            return result;
        }
        public static Fraction operator /(Fraction a, Fraction b)
        {
            return a * !b;
        }
        //
        public static Fraction operator +(Fraction a, long b)
        {
            Fraction result = new Fraction(a.Number + b * a.Denominator, a.Denominator);
            result.Simply();
            return result;
        }
        public static Fraction operator -(Fraction a, long b)
        {
            return a + (-b);
        }
        public static Fraction operator *(Fraction a, long b)
        {
            Fraction result = new Fraction(a.Number * b, a.Denominator);
            result.Simply();
            return result;
        }
        public static Fraction operator /(Fraction a, long b)
        {
            Fraction result = new Fraction(a.Number, a.Denominator * b);
            result.Fix();
            result.Simply();
            return result;
        }
        // порівняння
        public static bool operator ==(Fraction first, Fraction second)
        {
            if (first.Number * second.Denominator == first.Denominator * second.Number)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }
        public static bool operator >(Fraction a, Fraction b)
        {
            return (a - b).Number > 0;
        }
        public static bool operator <(Fraction a, Fraction b)
        {
            return (a - b).Number < 0;
        }
        public static bool operator <=(Fraction a, Fraction b) {
            if (a == b || a < b)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public static bool operator >=(Fraction a, Fraction b)
        {
            if (a == b || a > b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // приведення
        public static implicit operator double(Fraction target)
        {
            return (double)target.Number / target.Denominator;
        }
    }
}
