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

    /// <summary>
    /// Устанавливаем соответствующее здоровье перед первым вызовом Update
    /// </summary>
    void Start()
    {
        hp = characterSettings.Health;
    }
}
