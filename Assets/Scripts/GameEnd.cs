using UnityEngine;
using UnityEngine.Events;

/// <summary>
///  омпонент €вл€ющийс€ финишем уровн€
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class GameEnd : MonoBehaviour
{
    /// <summary>
    /// —обытие при достижении игроком финиша
    /// </summary>
    public UnityEvent onWin;

    private void OnTriggerEnter(Collider other)
    {
        var bombPaster = other.GetComponent<BombPaster>(); // »спользуем BombPaster, чтобы не провер€ть строки и не создавать дополнительные классы
        if(bombPaster != null)
        {
            Levels.Instance.LastLevel++; // указываем, что уровень пройден
            onWin?.Invoke();
        }
    }
}
