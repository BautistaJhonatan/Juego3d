using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform itemSpawn;
    Inventory inventory;
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;


    public CharacterController player;
    public float playerSpeed;
    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    public bool canJumpF=true;
    public Animator bodyAnim;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
        player = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.ToggleInventory();
        }
        MovePlayer();
    }
    void MovePlayer() 
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove,0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        
        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position+movePlayer);

        SetGravity();

        PlayerSkills();
        bodyAnim.SetFloat("Speed", Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove));

        player.Move(movePlayer *Time.deltaTime);

    }
    //habilidades del player
    void PlayerSkills()
    {


        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            bodyAnim.SetTrigger("CanJump");


        }
        



    }
    //gravedad del player
    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;

        }
        bodyAnim.SetBool("isGrounded",player.isGrounded);
    }

    //direccion de moviento 
    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight =camRight.normalized;
    }


    private void OnCollisionEnter(Collision collision)
    {
        canJumpF = true;
    }
    //hacer eque reciba daño
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Punch"))
        {

        }
    }
}
