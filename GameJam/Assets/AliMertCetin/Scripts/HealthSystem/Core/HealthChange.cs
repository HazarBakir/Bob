using UnityEngine;

namespace XIV.DesignPatterns.Common.HealthSystem
{
    public readonly struct HealthChange
    {
        public readonly HealthData healthDataBefore;
        public readonly HealthData healthDataAfter;
        public readonly ValueChange maxHealthValueChange;
        public readonly ValueChange currentHealthValueChange;
        
        public HealthChange(HealthData healthDataBefore, HealthData healthDataAfter) : this()
        {
            this.healthDataBefore = healthDataBefore;
            this.healthDataAfter = healthDataAfter;
            maxHealthValueChange = GetValueChange(healthDataBefore.maxHealth, healthDataAfter.maxHealth);
            currentHealthValueChange = GetValueChange(healthDataBefore.currentHealth, healthDataAfter.currentHealth);
        }

        static ValueChange GetValueChange(float prev, float curr)
        {
            if (Mathf.Abs(prev - curr) < Mathf.Epsilon)
            {
                return ValueChange.Unchanged;
            }

            return prev < curr ? ValueChange.Increased : ValueChange.Decreased;
        }
    }
}