using UnityEngine;

/// <summary>
/// Контроллер персонажа имеющий методы перемещения и поворота
/// </summary>
public class CharacterAxisController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private float Speed => characterSettings.Speed;

    [SerializeField, Range(1, 40)]
    private float acceleration; // параметр, для плавной работы анимаций
    private Vector2 smoothDirection; // переменная хранящая линейно интерполированный вектор направления движения

    [SerializeField] private CharacterSettings characterSettings;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    public void CompositeMove(Vector2 direction)
    {
        smoothDirection = Vector2.Lerp(smoothDirection, direction, acceleration * Time.deltaTime); // обновляем переменную в соответствии с новым входным параметром
        Move(direction);
        Rotate();
    }
    /// <summary>
    /// Двигает объект в заданную сторону по осям X и Z
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector2 direction)
    {

        var directionVec3 = new Vector3(direction.x, 0, direction.y);
        controller.SimpleMove(directionVec3 * Speed); // движемся прямо с заданной скоростью и в соответствии с входным параметром
        animator.SetFloat("InputMagnitude", smoothDirection.magnitude * Speed / 4); // устанавливаем плавную скорость анимации, делим на 4, для соответствия скорости перемещения и скорости анимации
    }

    /// <summary>
    /// Плавно поворачивает объект
    /// </summary>
    private void Rotate()
    {
        if (smoothDirection == Vector2.zero)
            return;
        var directionVec3 = new Vector3(smoothDirection.x, 0, smoothDirection.y);
        transform.rotation = Quaternion.LookRotation(directionVec3);
    }

}
