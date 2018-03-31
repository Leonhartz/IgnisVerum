using UnityEngine;
using System.Collections;

public class GUITextColor : MonoBehaviour
{
	public Color textColor = Color.white;

	void Awake ()
	{
		GetComponent<GUIText>().material.color = textColor;
	}
}
