using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    [Header("Visual")]
    public Animator playerAnimator;
    [Header("Control")]
    public Button chopButton;

    [Header("Stats")]
    public int attackDamage;
    public float attackRange;
    public float hitHorizontalNockBack = 2000000;
    public float hitVerticalNockBack = 1000000;
    public float misshorizontalNockBack = 2000000;
    public float missVerticalNockBack = 1000000;
    public Transform attackPos;
    [Header("World")]
    public LayerMask cuttableMask;
    public LayerMask metalMask;

    [Header("Private")]
    private Movement movement;
    private PlayerState state;

    private void Start() {
        chopButton.onClick.AddListener(attack);
        movement = FindObjectOfType<Movement>();
        state = FindObjectOfType<PlayerState>();
    }

    private void Update() {
        //REMOVE
        if (Input.GetButtonDown("Jump")) attack();
    }
    private void FixedUpdate() {
        //When Animation done
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_chop") && movement.isAttacking()) {
            cancleAttack();
        }
    }

    void attack() {
        if (!movement.isAttacking() && !movement.isCrouching() && !state.isCarrying() && !state.isDetected()) {

            //Play Once per Attack Animation
            movement.setAttacking(true);
            chop();
        }
    }

    private void chop() {
        //Animation
        playerAnimator.SetTrigger("chop");
        FindObjectOfType<AudioManager>().play("Swing");

        Collider2D[] destructables = Physics2D.OverlapCircleAll(attackPos.position, attackRange, cuttableMask);
        Collider2D[] metals = Physics2D.OverlapCircleAll(attackPos.position, attackRange, metalMask);

        bool hitMetal = (metals.Length != 0);
        bool hitDestructable = (destructables.Length != 0);

        //Calc Knock Back
        float chKnockBack = misshorizontalNockBack;
        float cvKnockBack = missVerticalNockBack;

        if (hitMetal || hitDestructable) {
            chKnockBack = hitHorizontalNockBack;
            cvKnockBack = hitVerticalNockBack;
        }

        //Apply Knock Back
        if (movement.isFacingRight()) {
            movement.nockBack(-chKnockBack, cvKnockBack);
        } else {
            movement.nockBack(chKnockBack, cvKnockBack);
        }

        if (hitMetal) FindObjectOfType<AudioManager>().play("Metal");

        //Select all Enemys in Range
        foreach (Collider2D dest in destructables) {
            //Hit Tree
            bool killed = dest.GetComponent<enemy>().takeDamage(attackDamage);

            if (killed) this.GetComponent<PlayerState>().makeCarry();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    public void cancleAttack() {
        movement.setAttacking(false);
    }
}
