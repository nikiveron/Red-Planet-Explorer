using UnityEngine;

/// <summary>
/// Компонент, представляющий исследовательский элемент, который может быть собран дроном.
/// При физическом контакте передаёт заданное количество исследовательских очков и уничтожается.
/// </summary>
public class ResearchItem : MonoBehaviour
{
    [SerializeField] private int _researchValue = 10;

    /// <summary>
    /// Вызывается при входе другого объекта в триггерную зону.
    /// Проверяет наличие компонента DroneResearchCollector и
    /// передаёт исследовательские очки.
    /// </summary>
    /// <param name="other">Объект, вошедший в триггер</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent<DroneResearchCollector>(out var collector))
        {
            collector.Collect(_researchValue);
            Destroy(gameObject);
        }
    }
}