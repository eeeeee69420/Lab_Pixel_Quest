using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;
    public EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();
    }
    // Update is called once per frame
    public void PlayAnimation(string Animation)
    {
        animator.Play(Animation);
    }
}
