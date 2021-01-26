using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public float onNearFrequency = 1;
    /// <summary>
    /// �������, ��� �������� ����������� ��������
    /// </summary>
    public UnityEvent<Vector2> onPlayerFound;
    /// <summary>
    /// ������� ��� �����
    /// </summary>
    public UnityEvent onPlayerNear;
    /// <summary>
    /// �����
    /// </summary>
    private static GameObject player;
    /// <summary>
    /// ��������� ����
    /// </summary>
    private Transform place;
    /// <summary>
    /// ��������� �� �������� � ���� ���������
    /// </summary>
    private bool found;

    private void Start()
    {
        // ���� ������ ���� ��� ��� ���� ����������� ������� ������
        if (player == null)
            player = FindObjectOfType<BombPaster>(true).gameObject;
        ChangeDirection(); // ������� ��������� ���� ��������
        StartCoroutine(FindPlayer()); // �������� ��������� ��������� ������
        StartCoroutine(FindDirection()); // �������� ������ ��������� ���� ��������
        StartCoroutine(PlayerNearRoutine()); // ��������� ���������� �� ������ � �������� �������
    }

    // Update is called once per frame
    private void Update()
    {
        // ���� ����� ������, �� �������� ������� � �������� ����������� � ������ ��� �������
        if (player != null && found)
        {
            var direction = player.transform.position - transform.position;
            var vec2Dir = new Vector2(direction.x, direction.z);
            vec2Dir = vec2Dir.magnitude > 1 ? vec2Dir.normalized : vec2Dir;
            onPlayerFound.Invoke(vec2Dir);
        }
        else // ����� ��� � ��������� ����
        {
            // ����, ���� ����, ���� ���� ���� ���������� � ������� �����
            if (place == null)
                ChangeDirection();
            var direction = place.position - transform.position;
            // ���� ���������� �� ���� ������ 1, �� �������� ������� ���� �� ��� ���, ���� ���������� �� ������ ������ 1 ��� ������ 4 �������
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
    /// ������������� ��� ����� ���� ��������, ������������, ����� ��������� �������� ����� �����������
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
    /// ������ ���� �������� � ��������� �������
    /// </summary>
    /// <returns>���������� true, ���� ���������� �� ����� ���� ������ 1, ����� false</returns>
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
    /// ��������, ��������� �� ����� � ���� ���������
    /// </summary>
    /// <returns></returns>
    public IEnumerator FindPlayer()
    {
        float wait = 1;
        while (true && player != null)
        {

            var direction = player.transform.position - transform.position;

            // ���� ����� ��� ���� ���������, �� ������ �� ������
            if (Vector3.Angle(transform.forward, direction) > 90 || direction.magnitude > 15)
            {
                found = false;
                yield return new WaitForSeconds(wait);
                continue;
            }

            // ������ ������� � ������� ���������, ���� �� ���� ��� �����������, �� �������� ������
            Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, direction.magnitude);
            if (raycastHit.transform == null || raycastHit.transform == player.transform)
            {
                found = true;
            }
            else
            {
                // ���� �������� ������� �� ����
                if (found == true) 
                {
                    direction = direction.normalized.Round(); // ����������� � ������� ������� ��������
                    Physics.Raycast(transform.position, direction, out RaycastHit raycastHit2);
                    place = raycastHit2.transform != null && raycastHit2.transform.GetComponent<Bomb>() == null ? raycastHit2.transform : place; // ���������� �������� � ����������� ���������
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
            // ���� ��������� ������ � ������, ������� ��� � ���������������
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
