using UnityEngine;

/// <summary>
/// Компонент, реализующий логику разрушения дрона со взрывным эффектом.
/// Обрабатывает:
/// - Отделение дочерних объектов
/// - Применение физических эффектов взрыва
/// - Уведомление GameManager о поражении
/// </summary>
public class DroneDestructor : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _upwardsModifier = 0f;
    private bool _isDestroyed;
    public bool IsDestroyed => _isDestroyed;

    /// <summary>
    /// Вызывается один раз при запуске сцены или активации объекта
    /// до вызова методов Start.
    /// </summary>    
    private void Awake()
    {
        _isDestroyed = false;
    }

    /// <summary>
    /// Активирует процесс разрушения дрона.
    /// Вызывается только один раз, если дрон ещё не разрушен.
    /// </summary>
    public void Detonate()
    {
        if (_isDestroyed == true) return;
        Explode();
        AfterExplode();
    }

    /// <summary>
    /// Выполняет физический взрыв дрона:
    /// - Отделяет все дочерние объекты
    /// - Добавляет физические компоненты
    /// - Применяет силы взрыва
    /// </summary>
    private void Explode()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child == transform) continue;
            child.parent = null;
            Rigidbody childRigidbody = InitializeChildPhysics(child.gameObject);
            ApplyExplosionForce(childRigidbody);
        }
        _gameManager.OnLose();
    }

    /// <summary>
    /// Добавляет Rigidbody к дочернему объекту и настраивает ограничения.
    /// </summary>
    /// <param name="childObject">Дочерний объект</param>
    /// <returns>Созданный Rigidbody</returns>
    private Rigidbody InitializeChildPhysics(GameObject childObject)
    {
        Rigidbody childRigidbody = childObject.AddComponent<Rigidbody>();
        childRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        return childRigidbody;
    }

    /// <summary>
    /// Применяет силу взрыва к дочернему объекту.
    /// Использует метод Unity AddExplosionForce.
    /// </summary>
    /// <param name="childRigidbody">Rigidbody дочернего объекта</param>
    private void ApplyExplosionForce(Rigidbody childRigidbody)
    {
        childRigidbody.AddExplosionForce(
            _explosionForce,          // Сила взрыва
            transform.position,       // Центр взрыва
            _explosionRadius,         // Радиус действия
            _upwardsModifier,         // Вертикальный модификатор
            ForceMode.Impulse         // Режим приложения силы
        );
    }

    /// <summary>
    /// Выполняет завершающие действия после взрыва:
    /// - Уничтожает основной объект
    /// - Устанавливает флаг разрушения
    /// </summary>
    private void AfterExplode()
    {
        Destroy(gameObject);
        _isDestroyed = true;
    }
}