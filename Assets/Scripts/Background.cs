using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	public Sprite[] frames;

	public int framesPerSecond = 10;

	private SpriteRenderer SpriteRenderer;
	private void Start()
	{
		SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		int index = (int) (Time.time * framesPerSecond); 
		index = index % frames.Length; 
		SpriteRenderer.sprite = frames[index]; 
	}
}
