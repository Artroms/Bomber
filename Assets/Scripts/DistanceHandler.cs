using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����� ��� ������������ ���������� �� ������
/// </summary>
public class DistanceHandler : MonoBehaviour
{
    /// <summary>
    /// �����
    /// </summary>
    private GameObject player;
    /// <summary>
    /// ����� ������
    /// </summary>
    private GameObject levelEnd;
    /// <summary>
    /// �������, ��� �����������
    /// </summary>
    private Slider slider;

    void Start()
    {
        player = FindObjectOfType<BombPaster>().gameObject;
        levelEnd = FindObjectOfType<GameEnd>().gameObject;
        slider = GetComponent<Slider>();
        slider.minValue = player.transform.position.z;
        slider.maxValue = levelEnd.transform.position.z;
    }

    /// <summary>
    /// ��������� �������� ��������
    /// </summary>
    void Update()
    {
        if(player != null)
            slider.value = player.transform.position.z;
    }
}
