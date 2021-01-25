using System.Collections;
using UnityEngine;

/// <summary>
/// Класс для визуализации установки бомбы
/// </summary>
public class BombIndicator : MonoBehaviour
{
    public BombSettings bombSettings;
    private Material sharedMaterial;

    /// <summary>
    /// Получаем материал здесь, т.к. OnEnable сработает перед стартом и мы получим NullReferenceException
    /// </summary>
    public void Awake()
    {
        sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial; // Получаем общий материал, чтобы не выделять новую память
    }

    /// <summary>
    /// При деактивации выключаем корутину таймера
    /// </summary>
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// При активации включаем корутину таймера
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    /// <summary>
    /// Таймер установки бомбы, использует специальный параметр материала, после прохождения времени скрывает объект
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
