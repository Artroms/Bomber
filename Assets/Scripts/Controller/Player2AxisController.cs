using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2AxisController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    [SerializeField, Range(1, 40)]
    private float speed;
    [SerializeField, Range(1, 40)]
    private float acceleration;
    private Vector2 oldDirection;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 direction)
    {
        oldDirection = Vector2.Lerp(oldDirection, direction, acceleration * Time.deltaTime);
        controller.Move(direction.magnitude * transform.forward * speed * Time.deltaTime);
        animator.SetFloat("InputMagnitude", oldDirection.magnitude * 1.5f);
    }

    public void Rotate(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;
        var directionVec3 = new Vector3(direction.x, 0, direction.y);
        transform.rotation = Quaternion.LookRotation(directionVec3);
    }

}
