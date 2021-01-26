using System.Collections;
using UnityEngine;

/// <summary>
/// Класс для установки бомбы
/// </summary>
public class BombPaster : MonoBehaviour
{
    /// <summary>
    /// Префаб бомбы
    /// </summary>
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject indicator;
    /// <summary>
    /// Ссылка на таймер установки бомбы
    /// </summary>
    private IEnumerator bombTimer;
    /// <summary>
    /// Настройки бомбы
    /// </summary>
    [SerializeField] private BombSettings bombSettings;
    private float Time => bombSettings.PlantTime;
    private int currentBombCount = 0;

    /// <summary>
    /// Метод вызывающий таймер установки бомбы или обрывающий его
    /// </summary>
    /// <param name="place">Начать установку или прервать</param>
    public void PlaceBomb(bool place)
    {
        if (place)
            StartTimer();
        else
            StopTimer();
    }

    /// <summary>
    /// Метод включающий таймер установки бомбы
    /// </summary>
    private void StartTimer()
    {
        if (currentBombCount >= bombSettings.MaxBombCount) // Проверяем допустимое количество бомб
            return;
        indicator.SetActive(true);
        if (bombTimer == null)
        {
            bombTimer = BombTimer();
            StartCoroutine(bombTimer);
        }
    }

    /// <summary>
    /// Метод обрывающий таймер
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
    /// Таймер установки бомбы, ждёт некоторое время и ставит бомбу
    /// </summary>
    /// <returns></returns>
    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(Time);
        StartCoroutine(BombCounter());
        var b = Instantiate(bomb);
        b.transform.position = transform.position.GridRound(); // Округлённое местоположение персонажа
        bombTimer = null;
        yield break;
    }

    private IEnumerator BombCounter()
    {
        currentBombCount++;
        yield return new WaitForSeconds(bombSettings.TimeToExplosion);
        currentBombCount--;
    }
}
