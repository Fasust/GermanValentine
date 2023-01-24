using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal") * 100;
        Vertical = Input.GetAxisRaw("Vertical") * 100;

    }
}
