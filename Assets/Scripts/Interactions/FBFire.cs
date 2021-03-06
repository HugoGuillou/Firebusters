﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBFire : MonoBehaviour {
	public delegate void FireEvent ();
	public event FireEvent OnPutOut;

	private float putOutTime = 0.5f;
	private Light fireLight;

	private void Start () {
		fireLight = GetComponent<Light> ();
		AkSoundEngine.PostEvent ("Play_Fire", gameObject);
	}

	public void PutOut () {
		if (OnPutOut != null)
			OnPutOut ();
		StartCoroutine (PutOutInternal ());
	}

	private IEnumerator PutOutInternal () {
		Vector3 initialScale = transform.localScale;
		float endTime = Time.time + putOutTime;
		float factor = 0.0f;
		AkSoundEngine.PostEvent ("Stop_Fire", gameObject);
		do {
			factor = (endTime - Time.time) / putOutTime;
			transform.localScale = Vector3.Lerp (initialScale, Vector3.zero, 1.0f - factor);
			fireLight.intensity *= factor;
			yield return null;
		} while (factor > 0.0f);
		Destroy (gameObject);
	}
}
