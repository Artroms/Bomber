using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public float onNearFrequency = 1;
    /// <summary>
    /// —обытие, дл€ отправки направлени€ движени€
    /// </summary>
    public UnityEvent<Vector2> onPlayerFound;
    /// <summary>
    /// —обытие дл€ атаки
    /// </summary>
    public UnityEvent onPlayerNear;
    /// <summary>
    /// »грок
    /// </summary>
    private static GameObject player;
    /// <summary>
    /// —лучайна€ цель
    /// </summary>
    private Transform place;
    /// <summary>
    /// Ќаходитс€ ли персонаж в зоне видимости
    /// </summary>
    private bool found;

    private void Start()
    {
        // »щем игрока один раз дл€ всех экземпл€ров данного класса
        if (player == null)
            player = FindObjectOfType<BombPaster>(true).gameObject;
        ChangeDirection(); // Ќаходим случайную цель движени€
        StartCoroutine(FindPlayer()); // Ќачинаем провер€ть видимость игрока
        StartCoroutine(FindDirection()); // Ќачинаем мен€ть случайную цель движени€
        StartCoroutine(PlayerNearRoutine()); // ѕровер€ем рассто€ние до игрока и вызываем событие
    }

    // Update is called once per frame
    private void Update()
    {
        // ≈сли игрок найден, то вызываем событие с вектором направлени€ к игроку или атакуем
        if (player != null && found)
        {
            var direction = player.transform.position - transform.position;
            var vec2Dir = new Vector2(direction.x, direction.z);
            vec2Dir = vec2Dir.magnitude > 1 ? vec2Dir.normalized : vec2Dir;
            onPlayerFound.Invoke(vec2Dir);
        }
        else // »наче идЄм к случайной цели
        {
            // »щем, куда идти, если цель была уничтожена в прошлом кадре
            if (place == null)
                ChangeDirection();
            var direction = place.position - transform.position;
            // ≈сли рассто€ние до цели меньше 1, то пытаемс€ сменить цель до тех пор, пока рассто€ние не станет больше 1 или пройдЄт 4 попытки
            if (direction.magnitude < 1)
            {
                for (int i = 0; i < 4 || !ChangeDirection(); i++) ;
            }
            var vec2Dir = new Vector2(direction.x, direction.z);
            vec2Dir = vec2Dir.magnitude > 1 ? vec2Dir.normalized : vec2Dir;
            onPlayerFound.Invoke(vec2Dir);
        }
    }

    /// <summary>
    /// ѕеречислитель дл€ смены цели движени€, используетс€, чтобы противник двигалс€ менее однообразно
    /// </summary>
    /// <returns></returns>
    public IEnumerator FindDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            ChangeDirection();
        }
    }

    /// <summary>
    /// ћен€ет цель движени€ в случайную сторону
    /// </summary>
    /// <returns>¬озвращает true, если рассто€ние до новой цели меньше 1, иначе false</returns>
    private bool ChangeDirection()
    {
        RaycastHit raycastHit = new RaycastHit();
        int r = Random.Range(0, 4);
        var pos = transform.position.GridRound();
        switch (r)
        {
            case 0:
                {
                    Physics.Raycast(pos, Vector3.forward, out raycastHit);
                    break;
                }
            case 1:
                {
                    Physics.Raycast(pos, -Vector3.forward, out raycastHit);
                    break;
                }
            case 2:
                {
                    Physics.Raycast(pos, Vector3.right, out raycastHit);
                    break;
                }
            case 3:
                {
                    Physics.Raycast(pos, -Vector3.right, out raycastHit);
                    break;
                }
            default:
                {
                    break;
                }
        }

        if (raycastHit.transform != null)
        {
            place = raycastHit.transform;
            if((raycastHit.transform.position - transform.position).magnitude > 1)
                return true;
        }
        return false;
    }

    /// <summary>
    /// ѕроверка, находитс€ ли игрок в зоне видимости
    /// </summary>
    /// <returns></returns>
    public IEnumerator FindPlayer()
    {
        float wait = 1;
        while (true && player != null)
        {

            var direction = player.transform.position - transform.position;

            // ≈сли игрок вне зоны видимости, то ничего не делаем
            if (Vector3.Angle(transform.forward, direction) > 90 || direction.magnitude > 15)
            {
                found = false;
                yield return new WaitForSeconds(wait);
                continue;
            }

            // ƒелаем рейкаст в сторону персонажа, если на пути нет преп€тствий, то персонаж найден
            Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, direction.magnitude);
            if (raycastHit.transform == null || raycastHit.transform == player.transform)
            {
                found = true;
            }
            else
            {
                // ≈сли персонаж скрылс€ из вида
                if (found == true) 
                {
                    direction = direction.normalized.Round(); // Ќаправление в котором скрылс€ персонаж
                    Physics.Raycast(transform.position, direction, out RaycastHit raycastHit2);
                    place = raycastHit2.transform != null && raycastHit2.transform.GetComponent<Bomb>() == null ? raycastHit2.transform : place; // продолжаем движение в направлении персонажа
                }
                found = false;
            }
            yield return new WaitForSeconds(wait);
        }
    }

    private IEnumerator PlayerNearRoutine()
    {
        while(true && player != null)
        {
            var direction = player.transform.position - transform.position;
            // если находимс€ близко к игроку, удар€ем его и останавливаемс€
            if (direction.magnitude < 1)
            {
                onPlayerNear?.Invoke();
                found = false;
                yield return new WaitForSeconds(onNearFrequency);
            }
            else
            {
                yield return null;
            }
        }
    }
}
