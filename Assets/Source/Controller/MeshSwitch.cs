using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshSwitch : MonoBehaviour
{

	public Dropdown mMeshDropdown;
	public TheWorld mWorld;

	public MainController mMainController;

	// Use this for initialization
	void Start ()
	{
		Debug.Assert(mMeshDropdown != null);
		Debug.Assert(mWorld != null);
		Debug.Assert(mMainController != null);
		mMeshDropdown.onValueChanged.AddListener(SwitchMesh);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SwitchMesh(int val)
	{
		switch (val)
		{
			case 0:		//quad
				mWorld.ActiveQuad();
				break;
			case 1:
				mWorld.ActiveCylinder();
				break;
		}

		mMainController.UpdateActiveMesh();
	}

}
