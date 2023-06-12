﻿struct Vector3
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector3(double x, double y, double z)
        : this()
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public static Vector3 operator +(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
    }

    public static Vector3 operator -(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
    }

    public static Vector3 operator *(Vector3 vec, double val)
    {
        return new Vector3(vec.X * val, vec.Y * val, vec.Z * val);
    }

    public static Vector3 operator /(Vector3 vec, double val)
    {
        return new Vector3(vec.X / val, vec.Y / val, vec.Z / val);
    }

    public double Dot(Vector3 vec)
    {
        return (this.X * vec.X + this.Y * vec.Y + this.Z * vec.Z);
    }

    public static Vector3 Cross(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.Y * vec2.Z - vec1.Z * vec2.Y,
            vec1.Z * vec2.X - vec1.X * vec2.Z,
            vec1.X * vec2.Y - vec1.Y * vec2.X);
    }

    public double Length
    { get { return Math.Sqrt(X * X + Y * Y + Z * Z); } }

    public double LengthSq
    { get { return X * X + Y * Y + Z * Z; } }

    public Vector3 Normalised
    { get { return this / this.Length; } }

    public static Vector3 Reflect(Vector3 vec, Vector3 normal)
    {
        double dot = normal.Dot(vec);
        return normal * dot * 2 - vec;
    }

    public static Vector3 operator -(Vector3 vec)
    {
        return new Vector3(-vec.X, -vec.Y, -vec.Z);
    }
}
