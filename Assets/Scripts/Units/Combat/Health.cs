using System;
using UnityEngine;

/// <summary>
///     Defenders have health and can take damage.
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    ///     The total amount of hp this defender can have.
    /// </summary>
    [SerializeField] private int _maxHp;

    /// <summary>
    ///     Hit points. Classic video game mechanic.
    /// </summary>
    public int hp { get; private set; }

    public int maxHp => _maxHp;

    private void Awake()
    {
        hp = _maxHp;
    }

    private void Start()
    {
        OnHealthCreated(this);
    }

    /// <summary>
    ///     Event that fires once this defender dies.
    /// </summary>
    public event Action OnDeath;

    /// <summary>
    ///     Event that fires whenever a Health is created, with that new Health as a parameter.
    /// </summary>
    public static event Action<Health> OnHealthCreated = delegate { };

    /// <summary>
    ///     Hit this Health for dmg damage, taking away its health.
    /// </summary>
    /// <param name="dmg">The amount of damage to deal.</param>
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) Die();
    }

    /// <summary>
    ///     Die.
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
        OnDeath?.Invoke();
    }
}