using UnityEngine;

public class SetColorOnMessage : MonoBehaviour
{
	void SetColor(Color color)
	{
		GetComponent<Renderer>().material.color = color;
	}
}
