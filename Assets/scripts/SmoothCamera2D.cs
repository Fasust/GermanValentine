using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public Camera camera;
	public Vector3 offset;
	public int yOverwrite = 0;

	// Update is called once per frame
	void Update()
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;

			if (yOverwrite != 0)
			{
				destination.y = yOverwrite;
			}
			transform.position = Vector3.SmoothDamp(transform.position, destination + offset, ref velocity, dampTime);
		}

	}

	public void centerX()
	{
		offset.x = 0;
	}
}
 