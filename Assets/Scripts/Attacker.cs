using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ласс дл€ нанесени€ урона в ближнем бою
/// </summary>
public class Attacker : MonoBehaviour
{
    /// <summary>
    /// Ёффект атаки
    /// </summary>
    [SerializeField] private GameObject effect;
    public void Attack()
    {
        int mask = (1 << LayerMask.NameToLayer("Player")); // маска, при которой провер€етс€ только слой персонажа
        var colliders = Physics.OverlapSphere(transform.position, 1, mask);
        for (int i = 0; i < colliders.Length; i++)
        {
            var health = colliders[i].GetComponent<Health>();
            if (health != null)
                health.HP--;
        }
        // спавним эффект, если ударили игрока
        if (colliders != null && colliders.Length != 0)
        {
            var hit = Instantiate(effect);
            hit.transform.position = transform.position.GridRound();
        }
    }
}
