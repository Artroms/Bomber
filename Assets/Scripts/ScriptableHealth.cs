using UnityEngine;

/// <summary>
/// Модифицированный класс здоровья, дли использования вместе с CharacterSettings
/// </summary>
public class ScriptableHealth : Health
{
    /// <summary>
    /// Настройки персонажа
    /// </summary>
    [SerializeField] private CharacterSettings characterSettings;
    [SerializeField] private HealthBar healthBar;

    /// <summary>
    /// Устанавливаем соответствующее здоровье перед первым вызовом Update
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
