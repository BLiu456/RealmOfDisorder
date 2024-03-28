using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveDir;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing;
    bool canDash = true;

    private void Start()
    {
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        moveDir = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDir.x * dashSpeed, moveDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public bool getDashState()
    {
        return isDashing;
    }

    public void setSpeed(float v)
    {
        moveSpeed = v;
    }
}
