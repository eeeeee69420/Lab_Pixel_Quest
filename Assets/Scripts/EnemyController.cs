using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class EnemyController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D enemyBody;
    public SpriteRenderer enemySprite;
    public EnemyAnimator enemyAnimator;
    public GameController controller;
    public float targetDistance = 0;
    public int playerTarget;
    public Vector2 direction;
    public Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        enemyAnimator = GetComponent<EnemyAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < controller.Players.Count; i++)
        {
            float dist = Vector2.Distance(enemyBody.position, controller.Players[i].GetComponent<PlayerController>().playerBody.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                playerTarget = i;
            }
        }
        targetPosition = controller.Players[playerTarget].GetComponent<PlayerController>().playerBody.position;
        direction = (targetPosition - enemyBody.position).normalized;
        if (direction.x < 0)
            enemySprite.flipX = true;
        else if (direction.x > 0)
            enemySprite.flipX = false;
            enemyBody.MovePosition(enemyBody.position + speed * Time.deltaTime * direction);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            enemyAnimator.PlayAnimation("Attack");
        }
    }
}
