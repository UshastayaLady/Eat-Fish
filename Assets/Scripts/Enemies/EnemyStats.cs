using UnityEngine;

public class EnemyStats : Fish
{
    [SerializeField] private FishScriptableObject _scriptableObject;
    private bool _isDestroying = false;

    public void InitializationFish(FishScriptableObject scriptable)
    {
        _lvl = scriptable.Lvl; 
        _score = scriptable.Score;
    }
    protected override void Death()
    {
        PlayerStats.Instance.AddScore(_score);
        this.gameObject.SetActive(false);
    }

    public void DestroyEnemy()
    {
        _isDestroying = true;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (_isDestroying)
            return;

        ObjectPool.Instance.PutEnemy(this);
    }
}
