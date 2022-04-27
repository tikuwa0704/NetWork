using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MonsterScript : MonoBehaviourPunCallbacks
{
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey("up"))
            {
                transform.position += transform.forward * 5.0f * Time.deltaTime;
                animator.SetBool("walk", true);
            }
            else
            {
                animator.SetBool("walk", false);
            }
            if (Input.GetKey("right"))
            {
                transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
            }
            if (Input.GetKey("left"))
            {
                transform.Rotate(new Vector3(0, -30, 0) * Time.deltaTime);
            }
        }
    }
}

