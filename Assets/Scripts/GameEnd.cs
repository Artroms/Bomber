using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��������� ���������� ������� ������
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class GameEnd : MonoBehaviour
{
    /// <summary>
    /// ������� ��� ���������� ������� ������
    /// </summary>
    public UnityEvent onWin;

    private void OnTriggerEnter(Collider other)
    {
        var bombPaster = other.GetComponent<BombPaster>(); // ���������� BombPaster, ����� �� ��������� ������ � �� ��������� �������������� ������
        if(bombPaster != null)
        {
            Levels.Instance.LastLevel++; // ���������, ��� ������� �������
            onWin?.Invoke();
        }
    }
}
