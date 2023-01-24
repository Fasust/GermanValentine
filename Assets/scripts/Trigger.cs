using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Transform player;
    public GameObject[] toTrigger;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PickUpRoad>().transform;
        setActiveOfToTrigger(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x <= player.position.x)
        {
            setActiveOfToTrigger(true);
        }
    }
    private void setActiveOfToTrigger(bool val)
    {
        for (int i = 0; i < toTrigger.Length; i++)
        {
            toTrigger[i].SetActive(val);
        }
    }
}
