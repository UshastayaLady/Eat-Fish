using System;
using UnityEngine;

public class PlayerStats : Fish
{
    [SerializeField] private int _scoreForNextLvl;
    private const int constForSpeed = 2500;

    public event Action<int> LvlRisen;
    public event Action<float> ScoreUpdated;

    public static PlayerStats Instance { get; private set; }
      
    private void Awake()
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

    private void Start()
    {
        LvlRisen?.Invoke(_lvl);
        ScoreUpdated?.Invoke(TakeProgressScore());
    }
     
    public void AddScore(int score)
    {
        _score += score;
        CheckScore();
    }
    private float TakeProgressScore()
    {
        if (_scoreForNextLvl <= 0)
            return 0f;
        return Mathf.Clamp01((float)_score / _scoreForNextLvl);
    }

    private void CheckScore()
    {
        if (_score >= _scoreForNextLvl)
        {
            LvlUp();
        }
        ScoreUpdated?.Invoke(TakeProgressScore());
    }

    private void LvlUp()
    {
        _lvl++;
        _score -= _scoreForNextLvl;
        if (_lvl < 12)
        {
            _scoreForNextLvl = (constForSpeed * _lvl * (_lvl - 1));
        }
        else
        {
            _scoreForNextLvl = Mathf.RoundToInt(
                constForSpeed * _lvl * (_lvl - 1)
                * Mathf.Pow( ((_lvl + 19) / 30f), 2));
        }
        
        LvlRisen?.Invoke(_lvl);
    }

    protected override void Death()
    {
        Debug.Log("Конец игры");
    }
}
