using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public float rotationSpeed;
    public float obstacleRadius;

    private Vector3 targetPosition;
    private Quaternion rotationToTarget;

    private new Camera camera;
    private CharacterController characterController;

	void Start()
    {
        targetPosition = transform.position;

        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
	}
	
	void Update()
    {
		if (Input.GetMouseButton(0))
        {
            targetPosition = GetTargetPosition();
        }

        if (Vector3.Distance(transform.position, targetPosition) > obstacleRadius)
        {
            Move();
        }
	}

    private void Move()
    {
        transform.rotation = GetRotationToTarget();

        characterController.Move(transform.forward * movementSpeed * Time.deltaTime);
    }

    private Vector3 GetTargetPosition()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return transform.position;
    }

    private Quaternion GetRotationToTarget()
    {
        Quaternion newRotation = Quaternion.LookRotation(targetPosition - transform.position);
        newRotation.x = 0;
        newRotation.z = 0;

        newRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

        return newRotation;
    }
}
