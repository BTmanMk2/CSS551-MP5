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
		mCylinderVertexMover.SetActive(false);

		mMeshCylinder.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AssignMover(VertexController ctrller)
	{
		if (ctrller.mType == MyMesh.MeshType.Quad)	// quad vertex
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
		else
		{
			mCylinderVertexMover.SetActive(true);
			if (prevCylinderCtrller)
			{
				prevCylinderCtrller.Unselected();
			}
			prevCylinderCtrller = ctrller;

			ctrller.Selected(mCylinderVertexMover);
		}
		
		
		
	}

	public void AssignMover()
	{
		MyMesh.MeshType type = GetActiveMesh().mType;
		if (type == MyMesh.MeshType.Quad)
		{
			mQuadVertexMover.SetActive(false);
			if (prevQuadCtrller)
			{
				prevQuadCtrller.Unselected();
				prevQuadCtrller = null;
			}
		}
		else
		{
			mCylinderVertexMover.SetActive(false);
			if (prevCylinderCtrller)
			{
				prevCylinderCtrller.Unselected();
				prevCylinderCtrller = null;
			}
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
