using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Tooltip("Maximum amount of health")] 
    public float maxHealth = 10f;
    
    public bool m_IsDead;
     
    public UnityAction<float> onDamaged;
    
    public UnityAction GetDamage;
    
    public UnityAction onDie; 
    
    public float currentHealth;
    public bool invincible { get; set;}

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (invincible)
            return;
        
        GetDamage.Invoke();
        
        float healthBefore = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        
        float trueDamageAmount = healthBefore - currentHealth;
        if (trueDamageAmount > 0f)
        {
            onDamaged?.Invoke(trueDamageAmount);
        }

        HandleDeath();
    }

    public void Kill()
    {
        currentHealth = 0f;

        // call OnDamage action
        onDamaged?.Invoke(maxHealth);
    }

    private void HandleDeath()
    {
        if (m_IsDead)
            return;

        // call OnDie action
        if (currentHealth <= 0f)
        {
            m_IsDead = true;
            onDie?.Invoke();
        }
    }
    
    [ContextMenu("Take Damage Test")]
    public void TestHit()
    {
        TakeDamage(0);
    } 
    
}