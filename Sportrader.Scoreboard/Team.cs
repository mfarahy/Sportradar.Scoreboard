﻿namespace Sportrader.Scoreboard
{
    public class Team : IEquatable<Team>
    {
        public Team(int id, string name, string flag)
        {
            Id = id;
            Name = name;
            Flag = flag;
        }

        public int Id { get; }
        public string Name { get; }
        public string Flag { get; }

        public bool Equals(Team? other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Team another && another != null)
                return another.Id == Id;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"${Id} - {Name}({Flag})";
        }
    }
}
