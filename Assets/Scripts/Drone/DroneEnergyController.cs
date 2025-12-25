using TMPro;
using UnityEngine;

/// <summary>
/// Система управления энергией дрона.
/// Обеспечивает:
/// - Хранение и изменение уровня энергии
/// - Отображение текущего значения энергии
/// - Ограничение максимального и минимального значения
/// </summary>
public class DroneEnergyController : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private float _maxEnergy = 100.0f;
    private float _currentEnergy;
    public float CurrentEnergy => _currentEnergy;

    /// <summary>
    /// Вызывается один раз при первой активации объекта
    /// </summary>     
    private void Start()
    {
        SetAndDisplay(_maxEnergy);
    }

    /// <summary>
    /// Добавляет указанное количество энергии.
    /// Значение всегда интерпретируется как положительное.
    /// </summary>
    /// <param name="amount">Количество энергии для добавления</param>
    public void AddEnergy(float amount)
    {
        float newEnergy = _currentEnergy + Mathf.Abs(amount);
        SetAndDisplay(newEnergy);
    }

    /// <summary>
    /// Пытается израсходовать энергию.
    /// Возвращает true, если энергия была успешно израсходована.
    /// </summary>
    /// <param name="energyPerSecond">Интенсивность расхода энергии в единицах/секунду</param>
    /// <returns>True, если энергия доступна и была израсходована</returns>
    public bool TryConsumeEnergy(float energyPerSecond)
    {
        energyPerSecond = Mathf.Abs(energyPerSecond);

        if (_currentEnergy > 0)
        {
            float requiredEnergy = energyPerSecond * Time.fixedDeltaTime;
            float newEnergy = _currentEnergy - requiredEnergy;
            SetAndDisplay(newEnergy);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Обновляет текущий уровень энергии и отображает его.
    /// Автоматически ограничивает значение в допустимом диапазоне.
    /// </summary>
    /// <param name="newEnergy">Новое значение энергии</param>
    private void SetAndDisplay(float newEnergy)
    {
        _currentEnergy = Mathf.Clamp(newEnergy, 0f, _maxEnergy);
        Display();
    }

    /// <summary>
    /// Обновляет текстовое отображение текущего уровня энергии.
    /// Округляет значение до двух знаков после запятой.
    /// </summary>
    private void Display()
    {
        _energyText.text = $"{Mathf.Round(_currentEnergy * 100f) / 100f:F2}";
    }
}