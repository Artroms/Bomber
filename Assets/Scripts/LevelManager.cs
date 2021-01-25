using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс для управления уровнем
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Останавливает все действия, связанные со временем
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Возобновляет все действия связанные со временем
    /// </summary>
    public void UnPause()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Загружает последний непройденный уровень
    /// </summary>
    public void LoadLastLevel()
    {
        SceneManager.LoadSceneAsync(Levels.Instance.LastLevelName);
    }

    /// <summary>
    /// Если объект будет уничтожен, то пауза всё равно прекратится
    /// </summary>
    private void OnDisable()
    {
        UnPause();
    }
}
