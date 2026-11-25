using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    [SerializeField] InputActionReference moveAction;
    public CharacterController charcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed;

    public Transform model;
    public float turnspeed;
    private float  yspeed;
    public float gravityScale;

    public Animator anim;


    public InputActionReference walkAction;
    public float walkspeed;

    private float activespeed;

    public InputActionReference rollAction;
    public float rollspeed, rolllength;


    private float rollcounter;

    public InputActionReference aimAction;
    








    void Start()
    {
        activespeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (walkAction.action.IsPressed())
        {
            activespeed = walkspeed;
        }
        else
        {
            activespeed = moveSpeed;
        }

        if (charcon.isGrounded)
        {
            yspeed = 0f;
        }


        yspeed += Physics.gravity.y * gravityScale * Time.deltaTime;



        if (aimAction.action.IsPressed())
        {
            activespeed = 0f;

            rollspeed = 0f;
            rolllength = 0f;

        }

        if(rollAction.action.WasPressedThisFrame() && !aimAction.action.IsPressed())
        {  

            if(rollcounter<=0f)
            {
                anim.SetTrigger("roll");

                rollcounter = rolllength;
            }
           
        }

        if (rollcounter > 0f)
        {
            rollcounter -= Time.deltaTime;
            activespeed = rollspeed;
        }

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        //transform.position += new Vector3(0f, 0f, moveSpeed * Time.deltaTime);

        // transform.position += new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;



        charcon.Move(new Vector3(moveInput.x, yspeed, moveInput.y) * activespeed * Time.deltaTime);


        if (moveInput != Vector2.zero && !aimAction.action.IsPressed())
        {
            Quaternion LookDir = Quaternion.LookRotation(new Vector3(moveInput.x, 0f, moveInput.y));

            model.rotation = Quaternion.Slerp(model.rotation, LookDir, turnspeed * Time.deltaTime);
        }

       

        anim.SetFloat("speed", moveInput.magnitude * activespeed);

    }
}
