using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour
{
	public GameObject mQuadVertexMover = null;

	private VertexController prevQuadCtrller;

	// Use this for initialization
	void Start ()
	{
		Debug.Assert(mQuadVertexMover != null);
		mQuadVertexMover.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AssignMover(VertexController ctrller)
	{
		mQuadVertexMover.SetActive(true);

		// if quad or cylinder
		if (prevQuadCtrller)
		{
			prevQuadCtrller.Unselected();
		}
		prevQuadCtrller = ctrller;

		ctrller.Selected(mQuadVertexMover);
		
	}

	public void AssignMover()
	{
		mQuadVertexMover.SetActive(false);
		if (prevQuadCtrller)
		{
			prevQuadCtrller.Unselected();
			prevQuadCtrller = null;
		}
		
	}


}
