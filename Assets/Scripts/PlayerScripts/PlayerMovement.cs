using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDir;
    public float activeMoveSpeed;

    public float dashSpeed = 2f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;

    public Image StaminaBar;
    public TextMeshProUGUI StaminaTxt;
    public float Stamina, MaxStamina;
    public float DashCost;
    public float ChargeRate;
    private Coroutine recharge;
    public AudioSource audioDash;

    void Start()
    {
        StaminaTxt.text = string.Format("{0}/{1}", Stamina, MaxStamina);
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            if (Stamina >= DashCost)
            {
                audioDash.Play();
                StartCoroutine(Dash());
                Stamina -= DashCost;
                if (Stamina < 0) Stamina = 0;
                StaminaTxt.text = string.Format("{0:0}/{1}", Stamina, MaxStamina);
                StaminaBar.fillAmount = Stamina / MaxStamina;
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
            }
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
        rb.velocity = new Vector2(moveDir.x * dashSpeed * moveSpeed, moveDir.y * dashSpeed * moveSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1.5f);
        while (Stamina < MaxStamina)
        {
            Stamina += ChargeRate / 10f;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaTxt.text = string.Format("{0:0}/{1}", Stamina, MaxStamina);
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }

    public bool getDashState()
    {
        return isDashing;
    }

    public void setSpeed(float v)
    {
        moveSpeed = v;
    }

    public void setMaxStamina(float s)
    {
        MaxStamina = s;
        StaminaTxt.text = string.Format("{0:0}/{1}", Stamina, MaxStamina);
        StaminaBar.fillAmount = Stamina / MaxStamina;
    }
}