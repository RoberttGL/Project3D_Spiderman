using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] Transform PlayerObj;
    bool isJump = false;
    Vector3 movement;
    Animator animator;
    public bool grounded = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        changeGround();
        if (movement != Vector3.zero)
        {
            animator.SetBool("Movement", true);
        }
        else
        {
            animator.SetBool("Movement", false);
        }

        if (rigidBody.velocity.y < 0 && !grounded)
        {
            animator.SetBool("Fall", true);
        }
        else
        {
            animator.SetBool("Fall", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !isJump)
        {
            isJump = true;
            rigidBody.AddForce(Vector3.up * 5, ForceMode.Impulse);
            animator.Play("Jump");
        }

    }



    private void FixedUpdate()
    {


        movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement += -transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += -transform.right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right;
        }

        movement = movement.normalized * playerStats.speed;
        print(movement);
        if (grounded)
        {
            rigidBody.velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
        }
        else
        {
            rigidBody.transform.Translate(movement.normalized * 0.05f,Space.World);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
    }

    void changeGround()
    {
        grounded = Physics.Raycast(PlayerObj.position, Vector3.down, 2f);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.3f);
    }


}
