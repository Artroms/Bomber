using System.Collections;
using UnityEngine;

/// <summary>
/// ����� ��� ������������ ��������� �����
/// </summary>
public class BombIndicator : MonoBehaviour
{
    public BombSettings bombSettings;
    private Material sharedMaterial;

    /// <summary>
    /// �������� �������� �����, �.�. OnEnable ��������� ����� ������� � �� ������� NullReferenceException
    /// </summary>
    public void Awake()
    {
        sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial; // �������� ����� ��������, ����� �� �������� ����� ������
    }

    /// <summary>
    /// ��� ����������� ��������� �������� �������
    /// </summary>
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// ��� ��������� �������� �������� �������
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    /// <summary>
    /// ������ ��������� �����, ���������� ����������� �������� ���������, ����� ����������� ������� �������� ������
    /// </summary>
    /// <returns></returns>
    private IEnumerator Timer()
    {
        float time = 0;
        while(time < bombSettings.PlantTime)
        {
            sharedMaterial.SetFloat("Fill", time / bombSettings.PlantTime);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
