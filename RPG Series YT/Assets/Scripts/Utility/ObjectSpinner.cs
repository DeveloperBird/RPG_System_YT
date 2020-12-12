using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpinner : MonoBehaviour {

    public float speed = 150;

	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(0, Time.deltaTime * speed, 0);
	}
}
