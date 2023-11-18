using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Animator animator;
    PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMove>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            animator.SetBool("Walking", true);
            player.speed = 24;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Running", true);
            player.speed = 35;
        }
        if (!Input.GetKey("w"))
        {
            animator.SetBool("Walking", false);
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Running", false);
        }
    }
}
