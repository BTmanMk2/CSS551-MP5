﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class MyMesh : MonoBehaviour {
    protected VertexController[] mControllers;
	public GameObject mControllerPrefab = null;

    protected void InitControllers(Vector3[] v)
    {
		ClearControllers();
        mControllers = new VertexController[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
	        GameObject temp = Instantiate(mControllerPrefab);
            //mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            temp.transform.localPosition = v[i];
            temp.transform.parent = this.transform;
	        mControllers[i] = temp.GetComponent<VertexController>();
        }

		// Cylinder
	    if (mType == MeshType.Cylinder)
	    {
		    for (int i = 0; i < mControllers.Length; i++)
		    {
			    int k = i % (xSize + 1);
			    if (k != 0) // not first column
			    {
				    mControllers[i].SetUnavailable();
			    }
		    }
	    }
    }

	void UpdateControllers(Vector3[] v)
	{
		for(int i = 0; i < v.Length; i++)
		{
			mControllers[i].transform.localPosition = v[i];
		}
	}

	void ClearControllers()
	{
		if (mControllers!=null)
		{
			for (int i = 0; i < mControllers.Length; i++)
			{
				if (mControllers[i] != null)
				{
					Destroy(mControllers[i].gameObject);
				}
			}
		}
		
	}


	public void DisableControllers()
	{
		for (int i = 0; i < mControllers.Length; i++)
		{
			mControllers[i].DisableController();
		}
	}

	public void EnableControllers()
	{
		for (int i = 0; i < mControllers.Length; i++)
		{
			mControllers[i].EnableController();
		}
	}

	public void UpdateRadius()
	{
		//Vector3[] v = theMesh.vertices;
		/* cylinder vertex indices arrangement
		 	12 13 14
		 	9 10 11
			6 7 8
			3 4 5
			0 1 2
		*/
		float d = rads[0];
		for (int i = 0; i < mControllers.Length; i++)
		{
			int k = i % (xSize+1);
			if (k == 0)	// first column
			{
				int row = i / (xSize + 1);
				// radius
				Vector2 radius=new Vector2(0,0);
				radius.x = mControllers[i].transform.localPosition.x;
				radius.y = mControllers[i].transform.localPosition.z;

				d = (radius - Vector2.zero).magnitude;
				rads[row] = d;
				continue;
			}
			Vector3 xz = new Vector3(mControllers[i].transform.localPosition.x,
								0f,
					mControllers[i].transform.localPosition.z).normalized;
			xz = xz * d;
			xz.y = mControllers[i-k].transform.position.y;
			mControllers[i].transform.localPosition = xz;
		}
	}
}
