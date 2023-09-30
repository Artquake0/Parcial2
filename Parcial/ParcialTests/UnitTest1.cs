
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
        _bike = new Bike();
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

    [Test]
    public void PartCanBeAdded()
    {
        var frontWheel = new FrontWheel();
        var backWheel = new BackWheel();
        var engine = new Engine(2.0, 2.0, 2.0);
        var muffler = new Muffler(1.5);
        var chassis = new Chassis();
        _bike.AddOrReplaceFrontWheel(frontWheel);
        Assert.AreEqual(frontWheel, _bike.FrontWheel);
        _bike.AddOrReplaceBackWheel(backWheel);
        Assert.AreEqual(backWheel, _bike.BackWheel);
        _bike.AddOrReplaceEngine(engine);
        Assert.AreEqual(engine, _bike.Engine);
        _bike.AddOrReplaceMuffler(muffler);
        Assert.AreEqual(muffler, _bike.Muffler);
        Assert.IsFalse(_bike.ReplaceChassis(chassis));
        Assert.IsNull(_bike.Chassis);
    }

    [Test]
    public void PartsModifyParameters()
    {
        var (speed, acceleration, handling, grip) = _bike.TotalParameters();
        Assert.AreEqual(0.0, speed);
        Assert.AreEqual(0.0, acceleration);
        Assert.AreEqual(0.0, handling);
        Assert.AreEqual(0.0, grip);


        var engine = new Engine(0.0, 0.0, 0.0);
        _bike.AddOrReplaceEngine(engine);
        (speed, acceleration, handling, grip) = _bike.TotalParameters();
        Assert.AreEqual(0.0, speed);
        var frontWheel = new FrontWheel { Speed = 2.0, Acceleration = 2.0, Handling = 2.0, Grip = 2.0 };
        _bike.AddOrReplaceFrontWheel(frontWheel);
        (speed, acceleration, handling, grip) = _bike.TotalParameters();
        Assert.AreEqual(2.0, speed);
        Assert.AreEqual(2.0, acceleration);
        Assert.AreEqual(2.0, handling);
        Assert.AreEqual(2.0, grip);


        var backWheel = new BackWheel { Speed = 1.0, Acceleration = 1.0, Handling = 1.0, Grip = 1.0 };
        var muffler = new Muffler(1.5);
        var chassis = new Chassis();
        _bike.AddOrReplaceBackWheel(backWheel);
        _bike.AddOrReplaceMuffler(muffler);
        _bike.AddOrReplaceChassis(chassis);
        (speed, acceleration, handling, grip) = _bike.TotalParameters();
        Assert.AreEqual(4.0, speed);
        Assert.AreEqual(4.5, acceleration);
        Assert.AreEqual(4.0, handling);
        Assert.AreEqual(4.0, grip);

        var newEngine = new Engine(3.0, 3.0, 3.0);
        _bike.AddOrReplaceEngine(newEngine);
        (speed, acceleration, handling, grip) = _bike.TotalParameters();
        Assert.AreEqual(7.0, speed);
        Assert.AreEqual(7.5, acceleration);
        Assert.AreEqual(7.0, handling);
        Assert.AreEqual(4.0, grip);


    }

    [Test]
    public void MaxParameterValueOnConstructor()
    {
 
        var engine = new Engine(4.0, 3.0, 2.0);
        Assert.AreEqual(4.0, engine.Speed);
        Assert.AreEqual(3.0, engine.Acceleration);
        Assert.AreEqual(2.0, engine.Handling);
        Assert.AreEqual(0.0, engine.Grip);

        var frontWheel = new FrontWheel { Speed = 2.0, Acceleration = 2.0, Handling = 2.0, Grip = 2.0 };
        Assert.AreEqual(2.0, frontWheel.Speed);
        Assert.AreEqual(2.0, frontWheel.Acceleration);
        Assert.AreEqual(2.0, frontWheel.Handling);
        Assert.AreEqual(2.0, frontWheel.Grip);

        var backWheel = new BackWheel { Speed = 1.0, Acceleration = 1.0, Handling = 1.0, Grip = 1.0 };
        Assert.AreEqual(1.0, backWheel.Speed);
        Assert.AreEqual(1.0, backWheel.Acceleration);
        Assert.AreEqual(1.0, backWheel.Handling);
        Assert.AreEqual(1.0, backWheel.Grip);

        var muffler = new Muffler(1.5);
        Assert.AreEqual(0.0, muffler.Speed);
        Assert.AreEqual(1.5, muffler.Acceleration);
        Assert.AreEqual(0.0, muffler.Handling);
        Assert.AreEqual(0.0, muffler.Grip);

        var chassis = new Chassis();
        Assert.AreEqual(0.0, chassis.Speed);
        Assert.AreEqual(0.0, chassis.Acceleration);
        Assert.AreEqual(0.0, chassis.Handling);
        Assert.AreEqual(0.0, chassis.Grip);
    }
}