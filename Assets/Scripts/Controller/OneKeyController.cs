using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OneKeyController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent<Vector2> onDrag;
    public UnityEvent<bool> onAllowBomb;

    public Vector2 deltaNormal;



    public void OnDrag(PointerEventData data)
    {
        var delta = data.delta;
        delta.Scale(new Vector2(4f / Screen.width, 4f / Screen.width));
        deltaNormal += delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        deltaNormal = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onAllowBomb?.Invoke(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onAllowBomb?.Invoke(true);
    }

    private void Update()
    {
        var vec = deltaNormal.magnitude > 1 ? deltaNormal.normalized : deltaNormal;
        onDrag?.Invoke(vec);
    }
}
