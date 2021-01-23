using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPaster : MonoBehaviour
{
    /// <summary>
    /// ������ �����
    /// </summary>
    [SerializeField] private GameObject bomb;
    /// <summary>
    /// ������ �� ������ ��������� �����
    /// </summary>
    private IEnumerator bombTimer;


    /// <summary>
    /// ����� ���������� ������ ��������� ����� ��� ���������� ���
    /// </summary>
    /// <param name="place">������ ��������� ��� ��������</param>
    public void PlaceBomb(bool place)
    {
        if (place)
            StartTimer();
        else
            StopTimer();
    }

    /// <summary>
    /// ����� ������������ �������� � ���������� ������ ��������� �����
    /// </summary>
    private void StartTimer()
    {
        if (bombTimer == null)
        {
            bombTimer = BombTimer();
            StartCoroutine(bombTimer);
        }
    }

    /// <summary>
    /// ����� ���������� �������� � ���������� ������
    /// </summary>
    private void StopTimer()
    {
        if (bombTimer != null)
        {
            StopCoroutine(bombTimer);
            bombTimer = null;
        }
    }

    /// <summary>
    /// ������ ��������� �����, ��� ��������� �����, �������� �������� � ������ ����� �� � �����
    /// </summary>
    /// <returns></returns>
    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(1);
        var b = Instantiate(bomb);
        b.transform.position = transform.position.GridRound(); // ���������� �������������� ���������
        bombTimer = null;
        yield break;
    }
}
