using UnityEngine;

public class ThrownTree : MonoBehaviour
{

    public float throwForce;
    public float moveForce;
    public float decayTime;
    private float currentDecay;
    public PickUpRoad player;

    private bool setteled = false;
    private bool throwing = false;

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (throwing && !setteled)
        {
            if (currentDecay < decayTime)
            {
                currentDecay += Time.deltaTime;
            }
            else
            {
                settel();
                player.lose();
            }
        }
    }

    public void activateThrow()
    {
        throwing = true;
        body.AddForce(new Vector2(moveForce, throwForce));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        FindObjectOfType<AudioManager>().play("Hit");
    }

    public void settel()
    {
        if (setteled)
        {
            return;
        }
        FindObjectOfType<AudioManager>().play("Hit");
        body.velocity = Vector2.zero;
        body.gravityScale = 0;
        body.mass = 0;
        body.rotation = 0;
        setteled = true;
    }
}