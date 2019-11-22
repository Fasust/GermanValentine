using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    public Animator playerAnimator;

    public int attackDamage;
    public float attackRange;
    public Transform attackPos;

	public Button chopButton;

    public LayerMask cutbableMask;

	private void Start()
	{
		chopButton.onClick.AddListener(chop);
	}

	private void Update(){
		//REMOVE
		if(Input.GetButtonDown("Jump")){
			chop();
		}
	}

	void chop() {
		//Play Once per Attack Animation
		if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_chop") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_carrying") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_carrying_idel") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_carrying_jump") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_carrying_land") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_carrying_sneak") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_sneak") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_sneak_idel") &&
			!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("german_detected"))
		{
            
			//Animation
            playerAnimator.SetTrigger("chop");

			//Sound
			FindObjectOfType<AudioManager>().play("Swing");

			//Get Destructables
			Collider2D[] destructables = Physics2D.OverlapCircleAll(attackPos.position, attackRange, cutbableMask);

			//Select all Enemys in Range
            foreach( Collider2D dest in destructables){

				//Damage them
				bool killed = dest.GetComponent<enemy>().takeDamage(attackDamage);

				if (killed)
				{
					//Switch to Carry if one dies
					this.GetComponent<PlayerState>().makeCarry();
				}
            }
        }
    }

	private void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(attackPos.position, attackRange);
	}
}
