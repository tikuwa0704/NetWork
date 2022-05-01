using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MonsterScript : MonoBehaviourPunCallbacks
{
    private Animator animator;

    Quaternion targetRotation;
    
    [SerializeField] float move_speed;

    // Use this for initialization
    void Start()
    {
        TryGetComponent(out animator);
        
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            MoveController();
            /*
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                animator.SetTrigger("forwards");
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
                animator.SetTrigger("forwards");
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * speed * Time.deltaTime;
                animator.SetTrigger("right");
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
                animator.SetTrigger("left");
            }
            */
        }
    }


    void MoveController()
    {
        //入力ベクトルの取得
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y,Vector3.up);
        var velocity = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;
        
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationspeed = 600 * Time.deltaTime;


        //移動方向を向く
        if (velocity.magnitude >= 0.5f)
        {
           targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationspeed);

        //移動速度をanimatorに反映
        animator.SetFloat("speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);

        if(velocity.magnitude>0)transform.position += velocity * move_speed * speed * Time.deltaTime;
        
    }
}

