using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float _timeSpawn;

    private Enemy _enemy;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            _enemy = ObjectPool.Instance.TakeEnemy();
            _enemy.transform.parent = this.transform;
            _enemy.gameObject.SetActive(true);

            yield return new WaitForSeconds(_timeSpawn);
        }
    }
}
