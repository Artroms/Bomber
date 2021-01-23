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
    /// �������� � ���� �����, ������������ ��� ������������� �������������� �����
    /// </summary>
    [SerializeField] private GameObject bombDummy;
    /// <summary>
    /// ������ �� ������ ��������� �����
    /// </summary>
    private IEnumerator bombTimer;

    /// <summary>
    /// ������ �������� � �������� �
    /// </summary>
    private void Start()
    {
        bombDummy = Instantiate(bombDummy);
        bombDummy.SetActive(false);
    }

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
        if (!CheckPlace())
            return;
        bombDummy.SetActive(true);
        bombDummy.transform.position = (transform.position + transform.forward.Round()).GridRound(); // ��������� �������������� ��������, ����� ��� ������ ���������� ������
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
        bombDummy.SetActive(false);
        if (bombTimer != null)
        {
            StopCoroutine(bombTimer);
            bombTimer = null;
        }
    }
    /// <summary>
    /// ���������, ���� �� ������ �� ��� �����, ���� �� ����� ��������� �����
    /// </summary>
    /// <returns></returns>
    private bool CheckPlace()
    {
        var pos = (transform.position + transform.forward.Round()).GridRound();
        Collider[] colliders = Physics.OverlapSphere(pos, 0.2f);
        return colliders == null || colliders.Length == 0;
    }

    /// <summary>
    /// ������ ��������� �����, ��� ��������� �����, �������� �������� � ������ �����
    /// </summary>
    /// <returns></returns>
    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(1);
        bombDummy.SetActive(false);
        var b = Instantiate(bomb);
        b.transform.position = bombDummy.transform.position; // ������������� ����� �� ����� ��������, �.�. �������� ��� ������� �������� � ������� ������ ���������
        bombTimer = null;
        yield break;
    }
}
