using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс бомбы, содержит таймер, метод нанесения урона и использует параметры из ScriptableObject
/// </summary>
public class Bomb : MonoBehaviour
{
    /// <summary>
    /// Наносимый урон
    /// </summary>
    [SerializeField] private int damage = 1;
    /// <summary>
    /// Настройки бомбы
    /// </summary>
    [SerializeField] private BombSettings bombSettings;
    /// <summary>
    /// Эффект взрыва
    /// </summary>
    [SerializeField] private GameObject effect;
    private float Distance => bombSettings.ExplosionDistance;
    public float Time => bombSettings.TimeToExplosion;

    void Start()
    {
        StartCoroutine(Timer());
    }

    /// <summary>
    /// Перечислитель, ожидает некоторое время, наносит урон в 4 напрвлениях, спавнит эффекты и уничтожает объект
    /// </summary>
    /// <returns></returns>
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(Time);
        int mask = ~(1 << LayerMask.NameToLayer("Water")); // маска для рейкаста, игнорирует воду
        Physics.BoxCast(transform.position, Vector3.one / 2.1f, transform.forward, out RaycastHit forward, Quaternion.identity, Distance, mask, QueryTriggerInteraction.Ignore);
        {
            TryDamage(forward);
            SpawnLineEffect(forward, transform.forward);
        }
        Physics.BoxCast(transform.position, Vector3.one / 2.1f, -transform.forward, out RaycastHit back, Quaternion.identity, Distance, mask, QueryTriggerInteraction.Ignore);
        {
            TryDamage(back);
            SpawnLineEffect(back, -transform.forward);
        }
        Physics.BoxCast(transform.position, Vector3.one / 2.1f, transform.right, out RaycastHit right, Quaternion.identity, Distance, mask, QueryTriggerInteraction.Ignore);
        {
            TryDamage(right);
            SpawnLineEffect(right, transform.right);
        }
        Physics.BoxCast(transform.position, Vector3.one / 2.1f, -transform.right, out RaycastHit left, Quaternion.identity, Distance, mask, QueryTriggerInteraction.Ignore);
        {
            TryDamage(left);
            SpawnLineEffect(left, -transform.right);
        }
        mask = ~(1 << LayerMask.NameToLayer("Bomb"));  // маска для рейкаста, игнорирует бомбу
        var center = Physics.OverlapSphere(transform.position, 0.4f, mask);
        for (int i = 0; center != null && i < center.Length; i++)
        {
            var health = center[i].GetComponent<Health>();
            if (health != null)
                health.HP -= damage;
        }
        SpawnEffect(transform.position);
        Destroy(gameObject);
    }

    /// <summary>
    /// Наносит урон объекту, если он содержит компонент здоровья и спавнит на нём эффект
    /// </summary>
    /// <param name="raycastHit">Объект, которому нужно нанести урон</param>
    private void TryDamage(RaycastHit raycastHit)
    {
        if (raycastHit.transform == null)
            return;
        var health = raycastHit.collider.gameObject.GetComponent<Health>();
        if (health != null)
        {
            SpawnEffect(raycastHit.transform.position.GridRound()); // спавним эффект, если можно нанести урон
            health.HP -= damage;
        }
    }

    /// <summary>
    /// Спавнит линию эффектов
    /// </summary>
    /// <param name="raycastHit"></param>
    /// <param name="direction"></param>
    private void SpawnLineEffect(RaycastHit raycastHit, Vector3 direction)
    {
        float count = raycastHit.transform != null ? raycastHit.distance : bombSettings.ExplosionDistance; // узнаём количество эффектов
        direction = raycastHit.transform != null ? (raycastHit.point.GridRound() - transform.position).normalized : direction; // узнаём направление линии
        for (int i = 1; i < count; i++)
        {
            var pos = transform.position;
            pos += i * direction;
            SpawnEffect(pos);
        }
    }

    /// <summary>
    /// Спавнит эффект в заданном месте
    /// </summary>
    /// <param name="position"></param>
    private void SpawnEffect(Vector3 position)
    {
        var explosion = Instantiate(effect);
        explosion.transform.position = position;
    }

    private void OnDrawGizmos()
    {
        if (bombSettings == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * Distance);
        Gizmos.DrawRay(transform.position, -transform.forward * Distance);
        Gizmos.DrawRay(transform.position, transform.right * Distance);
        Gizmos.DrawRay(transform.position, -transform.right * Distance);
    }
}
