using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Класс здоровья имеющий события нанесения урона и отсутствия очков здоровья
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// Очки здоровья
    /// </summary>
    [SerializeField] protected int hp = 1;
    /// <summary>
    /// Очки здоровья
    /// </summary>
    public int HP
    {
        get => hp;
        set
        {
            onDamage?.Invoke(value); // вызов события нанесения урона
            hp = value;
            if (value <= 0)
                onDead?.Invoke(); // вызов события отсутствия очков здоровья
        }
    }
    /// <summary>
    /// Событие получения урона
    /// </summary>
    public UnityEvent<int> onDamage;
    /// <summary>
    /// Событие отсутствия очков здоровья
    /// </summary>
    public UnityEvent onDead;

    /// <summary>
    /// Метод уничтожения объекта, удобен для добавления события уничтожения через инспектор при отсутствии очков здоровья
    /// </summary>
    public virtual void Destroy()
    {
        MonoBehaviour.Destroy(gameObject);
    }
}
