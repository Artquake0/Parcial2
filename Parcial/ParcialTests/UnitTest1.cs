
using NUnit.Framework;

[TestFixture]
public class BikeTests
{
    private Bike _bike;
    private Bike _completeBike;
    private Bike _bikeWithoutEngine;
    private Bike _bikeWithoutWheel;
    private Bike _bikeWithoutMuffler;

    [SetUp]
    public void Setup()
    {
        var frontWheel = new FrontWheel { Speed = 1.0, Acceleration = 1.0, Handling = 1.0, Grip = 1.0 };
        var backWheel = new BackWheel { Speed = 1.0, Acceleration = 1.0, Handling = 1.0, Grip = 1.0 };
        var chassis = new Chassis();
        var engine = new Engine(2.0, 2.0, 2.0);
        var muffler = new Muffler(1.5);
        _bike = new Bike { FrontWheel = frontWheel, BackWheel = backWheel, Chassis = chassis, Engine = engine, Muffler = muffler };
        _completeBike = new Bike { FrontWheel = frontWheel, BackWheel = backWheel, Chassis = chassis, Engine = engine, Muffler = muffler };
        _bikeWithoutEngine = new Bike { FrontWheel = frontWheel, BackWheel = backWheel, Chassis = chassis, Muffler = muffler };
        _bikeWithoutWheel = new Bike { FrontWheel = null, BackWheel = backWheel, Chassis = chassis, Engine = engine, Muffler = muffler };
        _bikeWithoutMuffler = new Bike { FrontWheel = frontWheel, BackWheel = backWheel, Chassis = chassis, Engine = engine };
    }

    [Test]
    public void BikeCanBeCreated()
    {
        Assert.IsNotNull(_bike);
        Assert.IsTrue(_bike.IsComplete);
        var (speed, acceleration, handling, grip) = _bike.TotalParameters();
        Assert.AreEqual(5.0, speed);
        Assert.AreEqual(6.5, acceleration);
        Assert.AreEqual(4.0, handling);
        Assert.AreEqual(3.0, grip);
    }

    [Test]
    public void BikeWithoutChassis()
    {
        _bike.Chassis = null;
        Assert.IsNotNull(_bike);
        Assert.IsFalse(_bike.IsComplete);
        var (speed, acceleration, handling, grip) = _bike.TotalParameters();
        Assert.AreEqual(0.0, speed);
        Assert.AreEqual(0.0, acceleration);
        Assert.AreEqual(0.0, handling);
        Assert.AreEqual(0.0, grip);
    }

}