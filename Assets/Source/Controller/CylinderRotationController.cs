using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderRotationController : MonoBehaviour
{

	public SliderWithEcho mSliderRot;
	public MyMesh mMesh;

	// Use this for initialization
	void Start ()
	{
		Debug.Assert(mSliderRot != null);
		Debug.Assert(mMesh != null);
		InitSlider();
		mSliderRot.SetSliderListener(RotValueChanged);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitSlider()
	{
		mSliderRot.TheSlider.wholeNumbers = true;
		mSliderRot.InitSliderRange(10, 360, 120);
		
	}

	void RotValueChanged(float rot)
	{
		mMesh.SetRotation(rot);
		/*if (Mathf.Abs(rot - 360) < float.Epsilon)
		{
			mMesh.ReconcileNormals();
		}*/
	}
}
