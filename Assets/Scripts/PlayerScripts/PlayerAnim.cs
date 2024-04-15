using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private PlayerMovement move;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            anim.SetBool("isWalking", true);
        }
        else{
            anim.SetBool("isWalking", false);
        }
        if(Input.GetKey(KeyCode.Space) && move.getDashState()){
            anim.SetTrigger("Dash");
        }
    }
}
