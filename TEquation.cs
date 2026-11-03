using System;

// Базовий абстрактний клас рівняння
public abstract class TEquation
{
    public abstract double[] GetRoots();
    public abstract bool IsRoot(double x);
}

// Клас лінійного рівняння: A*x + B = 0
public class TLinearEquation : TEquation
{
    public double A { get; set; }
    public double B { get; set; }

    public TLinearEquation(double a, double b)
    {
        A = a;
        B = b;
    }

    public override double[] GetRoots()
    {
        if (Math.Abs(A) < 1e-9) return new double[0]; // Немає коренів або безліч
        return new double[] { -B / A };
    }

    public override bool IsRoot(double x)
    {
        return Math.Abs(A * x + B) < 1e-6;
    }

    public override string ToString()
    {
        return $"Лінійне: {A}x + {B} = 0";
    }
}

// Клас квадратного рівняння: A*x^2 + B*x + C = 0
public class TQuadraticEquation : TEquation
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    public TQuadraticEquation(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public override double[] GetRoots()
    {
        if (Math.Abs(A) < 1e-9)
        {
            // Якщо A == 0, це звичайне лінійне рівняння B*x + C = 0
            if (Math.Abs(B) < 1e-9) return new double[0];
            return new double[] { -C / B };
        }

        double d = B * B - 4 * A * C;
        if (d < 0) return new double[0]; // Дійсних коренів немає
        if (Math.Abs(d) < 1e-9) return new double[] { -B / (2 * A) }; // Один корінь

        double sqrtD = Math.Sqrt(d);
        return new double[] { (-B + sqrtD) / (2 * A), (-B - sqrtD) / (2 * A) }; // Два корені
    }

    public override bool IsRoot(double x)
    {
        return Math.Abs(A * x * x + B * x + C) < 1e-6;
    }

    public override string ToString()
    {
        return $"Квадратне: {A}x^2 + {B}x + {C} = 0";
    }
}