public class Part
{
    public double Speed { get; set; }
    public double Acceleration { get; set; }
    public double Handling { get; set; }
    public double Grip { get; set; }

    public Part(double speed = 0.0, double acceleration = 0.0, double handling = 0.0, double grip = 0.0)
    {
        Speed = speed;
        Acceleration = acceleration;
        Handling = handling;
        Grip = grip;
    }
}

public class FrontWheel : Part { }

public class BackWheel : Part { }

public class Chassis : Part
{
    public Chassis() : base(0.0, 0.0, 0.0, 0.0) { }
}

public class Engine : Part
{
    public Engine(double speed, double acceleration, double handling) : base(speed, acceleration, handling, 0.0) { }
}

public class Muffler : Part
{
    public Muffler(double acceleration) : base(0.0, acceleration, 0.0, 0.0) { }
}

public class Bike
{
    public FrontWheel FrontWheel { get; set; }
    public BackWheel BackWheel { get; set; }
    public Chassis Chassis { get; set; }
    public Engine Engine { get; set; }
    public Muffler Muffler { get; set; }

    public bool IsComplete => FrontWheel != null && BackWheel != null && Chassis != null && Engine != null && Muffler != null;

    public (double speed, double acceleration, double handling, double grip) TotalParameters()
    {
        if (!IsComplete) return (0.0, 0.0, 0.0, 0.0);
        double speed = 1.0 + FrontWheel.Speed + BackWheel.Speed + Engine.Speed;
        double acceleration = 1.0 + FrontWheel.Acceleration + BackWheel.Acceleration + Engine.Acceleration + Muffler.Acceleration;
        double handling = 1.0 + FrontWheel.Handling + BackWheel.Handling + Engine.Handling;
        double grip = 1.0 + FrontWheel.Grip + BackWheel.Grip;
        return (speed, acceleration, handling, grip);
    }

}