using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement : Atributes")]
    CharacterController controller;
    Vector3 moveDirection;
    public float moveSpeed;

    [Header("Rotation : Atributes")]
    public GameObject mainCamera;
    public float cameraRotationSpeed;
    float cameraRotX;

    [Header("Character : Atributes")]
    public GameObject character;
    Quaternion rotation;
    float angle;
    Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = character.GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        PlayerMovement();
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        //Here i calculate an angle according to the "Horizontal" and "Vertical" Inputs
        angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg; //Here i translate form radians to angles
        rotation = Quaternion.Euler(0, angle, 0);//I'll create a Quaternion rotate
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //I'll just aplicate the rotation if the inputs is pressed
        {
            //(this is very important) i'll aplicate a rotation in 'localRotation' from the mesh, bacause this'll respect a rotation from the character controller
            character.transform.localRotation = Quaternion.Slerp(character.transform.localRotation, rotation, 15 * Time.deltaTime);
        }
    }

    private void PlayerMovement()
    {
        //Movement by player
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //I'll define a movement with the "Horizontal" and "Vertical" Inputs
        moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        //Rotation by player
        PlayerRotation();

        //Animations
        Animations();
    }

    private void Animations()
    {
        anim.SetFloat("pMove", moveDirection.magnitude);
    }

    private void PlayerRotation()
    {
        cameraRotX += Input.GetAxis("Mouse X") * cameraRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, cameraRotX, 0f);
    }
}
