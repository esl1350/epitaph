using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PlayerController : Controller
{
    [SerializeField] private float chainAttackTime = 0.1f;
    [SerializeField] private List<GameObject> meleeHitboxes;
    [SerializeField] private float attackDelay; // how long attacks actually last
    [SerializeField] private float attackShiftMultiplier;
    private PlayerInput playerInput;
    private Camera mainCam;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private int currentAttack;
    private bool hasBufferAttack;
    private bool canChainAttack;
    private bool isAttacking;
    private Vector2 lastMovementInput;
    public Vector2 LastMovementInput {
        get {
            return lastMovementInput;
        }
    }

    // audio
    private EventInstance playerFootsteps;

    void Start()
    {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
        base.Start();
        canMove = true;
        canChangeDirection = true;
        playerInput = GetComponent<PlayerInput>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        movementInput = Vector2.zero;
        currentAttack = 0;
        hasBufferAttack = false;
        canChainAttack = false;
        isAttacking = false;
        entity = GetComponent<Player>();
        stats = entity.EntityStats;
    }

    public void resetPos()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vel = Vector3.Normalize(rb.velocity);
        if (!isAttacking) {
            animator.SetFloat("vel x", vel.x);
            animator.SetFloat("vel y", vel.y);
        }


        if (rb.velocity.magnitude < 0.1 && !isAttacking) {
            animator.SetBool("is stopped", true);
        } else {
            animator.SetBool("is stopped", false);
        }

        if(!canMove) {
            UpdateSound();
            rb.velocity = Vector3.zero;
            return;
        }

        if (isAttacking) {
            UpdateSound();
            return;
        }

        if (canChangeDirection) {
            rb.velocity = movementInput * stats.GetStatValue(StatEnum.WALKSPEED);
        } else {
            rb.velocity = lastMovementInput * stats.GetStatValue(StatEnum.WALKSPEED);
        }
        UpdateSound();
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        movementInput = ctx.ReadValue<Vector2>();
        if (canChangeDirection){
            lastMovementInput = movementInput;
        }
    }

    public void OnMelee(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            if (!isAttacking) {
                StartCoroutine(AttackDelay());
            } else if (!hasBufferAttack) {
                StartCoroutine(BufferAttack());
            }
        }
    }
    IEnumerator AttackDelay() {
        if (canChainAttack) {
            if (currentAttack >= meleeHitboxes.Count - 1) {
            currentAttack = 0;
            } else {
                currentAttack++;
            }
        } else {
            currentAttack = 0;
        }
        isAttacking = true;
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 mousePosNorm = (new Vector2(mousePos.x, mousePos.y)).normalized;
        rb.velocity = mousePosNorm * attackShiftMultiplier;
        MeleeAttack meleeAttack = meleeHitboxes[currentAttack].GetComponent<MeleeAttack>();
        int atkTag = currentAttack + 1;
        animator.SetTrigger("is melee " + atkTag);
        meleeAttack.SetActive();
        meleeAttack.Attack(mousePos);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.miniKnifeSwing, transform.position);
        // Don't remove code below. 
        // if (currentAttack < 2)
        // {
        //     AudioManager.instance.PlayOneShot(FMODEvents.instance.miniKnifeSwing, transform.position);
        // }
        // else
        // {
        //     AudioManager.instance.PlayOneShot(FMODEvents.instance.knifeSwing, transform.position);
        // }
        yield return new WaitForSeconds(attackDelay);
        meleeAttack.SetInactive();
        rb.velocity = Vector2.zero;
        isAttacking = false;
        StartCoroutine(AttackChainTimeWindow());
    }
    IEnumerator BufferAttack() {
        hasBufferAttack = true;
        yield return new WaitUntil(() => isAttacking == false);
        isAttacking = true;
        StartCoroutine(AttackDelay());
        hasBufferAttack = false;
    }
    IEnumerator AttackChainTimeWindow() {
        canChainAttack = true;
        yield return new WaitForSeconds(chainAttackTime);
        canChainAttack = false;
    }

    private void UpdateSound()
    {
        // start footsteps event if the player has an x or y velocity
        // if ((rb.velocity.x != 0 || rb.velocity.y != 0) && !isAttacking && rb.velocity.x <= 10 && rb.velocity.y <= 10 && rb.velocity.x >= -10 && rb.velocity.y >= -10)
        if ((rb.velocity.x != 0 || rb.velocity.y != 0) && !isAttacking && canMove)
        {
            // get the playback state
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        } else 
        // otherwise, stop the footsteps event else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}