using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyForLvl : MonoBehaviour
{
    [SerializeField] private List<FishScriptableObject> _enemies;

    [SerializeField] private const int _constMaxLvlEnemies = 2; 
    private int _minLvl;
    private int _maxLvl;

    public static CreateEnemyForLvl Instance { get; private set; }
     

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public FishScriptableObject GetEnemy(int lvl)
    {
        if (lvl > _constMaxLvlEnemies - 2)
            lvl = _constMaxLvlEnemies - 2;

        _minLvl = lvl - 2;
        _maxLvl = lvl + 2;

        List<FishScriptableObject> availableEnemies =
            new List<FishScriptableObject>();

        foreach (FishScriptableObject enemy in _enemies)
        {
            if (_minLvl <= enemy.Lvl && enemy.Lvl <= _maxLvl)
            {
                availableEnemies.Add(enemy);
            }
        }

        int randomIndex = Random.Range(0, availableEnemies.Count);

        return availableEnemies[randomIndex];
    }
}
