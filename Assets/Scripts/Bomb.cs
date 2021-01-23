using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int damage = 1;
    public float timer = 1;

    void Start()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        int mask = ~(1 << LayerMask.NameToLayer("Water")); // маска, для рейкаста, игнорирует воду
        if (Physics.BoxCast(transform.position, Vector3.one / 2.1f, transform.forward, out RaycastHit forward, Quaternion.identity, 5, mask, QueryTriggerInteraction.Ignore))
        {
            TryDamage(forward);
        }
        if (Physics.BoxCast(transform.position, Vector3.one / 2.1f, -transform.forward, out RaycastHit back, Quaternion.identity, 5, mask, QueryTriggerInteraction.Ignore))
        {
            TryDamage(back);
        }
        if (Physics.BoxCast(transform.position, Vector3.one / 2.1f, transform.right, out RaycastHit right, Quaternion.identity, 5, mask, QueryTriggerInteraction.Ignore))
        {
            TryDamage(right);
        }
        if (Physics.BoxCast(transform.position, Vector3.one/2.1f, -transform.right, out RaycastHit left, Quaternion.identity, 5, mask, QueryTriggerInteraction.Ignore))
        {
            TryDamage(left);
        }
        Destroy(gameObject);
    }

    private void TryDamage(RaycastHit raycastHit)
    {
        var health = raycastHit.collider.gameObject.GetComponent<Health>();
        if (health != null)
            health.HP -= damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
        Gizmos.DrawRay(transform.position, -transform.forward * 5);
        Gizmos.DrawRay(transform.position, transform.right * 5);
        Gizmos.DrawRay(transform.position, -transform.right * 5);
    }
}
