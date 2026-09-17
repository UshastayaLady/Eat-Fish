using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<EnemyStats> _enemies;
    private int _lvlPlayr;
    public static ObjectPool Instance { get; private set; }
        
    void Awake()
    {        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _enemies = new Queue<EnemyStats>();
    }

    private void OnEnable()
    {       
        PlayerStats.Instance.LvlRisen += ClearQueue;
    }

    public void PutEnemy(EnemyStats enemy)
    {
        enemy.transform.parent = this.transform;
        _enemies.Enqueue(enemy);
    }

    public EnemyStats TakeEnemy()
    {
        if (_enemies.Count > 0)
            return _enemies.Dequeue();

        return CreateFish();

    }

    private void ClearQueue(int lvlPlayr)
    {
        int count = _enemies.Count;
        for (int i = 0; i < count; i++)
        {
            EnemyStats enemy = _enemies.Dequeue();
            enemy.DestroyEnemy();
        }
        _lvlPlayr = lvlPlayr;
    }

    private EnemyStats CreateFish()
    {
        FishScriptableObject fishData = CreateEnemyForLvl.Instance.GetEnemy(_lvlPlayr);

        GameObject enemyObject = Instantiate(fishData.Model);


        EnemyStats enemy = enemyObject.GetComponent<EnemyStats>();

        enemy.InitializationFish(fishData);

        return enemy;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.LvlRisen -= ClearQueue;
    }
}
