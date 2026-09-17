using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float _timeSpawn;
    [SerializeField] private float _spawnOffset = 2f;
    private float _radius;
    private EnemyStats _enemyTemp;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            if (transform.childCount < 30)
            {
                _enemyTemp = ObjectPool.Instance.TakeEnemy();
                _enemyTemp.transform.parent = transform;
                _enemyTemp.transform.position = GetSpawnPosition();
                _enemyTemp.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(_timeSpawn);
        }
    }
    private float GetSpawnRadius()
    {
        Camera camera = Camera.main;

        Vector3 playerPosition = PlayerStats.Instance.transform.position;

        Plane groundPlane = new Plane(Vector3.up, playerPosition);

        Vector2[] screenCorners =
        {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f)
        };

        float maximumDistance = 0f;

        foreach (Vector2 corner in screenCorners)
        {
            Ray ray = camera.ViewportPointToRay(corner);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 cornerPosition = ray.GetPoint(distance);

                float distanceFromPlayer = Vector3.Distance(
                    playerPosition,
                    cornerPosition
                );

                maximumDistance = Mathf.Max(
                    maximumDistance,
                    distanceFromPlayer
                );
            }
        }

        return maximumDistance + _spawnOffset;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        _radius = GetSpawnRadius();

        return PlayerStats.Instance.transform.position + 
            new Vector3(direction.x, 0f, direction.y) * _radius;
    }
}


