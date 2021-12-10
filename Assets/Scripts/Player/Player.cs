using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _character;
    //[Header ("Player Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    private float _yVelocity;
    [SerializeField] private float _gravity;
    private Camera _camera;
    //[Header ("Camera Settings")] 
    [SerializeField] private float _cameraSensitivity;
    // Start is called before the first frame update
    
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _character = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        CameraController();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }       
    }

    private void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X")*_cameraSensitivity *Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _cameraSensitivity *Time.deltaTime;

        

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;      

        transform.localRotation = Quaternion.AngleAxis(currentRotation.y , Vector3.up );

        Vector3 currentCameraRotation =_camera.transform.localEulerAngles;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 13, 21)- mouseY;
        _camera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }
   // 
    void CalculateMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput) * _speed;
        //may need to add velcity _veleocity = direction *_speed;

        direction = transform.TransformDirection(direction);
        if (Input.GetKeyDown(KeyCode.Space) && _character.isGrounded == true)
        {
            _yVelocity = _jumpHeight;
        }

        _yVelocity -= _gravity;
        
        direction.y = _yVelocity;
        
        _character.Move(direction * Time.deltaTime);
    }
}