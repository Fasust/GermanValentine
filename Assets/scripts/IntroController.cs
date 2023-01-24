using UnityEngine;

public class IntroController : MonoBehaviour
{

    public GameObject Text;
    public float delay;
    private float currentProgress;

    // Update is called once per frame
    void Update()
    {
        if (currentProgress < delay)
        {
            currentProgress += Time.deltaTime;
        }
        else
        {
            Text.active = false;
        }
    }
}
