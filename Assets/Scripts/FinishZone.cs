using UnityEngine;

/// <summary>
/// Компонент, реализующий логику посадки дрона в зоне приземления.
/// Проверяет условия (скорость, угол, время) и активирует
/// победное состояние при успешной посадке.
/// </summary>
public class FinishZone : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ResearchScore _coinScore;
    [SerializeField] private float _maxAngle = 5f;
    [SerializeField] private float _minSpeed = 0.1f;
    [SerializeField] private float _landingTime = 2f;
    private float _timer;

    /// <summary>
    /// Вызывается при физическом контакте с другим объектом.
    /// Проверяет условия посадки и запускает таймер при их соблюдении.
    /// </summary>
    /// <param name="collision">Данные о столкновении</param>
    private void OnCollisionStay(Collision collision)
    {
        DroneController drone = collision.gameObject.GetComponent<DroneController>();

        if (drone == null || drone.IsInteractive == false) return;

        if (IsLandingConditionsMet(drone))
        {
            _timer += Time.deltaTime;

            if (_timer >= _landingTime)
            {
                CompleteLanding(drone);
            }
        }
        else
        {
            _timer = 0f;
        }
    }

    /// <summary>
    /// Проверяет условия для успешной посадки:
    /// - Все монетки собраны
    /// - Скорость дрона должна быть меньше _minSpeed
    /// - Угол между дроном и вертикалью должен быть <= _maxAngle
    /// </summary>
    /// <param name="drone">Контроллер дрона</param>
    /// <returns>True, если оба условия соблюдены</returns>
    private bool IsLandingConditionsMet(DroneController drone)
    {
        bool scoreCondition = _coinScore.CheckScore();
        bool speedCondition = drone.Velocity.magnitude < _minSpeed;
        bool angleCondition = Vector3.Angle(Vector3.up, drone.transform.up) <= _maxAngle;

        if (!scoreCondition)
        {
            _gameManager.OnLackOfScore();
        }
        else if (!speedCondition || !angleCondition)
        {
            _gameManager.OnIncorrectLanding();
        }

        return speedCondition && angleCondition && scoreCondition;
    }

    /// <summary>
    /// Завершает процесс посадки:
    /// - Отключает взаимодействие дрона
    /// - Уведомляет GameManager о победе
    /// </summary>
    /// <param name="drone">Контроллер дрона</param>
    private void CompleteLanding(DroneController drone)
    {
        drone.IsInteractive = false;
        _gameManager.OnWin();
    }
}