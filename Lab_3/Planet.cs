﻿using Lab_3;

namespace Lab_3
{
    public class Planet
    {
        private PlanetType _type;
        private string _name;
        private double _mass;
        private double _radius;
        private double _distanceFromSun;
        private bool _hasLife;
        private int _age;

        public double OrbitalSpeed { get; private set; } = 29.78;
        public PlanetType Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(PlanetType), value))
                {
                    throw new ArgumentException("Invalid planet type. Must be 0-3.");
                }
                _type = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 30)
                {
                    throw new ArgumentException("Name must be 1-30 characters");
                }
                _name = value;
            }
        }
        public double Mass
        {
            get => _mass;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Mass must be positive.");
                _mass = value;
            }
        }
        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Radius must be positive.");
                _radius = value;
            }
        }
        public int Age
        {
            get => _age;
            private set => _age = value;
        }
        public bool HasLife
        {
            get => _hasLife;
            set => _hasLife = value;
        }
        public double DistanceFromSun
        {
            get => _distanceFromSun;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Distance must be >= 0.");
                _distanceFromSun = value;
            }
        }
        public double DistanceTraveled => OrbitalSpeed * Age * 60 * 60 * 24 * 365;

        public Planet(PlanetType type, string name, double mass, double radius, double distanceFromSun, bool hasLife)
        {
            Type = type;
            Name = name;
            Mass = mass;
            Radius = radius;
            DistanceFromSun = distanceFromSun;
            HasLife = hasLife;
            Age = 0;
        }

        public Planet()
        {
            Type = PlanetType.Terrestrial;
            Name = ".";
            Mass = 1;
            Radius = 1;
            DistanceFromSun = 0;
            HasLife = false;
            Age = 0;
        }

        public Planet(PlanetType type, string name)
            : this(type, name, 5.97e24, 6.371e6, 150e6, false)
        {

        }

        public Planet(Random rnd1)
        {
            var rnd = new Random();
            string[] names = { "Xenon", "Astra", "Orbis", "Nova", "Kronos", "Zephyr" };
            Type = (PlanetType)rnd.Next(0, 4);
            Name = names[rnd.Next(names.Length)] + rnd.Next(100, 999);
            Mass = rnd.NextDouble() * 1e25 + 1e22;
            Radius = rnd.NextDouble() * 7e7 + 1e6;
            DistanceFromSun = rnd.NextDouble() * 5000;
            HasLife = rnd.Next(0, 2) == 1;
            Age = 0;
        }

        public double GetGravity() => CalculateGravity();

        private double CalculateGravity()
        {
            const double G = 6.67430e-11;
            return G * Mass / (Radius * Radius);
        }

        public string MakeYearPass()
        {
            Age++;
            return $"{Name} made a full circle around the Sun. Age is now {Age} years.";
        }

        public string MakeYearPass(int age)
        {
            Age += age;
            return $"{Name} made a full circle around the Sun. Age is now {Age} years.";
        }

        public string BirthLife()
        {
            if (HasLife) return $"Error: life already exists on {Name}!";
            HasLife = true;
            return $"Life has begun on {Name}!";
        }

        public string DestroyLife()
        {
            if (!HasLife)
                return $"Error: {Name} already has no life!";
            HasLife = false;
            return $"Life on {Name} has been destroyed...";
        }

        public override string ToString()
        {
            return string.Format(
                "{0,-12} | {1,-12} | {2,12:E2} | {3,12:E2} | {4,12:E2} | {5,-3} | {6,5} | {7,12:F2} | {8,12:E2}",
                Name,
                Type,
                Mass,
                Radius,
                DistanceFromSun,
                HasLife ? "Yes" : "No",
                Age,
                OrbitalSpeed,
                DistanceTraveled
            );
        }
    }
}
