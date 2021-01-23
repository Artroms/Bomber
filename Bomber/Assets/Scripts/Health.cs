using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    public int HP
    {
        get => hp;
        set
        {
            onDamage?.Invoke(hp - value);
            hp = value;
            if (value <= 0)
                onDead?.Invoke();
        }
    }
    public UnityEvent<int> onDamage;
    public UnityEvent onDead;

    public void Destroy()
    {
        MonoBehaviour.Destroy(gameObject);
    }
}
