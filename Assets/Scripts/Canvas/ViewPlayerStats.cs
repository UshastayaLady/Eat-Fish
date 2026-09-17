using UnityEngine;
using UnityEngine.UI;


public class ViewPlayerStats : MonoBehaviour
{
    [SerializeField] protected Text _lvlTxt;
    [SerializeField] private Slider _scoreSlider;

    private void OnEnable()
    {
        PlayerStats.Instance.ScoreUpdated += UpdateSlider;
        PlayerStats.Instance.LvlRisen += WriteText;
    }

    private void UpdateSlider(float valueProgrss)
    {
        _scoreSlider.value = valueProgrss;
    }
    private void WriteText(int lvlPlayr)
    {
        _lvlTxt.text = "Уровень " + lvlPlayr.ToString();
    }

    private void OnDisable()
    {
        PlayerStats.Instance.ScoreUpdated -= UpdateSlider;
        PlayerStats.Instance.LvlRisen -= WriteText;
    }
}
