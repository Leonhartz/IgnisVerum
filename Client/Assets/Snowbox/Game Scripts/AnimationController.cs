using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
	public Transform character = null;

	private Vector3 prevPos = Vector3.zero;

	void Start()
	{
		// Add a duplicate shoot animation which we set up to only animate the upper body
		// We use this animation when the character is running.
		// By using mixing for this we dont need to make a seperate running-shoot animation
		GetComponent<Animation>().AddClip(GetComponent<Animation>()["Throw"].clip, "ThrowUpperBody");
		GetComponent<Animation>()["ThrowUpperBody"].AddMixingTransform(transform.Find("Armature.001/Mover/SpineRoot"));

		// Set all animations to loop
		GetComponent<Animation>().wrapMode = WrapMode.Loop;

		// Except our action animations, Dont loop those
		GetComponent<Animation>()["JumpWave"].wrapMode = WrapMode.Clamp;
		GetComponent<Animation>()["Throw"].wrapMode = WrapMode.Clamp;
		GetComponent<Animation>()["ThrowUpperBody"].wrapMode = WrapMode.Clamp;

		// Put idle and run in a lower layer. They will only animate if our action animations are not playing
		GetComponent<Animation>()["StandCraze"].layer = -1;
		GetComponent<Animation>()["Run"].layer = -1;
		GetComponent<Animation>()["StepLeft"].layer = -1;
		GetComponent<Animation>()["StepRight"].layer = -1;

		GetComponent<Animation>().Stop();
		
		prevPos = character.position;
	}
	
	void Update()
	{
		Vector3 velocity = character.position - prevPos;
		prevPos = character.position;

		float vertical = Vector3.Dot(character.forward, velocity);
		float horizontal = Vector3.Dot(character.right, velocity);

		if (Mathf.Abs(vertical) > 0.1f)
		{
			GetComponent<Animation>().CrossFade("Run");
			// Play animation backwards when running backwards
			GetComponent<Animation>()["Run"].speed = Mathf.Sign(vertical);
		}
		else if (horizontal < -0.1f)
		{
			GetComponent<Animation>().CrossFade("StepLeft");
		}
		else if (horizontal > 0.1f)
		{
			GetComponent<Animation>().CrossFade("StepRight");
		}
		else
		{
			GetComponent<Animation>().CrossFade("StandCraze");
		}
	}

	public void Throw()
	{
		// Play it only on the upper body
		GetComponent<Animation>().CrossFadeQueued("ThrowUpperBody", 0.1f, QueueMode.PlayNow);
	}
}
