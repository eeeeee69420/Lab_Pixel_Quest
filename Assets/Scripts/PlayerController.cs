using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float speed;
    public float velocityH;
    public float velocityV;
    public Rigidbody2D playerBody;
    public SpriteRenderer playerSprite;
    public PlayerAnimator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 inputDirection = new Vector2(inputX, inputY);
        if (inputDirection.magnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }
        if (inputDirection.x < 0)
        {
            playerSprite.flipX = true;
        }
        else if (inputDirection.x > 0)
        {
            playerSprite.flipX = false;
        }
        playerBody.position += speed * Time.deltaTime * inputDirection;
    }
}
