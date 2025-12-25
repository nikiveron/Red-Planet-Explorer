using UnityEngine;

/// <summary>
/// Компонент для сбора исследовательских очков дроном.
/// Связывается с системой учёта исследований и передаёт собранные значения.
/// </summary>
public class DroneResearchCollector : MonoBehaviour
{
    [SerializeField] private ResearchScore _researchScore;

    /// <summary>
    /// Передаёт полученное количество исследовательских очков системе подсчёта.
    /// </summary>
    /// <param name="researchValue">Количество очков для добавления</param>
    public void Collect(int researchValue)
    {
        _researchScore.AddPoints(researchValue);
    }
}