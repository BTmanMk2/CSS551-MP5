using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{

	public SliderWithEcho mMeshX, mMeshY;

	private MyMesh mMesh;

	// Use this for initialization
	void Start ()
	{
		Debug.Assert(mMeshX != null);
		Debug.Assert(mMeshY != null);

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
		mMeshX.InitSliderRange(4, 20, 4);
		mMeshX.TheSlider.wholeNumbers = true;
		mMeshY.InitSliderRange(4, 20, 4);
		mMeshY.TheSlider.wholeNumbers = true;
	}

	void XValueChanged(float v)
	{
		if (mMesh)
		{
			mMesh.SetXSize((int)v);
		}
	}

	void YValueChanged(float v)
	{
		if (mMesh)
		{
			mMesh.SetYSize((int)v);
		}
		
	}
}
