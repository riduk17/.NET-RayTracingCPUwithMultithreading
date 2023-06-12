struct Ray
{
    public const double Epsilon = 0.00001;
    public const double Huge = double.MaxValue;

    public Ray(Vector3 origin, Vector3 direction)
        : this()
    {
        this.Origin = origin;
        this.Direction = direction.Normalised;
    }

    public Vector3 Origin { get; set; }
    public Vector3 Direction { get; set; }
}