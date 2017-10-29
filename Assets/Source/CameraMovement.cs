using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField]
    private Transform target;

    private float currentZoom;
    private float offsetToTargetZ;

    [SerializeField]
    private int MIN_ZOOM;
    [SerializeField]
    private int MAX_ZOOM;

    private const int BOUNDARY_SIZE = 30;

    [SerializeField]
    private int cameraSpeed;
    
    private bool isFocused = false;
    private Vector3 cameraMovement;

	void Start () {
        cameraMovement = Vector3.zero;
        offsetToTargetZ = Mathf.Sin(Mathf.Deg2Rad * (90 - transform.rotation.eulerAngles.x)) * transform.position.y
                  / Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.x);
    }
	
	void Update () {
        CameraManagementInput();
        Zoom();
        Move();
	}

    private void CameraManagementInput()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Focus | follow
        {
            isFocused = !isFocused;
        }
        
        // ...


        if (isFocused)
        {
            return;
        }
        

        if (Input.mousePosition.x > Screen.width - BOUNDARY_SIZE || Input.GetKey(KeyCode.RightArrow))
        {
            cameraMovement += Vector3.right;
        }
        if (Input.mousePosition.x < BOUNDARY_SIZE || Input.GetKey(KeyCode.LeftArrow))
        {
            cameraMovement += Vector3.left;
        }
        if (Input.mousePosition.y > Screen.height - BOUNDARY_SIZE || Input.GetKey(KeyCode.UpArrow))
        {
            cameraMovement += Vector3.forward;
        }
        if (Input.mousePosition.y < BOUNDARY_SIZE || Input.GetKey(KeyCode.DownArrow))
        {
            cameraMovement += Vector3.back;
        }

        cameraMovement.Normalize();
    }

    private void Zoom()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            currentZoom += scrollDelta;

            if (MIN_ZOOM > currentZoom || currentZoom > MAX_ZOOM)
            {
                currentZoom = Mathf.Clamp(currentZoom, MIN_ZOOM, MAX_ZOOM);
                return;
            }

            transform.Translate(0, 0, scrollDelta);
            offsetToTargetZ = Mathf.Sin(Mathf.Deg2Rad * (90 - transform.rotation.eulerAngles.x)) * transform.position.y
                      / Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.x);
        }
    }

    private void Move()
    {
        if (isFocused && target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - offsetToTargetZ);
        }
        else if (cameraMovement != Vector3.zero)
        {
            transform.position += cameraMovement * cameraSpeed;
            cameraMovement = Vector3.zero;
        }
    }
}
