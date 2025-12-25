using TMPro;
using UnityEngine;

/// <summary>
/// Система учёта исследовательских очков.
/// Обеспечивает:
/// - Хранение и изменение значения исследовательского счёта
/// - Отображение текущего значения в UI
/// </summary>
public class ResearchScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _maxScoreText;
    [SerializeField] private int _maxScore;
    private int _currentScore;

    /// <summary>
    /// Вызывается один раз при запуске сцены или активации объекта
    /// до вызова методов Start.
    /// </summary>
    private void Start()
    {
        SetAndDisplayMaxScore(_maxScore);
        SetAndDisplay(0);
    }

    public bool CheckScore()
    {
        if (_currentScore >= _maxScore)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Добавляет указанное количество исследовательских очков.
    /// Значение всегда интерпретируется как положительное.
    /// </summary>
    /// <param name="amount">Количество очков для добавления</param>
    public void AddPoints(int amount)
    {
        int absScore = Mathf.Abs(amount);
        int newScore = _currentScore + absScore;
        SetAndDisplay(newScore);
    }

    /// <summary>
    /// Обновляет текущее значение исследовательских очков и отображает его.
    /// </summary>
    /// <param name="newValue">Новое значение счёта</param>
    private void SetAndDisplay(int newValue)
    {
        _currentScore = newValue;
        Display();
    }

    /// <summary>
    /// Обновляет текстовое отображение текущего значения исследовательских очков.
    /// </summary>
    private void Display()
    {
        _scoreText.text = $"{_currentScore}";
    }

    private void SetAndDisplayMaxScore(int newValue)
    {
        _currentScore = newValue;
        _maxScoreText.text = $"{_currentScore}";
    }
}