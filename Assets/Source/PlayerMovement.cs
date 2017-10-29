using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public float rotationSpeed;
    public float obstacleRadius;

    private Vector3 targetPosition;
    private Quaternion rotationToTarget;

    private new Camera camera;
    private CharacterController characterController;
    private NavMeshAgent navMeshAgent;

	void Start()
    {
        targetPosition = transform.position;

        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
            MoveTo(targetPosition);
        }
	}

    private void MoveTo(Vector3 position)
    {
        navMeshAgent.destination = position;
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
}
