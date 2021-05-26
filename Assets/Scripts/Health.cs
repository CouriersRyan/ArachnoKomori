using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth;
    private float _currHealth;

    public float currHealth => _currHealth;

    private void Awake()
    {
        _currHealth = startingHealth;
    }

    public void ChangeHealth(float delta)
    {
        _currHealth += delta;
    }
}