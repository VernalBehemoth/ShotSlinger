using UnityEngine;
 public class PlayerController : MonoBehaviour
 {
     public CharacterController characterController;
 
     [Header("Camera")]
     public Camera cam;
     public float XSensitivity, YSensitivity;
     public bool invertPitch, clamp;
     public int minY, maxY;
 
     [Header("Movement")]
     public bool grounded, jumping;
     public float walkSpeed, sprintSpeed, jumpPower, gravity;
 
     [HideInInspector] public float jumpSpeed, verticalRotation, movementSpeed;
 
     private void Start()
     {
         movementSpeed = walkSpeed;
     }
 
     void Update()
     {
         float mouseX = Input.GetAxis("Mouse X"), mouseY = Input.GetAxis("Mouse Y");
 
         if (!invertPitch)
             mouseY = -mouseY;
         verticalRotation += mouseY * YSensitivity;
         if (clamp)
             verticalRotation = Mathf.Clamp(verticalRotation, minY, maxY);
 
         transform.Rotate(0, mouseX * XSensitivity, 0);
         //cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
 
        //  if (Input.GetMouseButtonDown(0))
        //      Cursor.lockState = CursorLockMode.Locked;
 
         float horizontal = Input.GetAxis("Horizontal"); //forward = Input.GetAxis("Vertical");
 
         grounded = characterController.isGrounded;
 
         if (grounded)
         {
             jumping = false;
             jumpSpeed = 0;
         }
         else
             jumpSpeed -= (gravity * 25) * Time.deltaTime;
 
         if (Input.GetAxisRaw("Jump") != 0 && !jumping)
         {
             jumping = true;
             jumpSpeed = jumpPower;
         }
 
         if (Input.GetKey(KeyCode.LeftShift))
         {
             movementSpeed = sprintSpeed;
         }
         else if (Input.GetKeyUp(KeyCode.LeftShift))
         {
             movementSpeed = walkSpeed;
         }
 
         Vector3 motion = new Vector3(horizontal * movementSpeed, jumpSpeed, 0 * movementSpeed);
         characterController.Move((transform.rotation * motion) * Time.deltaTime);
     }
 }