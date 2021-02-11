using System.Collections;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    /*private float speed = 20.0f;
    private float panBorderThickness = 20f;
    private Vector2 panLimit;
    private float scrollSpeed = 20;
    private float minY = 20f;
    private float maxY = 1500f;
    private float zoomFactor = 30f;
    private float targetZoom;
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomLerpSpeed = 10f;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    */
    /* void Start()
     {
         cam = Camera.main;
         targetZoom = cam.orthographicSize;
         //   transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
     }*/
    private Vector3 previousPosition;
    
    Camera cam;
    int FOVpos = 0;
    [Header("Camera FOV Range")]
    private float[] FOVS = { 30, 35, 40, 45, 50, 55 };
    public float speed = 3f;
    private float speedUp = 150f;
    public bool zoomIn, zoomOut;
    void Start()
    {
        cam = GetComponent<Camera>();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            ZoomIn();
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            ZoomOut();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            RotateLeft();
        }
        if (Input.GetKey(KeyCode.E))
        {
            RotateRight();

        }

        if (Input.GetKey(KeyCode.Z))
        {
            RotateUp();
        }
        if (Input.GetKey(KeyCode.C))
        {
            RotateDown();

        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w") )
        
        {
            pos.z += speedUp * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            pos.z -= speedUp * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            pos.x += speedUp * Time.deltaTime;

        }

        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            pos.x -= speedUp * Time.deltaTime;

        }


        if (zoomIn) 
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FOVS[FOVpos], speed * Time.deltaTime);
        }
        if (zoomOut)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FOVS[FOVpos], speed * Time.deltaTime);
        }
        transform.position = pos;
    }


    void ZoomIn()
    {
        if (FOVpos >= FOVS.Length - 1)
        {
            FOVpos = FOVS.Length - 1;
        }
        else {
            FOVpos += 1;
        
        }
        zoomIn = true;
    
    }

    void ZoomOut()
    {
        if (FOVpos <=0)
        {
            FOVpos = 0;
        }
        else
        {
            FOVpos -= 1;

        }
        zoomOut = true;

    }

    void RotateLeft() 
    {
        cam.transform.Rotate(0, -0.06f, 0);

    }

    void RotateRight()
    {
        cam.transform.Rotate(0, 0.06f, 0);

    }
    void RotateUp()
    {
        cam.transform.Rotate(-0.06f, 0, 0);

    }

    void RotateDown()
    {
        cam.transform.Rotate(0.06f,0, 0);

    }

}
/*
void Update()
{
    var targetPos = transform.position;
    if (mouseEnabled)
    {
        // Move camera by mouse pos. unless dragging.
        if (Input.mousePosition.x > screenwidth - (percentageWidth * boundaryInPercentage))
        {
            targetPos.x += movementSpeed * Time.deltaTime; // +X
        }
        if (Input.mousePosition.x < 0 + (percentageWidth * boundaryInPercentage))
        {
            targetPos.x -= movementSpeed * Time.deltaTime; // -X
        }
        if (Input.mousePosition.y > screenheight - (percentageHeight * boundaryInPercentage))
        {
            targetPos.z += movementSpeed * Time.deltaTime; // +Z
        }
        if (Input.mousePosition.y < 0 + (percentageHeight * boundaryInPercentage))
        {
            targetPos.z -= movementSpeed * Time.deltaTime; // -Z
        }
    }
    // WASD / Arrow movement
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    {
        targetPos.x += movementSpeed * Time.deltaTime; // +X
    }
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {
        targetPos.x -= movementSpeed * Time.deltaTime; // -X
    }
    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
        targetPos.z += movementSpeed * Time.deltaTime; // +Z
    }
    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {
        targetPos.z -= movementSpeed * Time.deltaTime; // -Z
    }
    // Zoomin' part
    if (zoomAmount > -100)
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        { // zoom out
            targetPos.y += camMovementSpeed * Time.deltaTime;
            targetPos.z -= (camMovementSpeed * 2) * Time.deltaTime;
            zoomAmount--;
        }
    }
    if (zoomAmount < 100)
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        { // zoom in
            targetPos.y -= camMovementSpeed * Time.deltaTime;
            targetPos.z += (camMovementSpeed * 2) * Time.deltaTime;
            zoomAmount++;
        }
    }
}
*/

// Update is called once per frame
/*void Update()
{
    Vector3 pos = transform.position;
//        Vector3 rotation = transform.eulerAngles;


    //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

  /*  if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
    //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
    {
        pos.z += speed * Time.deltaTime;

    }

    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
    //if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
    {
        pos.z -= speed * Time.deltaTime;

    }

    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
    //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
    {
        pos.x += speed * Time.deltaTime;

    }

    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
    //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
    {
        pos.x -= speed * Time.deltaTime;

    }

    */


//float scrollData = Input.GetAxis("Mouse ScrollWheel");
//Debug.Log(scroll);
// targetZoom -= scrollData* zoomFactor;

//cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime* zoomLerpSpeed);
//Debug.Log(targetZoom);
//pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;*

//pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
//pos.y = Mathf.Clamp(pos.y, minY, maxY);
//pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

//transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
/*if (Input.GetKey("q"))
{
    //yaw += rotationSpeed * 10f * Time.deltaTime;
    pitch += rotationSpeed * 10f * Time.deltaTime;
}

if (Input.GetKey("e"))
{
    //yaw -= rotationSpeed * 10f * Time.deltaTime;
    pitch -= rotationSpeed * 10f * Time.deltaTime;
}
*/
//pitch -= rotationSpeed * Input.GetAxis("Mouse Y");

//float scroll = Input.GetAxis("Mouse ScrollWheel");
//pos.y -= scroll * scrollSpeed * 100f* Time.deltaTime;
//pos.y = Mathf.Clamp(pos.y, minY, maxY);
//pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
//pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
//transform.position = pos;
// transform.eulerAngles = new Vector3 (pitch, yaw, 0.0f);

