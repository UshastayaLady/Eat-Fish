using UnityEngine;

public class Enemy : Fish
{
    [SerializeField] private FishScriptableObject _scriptableObject;
    private bool _isDestroying = false;

    private void Awake()
    {
        GetComponent<MeshCollider>().convex = true;
    }

    public void InitializationFish(FishScriptableObject scriptable)
    {
        _name = scriptable.FishName;
        _textName.text = _name;

        _lvl = scriptable.Lvl; 
        _textLvl.text = _lvl.ToString();

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
