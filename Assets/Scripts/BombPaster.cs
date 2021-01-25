using System.Collections;
using UnityEngine;

/// <summary>
/// ����� ��� ��������� �����
/// </summary>
public class BombPaster : MonoBehaviour
{
    /// <summary>
    /// ������ �����
    /// </summary>
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject indicator;
    /// <summary>
    /// ������ �� ������ ��������� �����
    /// </summary>
    private IEnumerator bombTimer;
    /// <summary>
    /// ��������� �����
    /// </summary>
    [SerializeField] private BombSettings bombSettings;
    private float Time => bombSettings.PlantTime;

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
    /// ����� ���������� ������ ��������� �����
    /// </summary>
    private void StartTimer()
    {
            indicator.SetActive(true);
        if (bombTimer == null)
        {
            bombTimer = BombTimer();
            StartCoroutine(bombTimer);
        }
    }

    /// <summary>
    /// ����� ���������� ������
    /// </summary>
    private void StopTimer()
    {
        indicator.SetActive(false);
        if (bombTimer != null)
        {
            StopCoroutine(bombTimer);
            bombTimer = null;
        }
    }

    /// <summary>
    /// ������ ��������� �����, ��� ��������� ����� � ������ �����
    /// </summary>
    /// <returns></returns>
    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(Time);
        var b = Instantiate(bomb);
        b.transform.position = transform.position.GridRound(); // ���������� �������������� ���������
        bombTimer = null;
        yield break;
    }
}
