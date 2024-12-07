using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    // Rigidbody
    private Rigidbody2D rigidbody2D; // Controls the speed 
    public float JumpForce = 4; // Controls how high the player jumps 

    // Checks
    public Transform ground;     // Where the collision will be checked for 
    public LayerMask groundMask; // The layer we're looking for 
    private bool waterCheck;     // Is the player touching water 

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player is touching any object who's layermask is called Ground
        bool groundCheck = Physics2D.OverlapCapsule(ground.position, new Vector2(1, 0.08f), CapsuleDirection2D.Horizontal, 0, groundMask);


        // Checks if the player can jump
        // Checks if the player is click the space key, checks if the player is touching ground or water
        if (Input.GetKeyDown(KeyCode.Space) && (groundCheck || waterCheck ) && playerStats.playerLife > 0 && playerStats.finish == false)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpForce);
        }
    }

    // Checks if the player has touched water, if so let them jump 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            waterCheck = true;
        }
    }

    // Checks if the player has left water, if so they can't jump 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            waterCheck = false;
        }
    }

}
