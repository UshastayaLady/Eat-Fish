using UnityEngine;

public abstract class Fish : MonoBehaviour, IGiveDamage
{
    [SerializeField] protected int _lvl;
    [SerializeField] protected int _score;

    private bool _isDead;

    int IGiveDamage.Lvl => _lvl;

    private void OnEnable()
    {
        _isDead = false;
    }

    public void GiveDamage()
    {
        if (_isDead)
            return;

        _isDead = true;
        Death();
    }

    protected abstract void Death();
}
