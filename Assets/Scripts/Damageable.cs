using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Health health;
    private void Awake()
    {
        // find the health component either at the same level, or higher in the hierarchy
        health = GetComponent<Health>();
    }

    public void InflictDamage(int attackDetails)
    {
        if (health)
        {
            var totalDamage = attackDetails;
            // apply the damages
            health.TakeDamage(totalDamage);
        }
    }
        
}