using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Fish
{
    [SerializeField] private int _scoreForNextLvl;
    [SerializeField] private Text _textScoreForNextLvl;
    private const int constForSpeed = 2500;

    public event Action LvlRisen;

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

        _textName.text = _name;
        _textLvl.text = "Уровень: " + _lvl.ToString();
        _textScoreForNextLvl.text = _score.ToString() + " / " + _scoreForNextLvl.ToString();
    }
     
    public void AddScore(int score)
    {
        _score += score;        
        CheckScore();
    }

    private void CheckScore()
    {
        if (_score >= _scoreForNextLvl)
        {
            LvlUp();
        }

        _textScoreForNextLvl.text = _score.ToString() + " / " + _scoreForNextLvl.ToString();
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
        
        LvlRisen?.Invoke();

        _textLvl.text = "Уровень: " + _lvl.ToString();
    }

    protected override void Death()
    {
        Debug.Log("Конец игры");
    }
}
