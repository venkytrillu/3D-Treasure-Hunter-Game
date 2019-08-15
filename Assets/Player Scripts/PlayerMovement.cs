using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController character_Controller;
    private HealthScript healthScript;
    public Vector3 move_Direction;
    public float speed = 5f;
    public float gravity = 20f;
    public float setGravity;
    public float jump_Force = 10f;
    public float vertical_Velocity;
    bool isMoving;
    float yPos;
    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();
        healthScript = GetComponent<HealthScript>();
    }

    void Update()
    {
 
            if(!healthScript.isDead)
            {
                Health(Time.deltaTime);
                MoveThePlayer();
            }
    }

    void Health(float damage)
    {
        healthScript.ApplayDamage(damage);
    }

    void MoveThePlayer()
    {
      move_Direction = new Vector3(Input.GetAxis(Axis.Horizontal), 0f,
                                   Input.GetAxis(Axis.Vertical));

       
        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();
        
        character_Controller.Move(move_Direction);

        if (character_Controller.isGrounded)
        {
            isMoving = true;
        }
        else
        {

            isMoving = false;
        }
        if (isMoving)
        {
            if (character_Controller.isGrounded && move_Direction.z != 0)
            {
                gravity = setGravity;
            }
        }
        if (!character_Controller.isGrounded)
        {
            if (gravity< 0.3)
                gravity += Time.deltaTime;
        }
    
    } // move player

    void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime;
        PlayerJump();
        move_Direction.y = vertical_Velocity * Time.deltaTime;


    } // apply gravity

    void PlayerJump()
    {

        if(character_Controller.isGrounded&&Input.GetKeyDown(KeyCode.Space))
        {
            yPos = transform.localPosition.y;
            vertical_Velocity = jump_Force;

        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == Tags.Oxygen)
        {
            Controller.instance.SetOxygenCount(1);
            Destroy(collision.transform.gameObject);
            Controller.instance.PlaceObject(1);
        }

        if (collision.transform.tag == Tags.Crystal)
        {
            Controller.instance.SetScore(10);
            Destroy(collision.transform.gameObject);
            Controller.instance.PlaceObject(0);
        }

    }



} // class


































