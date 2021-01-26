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
    [SerializeField] private HealthBar healthBar;

    /// <summary>
    /// ������������� ��������������� �������� ����� ������ ������� Update
    /// </summary>
    void Start()
    {
        hp = characterSettings.Health;
        healthBar = Instantiate(healthBar);
        healthBar.target = transform;
        healthBar.maxValue = hp;
        onDamage.AddListener(healthBar.SetHealthInfo);
    }


    private void OnDisable()
    {
        healthBar?.gameObject.SetActive(false);
    }
}
