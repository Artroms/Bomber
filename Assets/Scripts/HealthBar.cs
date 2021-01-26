using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;
    public int maxValue;

    [SerializeField] private Image innerImage;
    [SerializeField] private Text currentHealth;

    private static Canvas canvas;

    public void Start()
    {
        if(canvas == null)
            canvas = FindObjectOfType<Canvas>();
        gameObject.transform.SetParent(canvas.transform);
        SetHealthInfo(maxValue);
    }

    public void SetHealthInfo(int healthValue)
    {
        currentHealth.text = healthValue.ToString();
        innerImage.fillAmount = (float)healthValue / maxValue;
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + Vector3.up * 2);
    }
}
