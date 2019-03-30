using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Rigidbody[] rigidbodies;
    int left_jab = Animator.StringToHash("Base Layer.punch_left_jab");
    int right_jab = Animator.StringToHash("Base Layer.punch_right_jab");
    int left_uppercut = Animator.StringToHash("Base Layer.left_uppercut");
    int right_uppercut = Animator.StringToHash("Base Layer.right_uppercut");
    int[] actions;
    void Start()
    {
        actions = new int[] { left_jab, right_jab, left_uppercut, right_uppercut};
        anim = GetComponentInChildren<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        anim.enabled = true;
        anim.SetBool("Ready", true);
        
    }

    // Update is called once per frame
    int counter = 0;
    void Update()
    {
        if(tag == "Player")
        {
            //Fetch Data from Server
        }
        else
        {
            counter = (counter+1) % 10;
            if(counter == 0)
            anim.Play(actions[Random.Range(0,actions.Length)]);
        }
    }
}
