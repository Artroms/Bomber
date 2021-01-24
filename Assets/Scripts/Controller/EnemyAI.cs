using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent<Vector2> onPlayerFound;
    private static GameObject player;
    private Transform place;
    private bool found;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = FindObjectOfType<BombPaster>(true).gameObject;
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (found)
        {
            var direction = player.transform.position - transform.position;
            var vec2Dir = new Vector2(direction.x, direction.z);
            vec2Dir = vec2Dir.magnitude > 0.7f ? vec2Dir.normalized * 0.7f : vec2Dir;
            onPlayerFound.Invoke(vec2Dir);
        }
        else
        {
            var direction = place.position - transform.position;
            if (direction.magnitude < 1)
            {
                ChangeDirection();
            }
            var vec2Dir = new Vector2(direction.x, direction.z);
            vec2Dir = vec2Dir.magnitude > 0.7f ? vec2Dir.normalized * 0.7f : vec2Dir;
            onPlayerFound.Invoke(vec2Dir);
        }
    }

    public IEnumerator FindDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ChangeDirection();
        }
    }

    private bool ChangeDirection()
    {
        RaycastHit raycastHit = new RaycastHit();
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                {
                    Physics.Raycast(transform.position, Vector3.forward, out raycastHit);
                    break;
                }
            case 1:
                {
                    Physics.Raycast(transform.position, -Vector3.forward, out raycastHit);
                    break;
                }
            case 2:
                {
                    Physics.Raycast(transform.position, Vector3.right, out raycastHit);
                    break;
                }
            case 3:
                {
                    Physics.Raycast(transform.position, -Vector3.right, out raycastHit);
                    break;
                }
            default:
                {
                    break;
                }
        }

        if (raycastHit.transform != null && (raycastHit.transform.position - transform.position).magnitude > 1)
        {
            place = raycastHit.transform;
            return true;
        }
        return false;
    }

    public IEnumerator FindPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            found = false;
            var direction = player.transform.position - transform.position;
            Debug.DrawRay(transform.position + Vector3.up, direction, Color.green);
            if (direction.magnitude > 15)
            {
                continue;
            }
            Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, direction.magnitude);
            {
                if (raycastHit.transform == null || raycastHit.transform == player.transform)
                {
                    Debug.Log("hit");
                    found = true;
                }
            }
        }
    }
}
