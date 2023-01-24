using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private Movement movement;
    private bool stepsPlaying;
    private bool jumpPlaying;
    // Start is called before the first frame update
    void Start()
    {
        movement = this.GetComponent<Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sound --------------------------------------------------------
        if (movement.getHorizontalMove() != 0)
        {
            if (!stepsPlaying)
            {
                stepsPlaying = true;
                FindObjectOfType<AudioManager>().play("Step");
            }

        }
        else
        {
            stepsPlaying = false;
            FindObjectOfType<AudioManager>().stop("Step");
        }

        if (movement.isJumping() && !jumpPlaying)
        {
            jumpPlaying = true;
            FindObjectOfType<AudioManager>().play("Jump");
        }


        if (movement.isCrouching() || movement.isJumping())
        {
            stepsPlaying = false;
            FindObjectOfType<AudioManager>().stop("Step");
        }
    }
    public void onLand()
    {
        jumpPlaying = false;
        FindObjectOfType<AudioManager>().stop("Jump");
        FindObjectOfType<AudioManager>().play("Land");
    }
}
