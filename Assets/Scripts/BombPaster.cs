using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPaster : MonoBehaviour
{
    /// <summary>
    /// Префаб бомбы
    /// </summary>
    [SerializeField] private GameObject bomb;
    /// <summary>
    /// Ссылка на таймер установки бомбы
    /// </summary>
    private IEnumerator bombTimer;


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
    /// Метод активирующий пустышку и включающий таймер установки бомбы
    /// </summary>
    private void StartTimer()
    {
        if (bombTimer == null)
        {
            bombTimer = BombTimer();
            StartCoroutine(bombTimer);
        }
    }

    /// <summary>
    /// Метод скрывающий пустышку и обрывающий таймер
    /// </summary>
    private void StopTimer()
    {
        if (bombTimer != null)
        {
            StopCoroutine(bombTimer);
            bombTimer = null;
        }
    }

    /// <summary>
    /// Таймер установки бомбы, ждём некоторое время, скрываем пустышку и ставим бомбу на её место
    /// </summary>
    /// <returns></returns>
    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(1);
        var b = Instantiate(bomb);
        b.transform.position = transform.position.GridRound(); // Округлённое местоположение персонажа
        bombTimer = null;
        yield break;
    }
}
