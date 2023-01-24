using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    public Image image;
    public float minTranperancy;
    public float speed;

    private float currentTransperncy = 1;

    // Update is called once per frame
    void Update()
    {
        if (currentTransperncy > minTranperancy)
        {
            Color color = new Color(1, 1, 1, currentTransperncy);
        }
    }
}
