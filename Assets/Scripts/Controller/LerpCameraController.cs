using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LerpCameraController : MonoBehaviour
{
	public GameObject player;
	[SerializeField, Range(1,20)]
	private float moveSpeed = 1;
	[SerializeField, Range(5, 20)]
	private float distance = 5;
	[SerializeField, Range(2, 20)]
	private float height = 2;


    private void LateUpdate()
    {
		MoveCamera();
	}

	private void MoveCamera()
    {
		var target = player.transform.position + new Vector3(0, height, -distance);
		transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
	}

    private void OnValidate()
    {
		if(player != null)
	        transform.position = player.transform.position + new Vector3(0, height, -distance);
	}

}
