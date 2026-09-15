using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<Enemy> _enemies;
    private int _lvlEnemies;
    public static ObjectPool Instance { get; private set; }
        
    void Awake()
    {        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _lvlEnemies = 3;
        _enemies = new Queue<Enemy>();
    }

    private void OnEnable()
    {       
        PlayerStats.Instance.LvlRisen += ClearQueue;
    }

    public void PutEnemy(Enemy enemy)
    {
        enemy.transform.parent = this.transform;
        _enemies.Enqueue(enemy);
    }

    public Enemy TakeEnemy()
    {
        if (_enemies.Count > 0)
            return _enemies.Dequeue();

        return CreateFish();

    }

    private void ClearQueue()
    {
        int count = _enemies.Count;
        for (int i = 0; i < count; i++)
        {
            Enemy enemy = _enemies.Dequeue();
            enemy.DestroyEnemy();
        }
        _lvlEnemies++;
    }

    private Enemy CreateFish()
    {
        FishScriptableObject fishData = CreateEnemyForLvl.Instance.GetEnemy(_lvlEnemies);

        GameObject enemyObject = Instantiate(fishData.Model);


        Enemy enemy = enemyObject.GetComponent<Enemy>();

        enemy.InitializationFish(fishData);

        return enemy;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.LvlRisen -= ClearQueue;
    }
}
