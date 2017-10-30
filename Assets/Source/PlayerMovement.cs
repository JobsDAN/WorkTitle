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
    public delegate void Action();

	void Start()
    {
        targetPosition = transform.position;

        characterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        camera = Camera.main;
	}
	
    const float EPS = 1E-5f;
	void Update()
    {
        if (currentAction != null)
        {
            navMeshAgent.isStopped = true;
			Quaternion rot = Quaternion.LookRotation(seeDest - transform.position);
            rot.x = 0;
            rot.z = 0;
			float diff = Quaternion.Angle(transform.rotation, rot);
            float dist = Vector3.Distance(seeDest, transform.position);
            while (dist > 2 && diff > 15)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
                return;
            }

            transform.rotation = rot;
            currentAction();
            currentAction = null;
            navMeshAgent.isStopped = false;
            return;
        }

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

    public Vector3 GetTargetPosition()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return transform.position;
    }

    private Action currentAction;
    private Vector3 seeDest;
    private Quaternion saveRotation;
    public void SeeAndDo(Vector3 point, Action action)
    {
        currentAction = action;
        seeDest = point;
    }
}
