using UnityEngine;
using UnityEngine.UI;

public abstract class Fish : MonoBehaviour, IGiveDamage
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _lvl;
    [SerializeField] protected int _score;

    [SerializeField] protected Text _textName;
    [SerializeField] protected Text _textLvl;

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
