using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour
{
	public GameObject mQuadVertexMover = null;
	public GameObject mCylinderVertexMover = null;

	private VertexController prevQuadCtrller;
	private VertexController prevCylinderCtrller;

	public MyMesh mMeshQuad, mMeshCylinder;

	// Use this for initialization
	void Start ()
	{
		Debug.Assert(mQuadVertexMover != null);
		Debug.Assert(mCylinderVertexMover != null);
		Debug.Assert(mMeshQuad!=null);
		Debug.Assert(mMeshCylinder!=null);
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

	public MyMesh GetActiveMesh()
	{
		return (mMeshQuad.gameObject.activeSelf) ? mMeshQuad : mMeshCylinder;
	}

	public MyMesh GetQuad()
	{
		return mMeshQuad;
	}

	public MyMesh GetCylinder()
	{
		return mMeshCylinder;
	}

	public void ActiveQuad()
	{
		mMeshQuad.gameObject.SetActive(true);
		mMeshCylinder.gameObject.SetActive(false);
	}

	public void ActiveCylinder()
	{
		mMeshQuad.gameObject.SetActive(false);
		mMeshCylinder.gameObject.SetActive(true);
	}

}
