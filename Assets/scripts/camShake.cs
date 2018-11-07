using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour {

    public Animator camAnim;

	public void shake(){
        camAnim.SetTrigger("shake");
    }
}
