using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bot : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Rigidbody[] rigidbodies;
    public Manager manager;
    int left_jab = Animator.StringToHash("Base Layer.punch_left_jab");
    int right_jab = Animator.StringToHash("Base Layer.punch_right_jab");
    int left_uppercut = Animator.StringToHash("Base Layer.left_uppercut");
    int right_uppercut = Animator.StringToHash("Base Layer.right_uppercut");
    int walk_backward = Animator.StringToHash("Base Layer.walk_backward");
    int idle = Animator.StringToHash("Base Layer.idle");
    int[] actions;
    void Start()
    {
        actions = new int[] { left_jab, right_jab, left_uppercut, right_uppercut};
        anim = GetComponentInChildren<Animator>();
        Collider[] colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach(Collider collider in colliders)
        {
            collider.isTrigger = true;
        }
        anim.enabled = true;
        anim.SetBool("Ready", true);
        
    }

    // Update is called once per frame
    int counter = 0;
    bool gameOver = false;
    void Update()
    {
        if (!gameOver)
        {
            if (tag == "Player")
            {
                //Fetch Data from Server
            }
            else
            {
                counter = (counter + 1) % 5;
                if (counter == 0)
                    if (manager.isAlive["Player"])
                        anim.Play(actions[UnityEngine.Random.Range(0, actions.Length)]);
                    else
                    {
                        anim.Play(walk_backward);
                        transform.Translate((Vector3.right + Vector3.back) * Time.deltaTime * 2);

                        gameOver = true;
                    }
            }
        }
    }
}
