using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��� ��������� ����� � ������� ���
/// </summary>
public class Attacker : MonoBehaviour
{
    /// <summary>
    /// ������ �����
    /// </summary>
    [SerializeField] private GameObject effect;
    public void Attack()
    {
        int mask = (1 << LayerMask.NameToLayer("Player")); // �����, ��� ������� ����������� ������ ���� ���������
        var colliders = Physics.OverlapSphere(transform.position, 1, mask);
        for (int i = 0; i < colliders.Length; i++)
        {
            var health = colliders[i].GetComponent<Health>();
            if (health != null)
                health.HP--;
        }
        // ������� ������, ���� ������� ������
        if (colliders != null && colliders.Length != 0)
        {
            var hit = Instantiate(effect);
            hit.transform.position = transform.position.GridRound();
        }
    }
}
