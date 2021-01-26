using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// UI элемент, имеет события драга и отпускания пальца
/// </summary>
public class OneKeyController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// Событие, вызываемое при драге
    /// </summary>
    public UnityEvent<Vector2> onDrag;
    /// <summary>
    /// Событие вызываемое при поднятии пальца
    /// </summary>
    public UnityEvent<bool> onPointerUp;
    /// <summary>
    /// Смещение пальца за время, пока происходит драг
    /// </summary>
    private Vector2 deltaNormal;


    /// <summary>
    /// Обновляет переменную смещения
    /// </summary>
    /// <param name="data"></param>
    public void OnDrag(PointerEventData data)
    {
        var delta = data.delta;
        delta.Scale(new Vector2(4f / Screen.width, 4f / Screen.width));
        deltaNormal += delta;
    }

    /// <summary>
    /// Обнуляет переменную смещения
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        deltaNormal = Vector2.zero;
    }

    /// <summary>
    /// Вызывает события опускания пальца
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerUp?.Invoke(false);
    }

    /// <summary>
    /// Вызывает событие поднятия пальца
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp?.Invoke(true);
    }

    /// <summary>
    /// Каждый кадр отправляет информацию о смещении в событие onDrag
    /// </summary>
    private void Update()
    {
        var vec = deltaNormal;

#if UNITY_EDITOR
        vec.x = vec.x == 0? Input.GetAxis("Horizontal"): vec.x;
        vec.y = vec.y == 0?  Input.GetAxis("Vertical"): vec.y;

        if (Input.GetKeyDown(KeyCode.X))
        {
            onPointerUp?.Invoke(true);
        }
#endif
        vec = vec.magnitude > 1 ? vec.normalized : vec;
        onDrag?.Invoke(vec);
    }
}
