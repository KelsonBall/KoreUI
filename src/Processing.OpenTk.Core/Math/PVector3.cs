using OpenTK;
using System;
using static System.Math;

namespace Processing.OpenTk.Core.Math
{
    public struct PVector3
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public PVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static PVector3 î => new PVector3(1, 0, 0);

        public static PVector3 ĵ => new PVector3(0, 1, 0);

        public static PVector3 k̂ => new PVector3(0, 0, 1);

        public static PVector3 O => new PVector3(0, 0, 0);

        public static PVector3 FromAngle(double angle)
        {
            return new PVector3(Sin(angle), Cos(angle), 0);
        }

        public static PVector3 operator +(PVector3 a, PVector3 b)
        {
            return a.Add(b);
        }

        public static PVector3 operator -(PVector3 a, PVector3 b)
        {
            return a.Add(new PVector3(-b.X, -b.Y, -b.Z));
        }

        public static PVector3 operator *(PVector3 a, PVector3 b)
        {
            return a.Cross(b);
        }

        public static PVector3 operator *(PVector3 a, double b)
        {
            return a.Scale(b);
        }

        public static PVector3 operator *(PVector3 a, int b)
        {
            return a.Scale(b);
        }

        public static PVector3 operator *(double a, PVector3 b)
        {
            return b.Scale(a);
        }

        public static PVector3 operator *(int a, PVector3 b)
        {
            return b.Scale(a);
        }

        public static PVector3 operator /(PVector3 a, double b)
        {
            return a.Scale(1 / b);
        }

        public static implicit operator Vector3 (PVector3 p) => new Vector3((float)p.X, (float)p.Y, (float)p.Z);

        public static implicit operator PVector3 (Vector3 v) => new PVector3(v.X, v.Y, v.Z);

        public PVector ToVector () => new PVector(X, Y);        

        public static implicit operator PVector3 ((double x, double y, double z) p) => new PVector3(p.x, p.y, p.z);

        public PVector3 Add(PVector3 to)
        {
            return new PVector3(X + to.X, Y + to.Y, Z + to.Z);
        }

        public PVector3 Scale(double scalar)
        {
            return new PVector3(X * scalar, Y * scalar, Z * scalar);
        }

        public double Dot(PVector3 by)
        {
            return X * by.X + Y + by.Y;
        }

        public PVector3 Cross(PVector3 b)
        {
            return new PVector3(Y * b.Z - Z * b.Y, X * b.Z - Z * b.X, X * b.Y - Y * b.X);
        }

        public double MagnitudeSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Magnitude()
        {
            return Sqrt(MagnitudeSquared());
        }

        public double Angle()
        {
            return Atan2(Y, X);
        }

        public PVector3 Unit()
        {
            return this / Magnitude();
        }

        public PVector3 Rotate(double angle)
        {
            return FromAngle(Angle() + angle) * Magnitude();
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }
    }
}
