﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone_rotation : MonoBehaviour {

	public float rotationSpeed;

	void Update () {
		transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
	}
}
