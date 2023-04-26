using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Helpers
{
    public class Roulette
    {
        private static Random _random = new Random();
    
        private readonly List<Sector> _sectors;

        public Roulette(Dictionary<int, double> edges)
        {
            var sectors = new List<Sector>();

            double onePercentLength = 1.0d / 100;
            double sectorFrom = 0;

            foreach (var edge in edges)
            {
                var sectorTo = sectorFrom + edge.Value * onePercentLength;

                var sector = new Sector(edge.Key, sectorFrom, sectorTo);
                sectors.Add(sector);

                sectorFrom = sectorTo;
            }

            _sectors = sectors;
        }

        public int Roll()
        {
            var value = _random.NextDouble();

            foreach (var sector in _sectors)
            {
                if (IsValueInSector(value, sector))
                {
                    return sector.Id;
                }
            }
        
            return 0;
        }

        private static bool IsValueInSector(double value, Sector sector)
        {
            return value >= sector.From && value < sector.To;
        }
    }
    
    public class Sector
    {
        public int Id { get; }
        public double From { get; }
        public double To { get; }

        public Sector(int id, double from, double to)
        {
            Id = id;
            From = from;
            To = to;
        }
    }

    [Serializable]
    public class ProbabilityCell<T>
    {
        [SerializeField] private T _item;
        [SerializeField] private float _probability;
    
        public T Item => _item;

        public float Probability
        {
            get
            {
                return _probability;
            }

            set
            {
                _probability = value;
            }
        }
    }
}