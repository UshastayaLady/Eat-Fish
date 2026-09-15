using UnityEngine;

[CreateAssetMenu(fileName = "New fish", menuName = "Create Item/New fish")]
public class FishScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private string _name;
    [SerializeField] protected int _lvl;
    [SerializeField] protected int _score;


    public GameObject Model => _model;
    public string FishName => _name;    
    public int Lvl => _lvl;
    public int Score => _score;
}
