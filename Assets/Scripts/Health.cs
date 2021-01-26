using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����� �������� ������� ������� ��������� ����� � ���������� ����� ��������
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// ���� ��������
    /// </summary>
    [SerializeField] protected int hp = 1;
    /// <summary>
    /// ���� ��������
    /// </summary>
    public int HP
    {
        get => hp;
        set
        {
            onDamage?.Invoke(value); // ����� ������� ��������� �����
            hp = value;
            if (value <= 0)
                onDead?.Invoke(); // ����� ������� ���������� ����� ��������
        }
    }
    /// <summary>
    /// ������� ��������� �����
    /// </summary>
    public UnityEvent<int> onDamage;
    /// <summary>
    /// ������� ���������� ����� ��������
    /// </summary>
    public UnityEvent onDead;

    /// <summary>
    /// ����� ����������� �������, ������ ��� ���������� ������� ����������� ����� ��������� ��� ���������� ����� ��������
    /// </summary>
    public virtual void Destroy()
    {
        MonoBehaviour.Destroy(gameObject);
    }
}
