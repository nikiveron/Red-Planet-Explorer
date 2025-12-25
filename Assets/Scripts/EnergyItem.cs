using UnityEngine;

/// <summary>
/// Компонент, представляющий элемент энергии, который может быть собран дроном.
/// При физическом контакте передаёт заданное количество эергии дрону и уничтожается.
/// </summary>
public class EnergyItem : MonoBehaviour
{
    [SerializeField] private int _energyValue = 10;

    /// <summary>
    /// Вызывается при входе другого объекта в триггерную зону.
    /// Проверяет наличие компонента DroneEnergyController и
    /// передаёт очки энергии.
    /// </summary>
    /// <param name="other">Объект, вошедший в триггер</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent<DroneEnergyController>(out var controller))
        {
            controller.AddEnergy(_energyValue);
            Destroy(gameObject);
        }
    }
}