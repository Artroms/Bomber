using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent<Vector2> onPlayerFound;
    private static GameObject player;
    private bool found;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = FindObjectOfType<BombPaster>(true).gameObject;
        StartCoroutine(CheckNear());
    }

    // Update is called once per frame
    void Update()
    {
        if (found)
        {
            var direction = player.transform.position - transform.position;
            var vec2Dir = new Vector2(direction.x, direction.z);
            vec2Dir = vec2Dir.magnitude > 1 ? vec2Dir.normalized : vec2Dir;
            onPlayerFound.Invoke(vec2Dir);
        }
        else
        {
            onPlayerFound.Invoke(Vector2.zero);
        }
    }

    public IEnumerator CheckNear()
    {
        while(true)
        {
            yield return null;

            found = false;
            var direction = player.transform.position - transform.position;
            Debug.DrawRay(transform.position + Vector3.up, direction, Color.green);
            if (direction.magnitude > 15)
            {
                continue;
            }
            if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, direction.magnitude))
            {
                if (raycastHit.transform == player.transform)
                {
                    Debug.Log("hit");
                    found = true;
                }
            }
        }
    }
}
