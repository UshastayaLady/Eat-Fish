using UnityEngine;

public class FishHeadTrigger : MonoBehaviour
{

    private Fish _owner;
    private IGiveDamage _ownerIntarfase;

    private void Awake()
    {
        _owner = GetComponentInParent<Fish>();
        _ownerIntarfase = _owner as IGiveDamage;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Fish targetFish = other.GetComponentInParent<Fish>();

        if (targetFish == null || targetFish == _owner)
            return;

        IGiveDamage target = targetFish;

        if (_ownerIntarfase.Lvl < target.Lvl)
        {
            _ownerIntarfase.GiveDamage();
        }
        else if (_ownerIntarfase.Lvl > target.Lvl)
        {
            target.GiveDamage();
        }
        else
        {
            // При равных уровнях побеждает игрок
            if (_owner is PlayerStats)
                target.GiveDamage();
            else if (targetFish is PlayerStats)
                _ownerIntarfase.GiveDamage();
        }
    }
}
