using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Контроллер дрона, отвечающий:
/// - за обработку пользовательского ввода
/// - расчёт сил тяги двигателей
/// - физическое перемещение объекта
/// </summary>
public class DroneController : MonoBehaviour
{
    [SerializeField] private DroneEnergyController _droneEnergy;
    [SerializeField] private Transform _leftEnginePosition;
    [SerializeField] private Transform _rightEnginePosition;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _mainThrust = 10f;
    [SerializeField] private float _attitudeThrust = 5f;
    [SerializeField] private float _energyConsumptionRate = 2f;
    [SerializeField] private float _maxHeight = 10f;
    private Vector2 _direction;
    public Vector3 Velocity => _rigidbody.velocity;
    public bool IsInteractive { get; set; }

    private void Awake()
    {
        IsInteractive = true;
        _direction = Vector2.zero;
    }

    private void Start()
    {
        _rigidbody.sleepThreshold = 0.0f;
    }

    private void FixedUpdate()
    {
        if (IsInteractive == false) return;
        ApplyEngineForces();
    }

    /// <summary>
    /// Обрабатывает событие ввода от игрока.
    /// Сохраняет направление движения в _direction.
    /// </summary>
    /// <param name="value">Значение ввода (Vector2)</param>
    public void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
    }

    /// <summary>
    /// Распределяет силу тяги между левым и правым двигателем.
    /// </summary>
    private void ApplyEngineForces()
    {
        float leftThrust = CalculateThrust(_direction.x, _direction.y);
        float rightThrust = CalculateThrust(_direction.x * -1, _direction.y);

        ApplyEngineForce(leftThrust, _leftEnginePosition.position);
        ApplyEngineForce(rightThrust, _rightEnginePosition.position);
    }

    /// <summary>
    /// Рассчитывает значение тяги для двигателя.
    /// Комбинирует вертикальное и горизонтальное управление.
    /// </summary>
    /// <param name="horizontalInput">Горизонтальный ввод</param>
    /// <param name="verticalInput">Вертикальный ввод</param>
    /// <returns>Значение тяги (всегда неотрицательное)</returns>
    private float CalculateThrust(float horizontalInput, float verticalInput)
    {
        float thrustValue = _mainThrust * verticalInput + _attitudeThrust * horizontalInput;
        float actualThrust = Mathf.Clamp(thrustValue, 0, float.MaxValue);
        return actualThrust;
    }

    /// <summary>
    /// Применяет силу тяги к указанной точке.
    /// Учитывает уровень энергии и высоту полёта.
    /// </summary>
    /// <param name="thrustValue">Значение тяги</param>
    /// <param name="enginePosition">Точка приложения силы</param>
    private void ApplyEngineForce(float thrustValue, Vector3 enginePosition)
    {
        if (_droneEnergy.TryConsumeEnergy(thrustValue * _energyConsumptionRate))
        {
            float powerFactor = CalculatePowerFactor();
            float actualThrust = thrustValue * powerFactor;

            _rigidbody.AddForceAtPosition(
                transform.up * actualThrust,     // Направление тяги
                enginePosition,                  // Точка приложения
                ForceMode.Acceleration          // Режим применения силы
            );
        }
    }

    /// <summary>
    /// Рассчитывает коэффициент мощности двигателей в зависимости от высоты.
    /// При достижении максимальной высоты тяга плавно снижается.
    /// </summary>
    /// <returns>Коэффициент мощности (0-1)</returns>
    private float CalculatePowerFactor()
    {
        float currentHeight = transform.position.y;
        float heightRatio = Mathf.Clamp01((_maxHeight - currentHeight) / _maxHeight);
        return heightRatio * heightRatio;
    }
}