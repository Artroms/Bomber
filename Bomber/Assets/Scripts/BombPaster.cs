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
    /// Пустышка в виде момбы, используется для предпросмотра местоположения бомбы
    /// </summary>
    [SerializeField] private GameObject bombDummy;
    /// <summary>
    /// Ссылка на таймер установки бомбы
    /// </summary>
    private IEnumerator bombTimer;

    /// <summary>
    /// Создаём пустышку и скрываем её
    /// </summary>
    private void Start()
    {
        bombDummy = Instantiate(bombDummy);
        bombDummy.SetActive(false);
    }

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
        if (!CheckPlace())
            return;
        bombDummy.SetActive(true);
        bombDummy.transform.position = (transform.position + transform.forward.Round()).GridRound(); // Округляем местоположение пустышки, чтобы она стояла посередине клетки
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
        bombDummy.SetActive(false);
        if (bombTimer != null)
        {
            StopCoroutine(bombTimer);
            bombTimer = null;
        }
    }
    /// <summary>
    /// Проверяем, есть ли объект на том месте, куда мы хотим поставить бомбу
    /// </summary>
    /// <returns></returns>
    private bool CheckPlace()
    {
        var pos = (transform.position + transform.forward.Round()).GridRound();
        Collider[] colliders = Physics.OverlapSphere(pos, 0.2f);
        return colliders == null || colliders.Length == 0;
    }

    /// <summary>
    /// Таймер установки бомбы, ждём некоторое время, скрывает пустышку и ставит бомбу
    /// </summary>
    /// <returns></returns>
    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(1);
        bombDummy.SetActive(false);
        var b = Instantiate(bomb);
        b.transform.position = bombDummy.transform.position; // устанавливаем бомбу на место пустышки, т.к. персонаж мог немного сместить с момента начала установки
        bombTimer = null;
        yield break;
    }
}
