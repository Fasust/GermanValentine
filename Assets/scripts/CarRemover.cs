using UnityEngine;

public class CarRemover : MonoBehaviour
{
    public GameObject car;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= this.transform.position.x)
        {
            car.SetActive(false);
        }
    }
}
