using UnityEngine;

/// <summary>
/// Компонент обработки столкновений дрона.
/// Реагирует на физические столкновения и активирует разрушение при превышении пороговой скорости.
/// </summary>
public class DroneHitHandler : MonoBehaviour
{
    [SerializeField] private DroneDestructor _destructor;
    [SerializeField] private float destructionSpeedThreshold = 3f;

    /// <summary>
    /// Вызывается при физическом столкновении с другим объектом.
    /// Проверяет скорость столкновения и активирует разрушение при необходимости.
    /// </summary>
    /// <param name="collision">Данные о столкновении</param>
    private void OnCollisionEnter(Collision collision)
    {
        float impactSpeed = collision.relativeVelocity.magnitude;
        if (impactSpeed > destructionSpeedThreshold)
        {
            _destructor.Detonate();
        }
    }
}