using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{

	private GameObject mVertexMover = null;

	private Color originColor;

	public bool mAvailable = true;

	// Use this for initialization
	void Start ()
	{
		originColor = this.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (mVertexMover != null && mVertexMover.activeSelf)
		{
			transform.localPosition = mVertexMover.transform.localPosition;
		}
	}

	public void DisableController()
	{
		transform.gameObject.SetActive(false);
		if (mVertexMover)
		{
			mVertexMover.SetActive(false);
		}
	}

	public void EnableController()
	{
		transform.gameObject.SetActive(true);
		if (mVertexMover)
		{
			mVertexMover.SetActive(true);
		}
	}

	public void Selected(GameObject mover)
	{
		if (mAvailable)
		{
			mVertexMover = mover;
			mover.transform.localPosition = transform.localPosition;

			this.GetComponent<Renderer>().material.color = Color.red;
		}
		
	}

	public void Unselected()
	{
		mVertexMover = null;
		this.GetComponent<Renderer>().material.color = originColor;
	}

	public bool isAvailable()
	{
		return mAvailable;
	}

	public void SetEndPoints(Vector3 p1, Vector3 p2)
	{
		Vector3 mv = (p2 - p1).normalized;
		//float ml = (p2 - p1).magnitude;
		Quaternion q = Quaternion.FromToRotation(Vector3.up, mv);
		transform.localRotation = q;
	}
}
