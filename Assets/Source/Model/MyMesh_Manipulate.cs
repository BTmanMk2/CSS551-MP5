using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class MyMesh : MonoBehaviour {
    VertexController[] mControllers;
	public GameObject mControllerPrefab = null;

    void InitControllers(Vector3[] v)
    {
        mControllers = new VertexController[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
	        GameObject temp = Instantiate(mControllerPrefab);
            //mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            temp.transform.localPosition = v[i];
            temp.transform.parent = this.transform;
	        mControllers[i] = temp.GetComponent<VertexController>();
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
}
