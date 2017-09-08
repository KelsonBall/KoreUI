using System;
using OpenTK;
using static System.Math;

namespace Processing.OpenTk.Core.Math
{
    public struct PVector
    {
        public readonly double X;
        public readonly double Y;

        public PVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static PVector î => new PVector(1, 0);

        public static PVector ĵ => new PVector(0, 1);

        public static PVector O => new PVector(0, 0);

        public static PVector FromAngle(double angle) => new PVector(Sin(angle), Cos(angle));

        public static PVector operator +(PVector a, PVector b) => a.Add(b);

        public static PVector operator -(PVector a, PVector b) => a.Add(new PVector(-b.X, -b.Y));

        public static PVector operator *(PVector a, double b) => a.Scale(b);

        public static PVector operator *(PVector a, int b) => a.Scale(b);

        public static PVector operator *(double a, PVector b) => b.Scale(a);

        public static PVector operator *(int a, PVector b) => b.Scale(a);

        public static PVector operator /(PVector a, double b) => a.Scale(1 / b);

        public static bool operator ==(PVector a, PVector b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(PVector a, PVector b) => !(a == b);

        public static implicit operator Vector2(PVector p) => new Vector2((float)p.X, (float)p.Y);

        public static implicit operator PVector(Vector2 v) => new PVector(v.X, v.Y);

        public static implicit operator PVector((double x, double y) p) => new PVector(p.x, p.y);

        public static implicit operator PVector((int x, int y) p) => new PVector(p.x, p.y);

        public PVector Midpoint(PVector with) => (this + with) / 2;

        public PVector Inverse() => new PVector(Y, X);

        public PVector Perpendicular() => new PVector(-Y, X).Unit();

        public PVector3 ToVector3() => new PVector3(X, Y, 0);

        public PVector Add(PVector to) => new PVector(X + to.X, Y + to.Y);

        public PVector Scale(double scalar) => new PVector(X * scalar, Y * scalar);

        public double Dot(PVector by) => X * by.X + Y + by.Y;

        public double MagnitudeSquared() => X * X + Y * Y;

        public double Magnitude() => Sqrt(MagnitudeSquared());

        public double Angle() => Atan2(Y, X);

        public PVector Unit()
        {
            var mag = Magnitude();
            if (mag == 0)
                return (0, 1);
            return this / Magnitude();
        }

        public PVector Rotate(double angle) => FromAngle(Angle() + angle) * Magnitude();

        public override string ToString() => $"{{{X}, {Y}}}";

        public PVector Copy() => new PVector(X, Y);

        public (PVector pv, PVector nv, PVector ov) GetOuterFaceVector(PVector previous, PVector next)
        {            
            PVector dv = (this - previous).Perpendicular();
            PVector dp = previous.Midpoint(this);
            if (((dp + dv) - next).Magnitude() < (dp - next).Magnitude())
                dv *= -1;


            PVector ev = (next - this).Perpendicular();
            PVector ep = this.Midpoint(next);
            if (((ep + ev) - previous).Magnitude() < (ep - previous).Magnitude())
                ev *= -1;

            return (ev, dv, (dv + ev).Unit());
        }
    }
}
