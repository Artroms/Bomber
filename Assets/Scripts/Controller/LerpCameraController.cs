using UnityEngine;

/// <summary>
/// Контроллер камеры
/// </summary>
public class LerpCameraController : MonoBehaviour
{
	/// <summary>
	/// Цель
	/// </summary>
	public GameObject target;
	/// <summary>
	/// Скорость движения
	/// </summary>
	[SerializeField, Range(1,20)]
	private float moveSpeed = 1;
	/// <summary>
	/// Расстояние по оси Z до цели
	/// </summary>
	[SerializeField, Range(1, 20)]
	private float distance = 5;
	/// <summary>
	/// Расстояние по оси Y до цели
	/// </summary>
	[SerializeField, Range(2, 20)]
	private float height = 2;
	/// <summary>
	/// На сколько единиц смещается камера
	/// </summary>
	public float shiftModifier = 1;
	/// <summary>
	/// Текущее смещение камеры
	/// </summary>
	private float shiftValue = 0;

	/// <summary>
	/// Каждый кадр движем объект
	/// </summary>
    private void Update()
    {
		MoveCamera();
	}
	/// <summary>
	/// Устанавливает параметр смещения
	/// </summary>
	/// <param name="shift">Вектор смещения, ось y не учитывается</param>
	public void Shift(Vector2 shift)
    {
		shiftValue = shift.x * shiftModifier;
    }

	/// <summary>
	/// Линейно интерполирует местопложение объекта относительно цели
	/// </summary>
	private void MoveCamera()
	{
		if (target == null)
			return;
		var relativePos = this.target.transform.position + new Vector3(shiftValue, height, -distance); // Вычисляем необходимое местопложение относительно цели
		transform.position = Vector3.Lerp(transform.position, relativePos, moveSpeed * Time.deltaTime); // Движемся в направлении относительного местоположения
	}

	/// <summary>
	/// Применяет параметры высоты и расстояния к transform
	/// </summary>
    private void OnValidate()
    {
		if(target != null)
	        transform.position = target.transform.position + new Vector3(0, height, -distance);
	}

}
