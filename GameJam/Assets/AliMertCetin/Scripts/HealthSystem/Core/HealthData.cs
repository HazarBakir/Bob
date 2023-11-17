using System;

namespace XIV.DesignPatterns.Common.HealthSystem
{
    public readonly struct HealthData : IEquatable<HealthData>
    {
        public readonly float maxHealth;
        public readonly float currentHealth;
        public readonly float normalized;
        public readonly bool isDepleted;

        public HealthData(float maxHealth, float currentHealth, bool isDepleted) : this()
        {
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.normalized = currentHealth / maxHealth;
            this.isDepleted = isDepleted;
        }

        public static bool operator ==(HealthData a, HealthData b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(HealthData a, HealthData b)
        {
            return !(a == b);
        }

        public bool Equals(HealthData other)
        {
            return maxHealth.Equals(other.maxHealth) && currentHealth.Equals(other.currentHealth) && normalized.Equals(other.normalized) && isDepleted == other.isDepleted;
        }

        public override bool Equals(object obj)
        {
            return obj is HealthData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(maxHealth, currentHealth, normalized, isDepleted);
        }
    }
}