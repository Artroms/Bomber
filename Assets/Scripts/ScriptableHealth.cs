using UnityEngine;

/// <summary>
/// ���������������� ����� ��������, ��� ������������� ������ � CharacterSettings
/// </summary>
public class ScriptableHealth : Health
{
    /// <summary>
    /// ��������� ���������
    /// </summary>
    [SerializeField] private CharacterSettings characterSettings;

    /// <summary>
    /// ������������� ��������������� �������� ����� ������ ������� Update
    /// </summary>
    void Start()
    {
        hp = characterSettings.Health;
    }
}
