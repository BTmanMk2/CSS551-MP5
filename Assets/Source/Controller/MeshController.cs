using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{

	public SliderWithEcho mMeshX, mMeshY;
	public TheWorld mWorld;

	private MyMesh mMesh;

	// Use this for initialization
	void Start ()
	{
		Debug.Assert(mMeshX != null);
		Debug.Assert(mMeshY != null);
		Debug.Assert(mWorld != null);

		mMeshX.SetSliderListener(XValueChanged);
		mMeshY.SetSliderListener(YValueChanged);

		InitSliders();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSelectedMesh(MyMesh mesh)
	{
		mMesh = mesh;
	}

	void InitSliders()
	{
		mMeshX.TheSlider.wholeNumbers = true;
		mMeshX.InitSliderRange(1, 19, 4);

		mMeshY.TheSlider.wholeNumbers = true;
		mMeshY.InitSliderRange(1, 19, 4);
		
	}

	void XValueChanged(float v)
	{
		if (mMesh)
		{
			mMesh.SetXSize((int)v);
			mWorld.AssignMover();
		}
	}

	void YValueChanged(float v)
	{
		if (mMesh)
		{
			mMesh.SetYSize((int)v);
			mWorld.AssignMover();
		}
	}

	public void ObjectSetSliders(int x, int y, MyMesh.MeshType type)
	{
		if (type == MyMesh.MeshType.Quad)
		{
			mMeshX.InitSliderRange(1, 19, x);
			mMeshY.InitSliderRange(1, 19, y);
		}
		else
		{
			mMeshX.InitSliderRange(3, 19, x);
			mMeshY.InitSliderRange(3, 19, y);
		}
		
	}
}
