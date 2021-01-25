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
	[SerializeField, Range(5, 20)]
	private float distance = 5;
	/// <summary>
	/// Расстояние по оси Y до цели
	/// </summary>
	[SerializeField, Range(2, 20)]
	private float height = 2;

	/// <summary>
	/// Каждый кадр движем объект
	/// </summary>
    private void Update()
    {
		MoveCamera();
	}

	/// <summary>
	/// Линейно интерполирует местопложение объекта относительно цели
	/// </summary>
	private void MoveCamera()
	{
		if (target == null)
			return;
		var relativePos = this.target.transform.position + new Vector3(0, height, -distance); // Вычисляем необходимое местопложение относительно цели
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
