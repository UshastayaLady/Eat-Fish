using UnityEngine;
using UnityEngine.UI;

public class ViewEnemyStats : MonoBehaviour
{
    [SerializeField] protected Text _lvlTxt;

    private void Start()
    {
        WriteText();
    }

    protected virtual void WriteText()
    {
        if (TryGetComponent<IGiveDamage>(out IGiveDamage fishEnemy))
            _lvlTxt.text = fishEnemy.Lvl.ToString();
    }
}
