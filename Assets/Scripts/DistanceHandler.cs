using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для визуализации расстояния до финиша
/// </summary>
public class DistanceHandler : MonoBehaviour
{
    /// <summary>
    /// Игрок
    /// </summary>
    private GameObject player;
    /// <summary>
    /// Конец уровня
    /// </summary>
    private GameObject levelEnd;
    /// <summary>
    /// Слайдер, для визализации
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
    /// Обновляем значение слайдера
    /// </summary>
    void Update()
    {
        if(player != null)
            slider.value = player.transform.position.z;
    }
}
