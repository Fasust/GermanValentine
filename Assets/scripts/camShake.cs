using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour {

    public Animator camAnim;

	public void shake(){
        camAnim.SetTrigger("shake");
    }
	public void shakeTiny()
	{
		camAnim.SetTrigger("shakeTiny");
	}
	public void shakeDrive()
	{
		camAnim.SetTrigger("shakeDrive");
	}
	public void zoomOut()
	{
		camAnim.SetBool("zoomOut",true);
	}
}
