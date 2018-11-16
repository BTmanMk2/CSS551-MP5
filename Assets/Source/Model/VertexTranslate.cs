using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTranslate : MonoBehaviour
{
    [System.Serializable]
    public enum Axis {X,Y,Z};

    public Axis axis;

    private Vector3 lastLMBPos = Vector3.zero;

    private Color initMatColor;

    private float moveMagnifier = 10.0f;
    // Use this for initialization
    void Start ()
    {
        initMatColor = this.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Axis GetMyAxis()
    {
        return axis;
    }

    public void LMBPress(Vector3 point)
    {
        lastLMBPos = point;
        this.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void LMBMove(Vector3 point)
    {
        Vector3 delta = point - lastLMBPos;
        lastLMBPos = point;
        
        switch (axis)
        {
            case Axis.X:
                delta.y = 0;
                delta.z = 0;
                break;
            case Axis.Y:
                delta.x = 0;
                delta.z = 0;
                //delta.y = -delta.y;
                break;
            case Axis.Z:
                delta.x = 0;
                delta.y = 0;
                break;
        }

        delta *= moveMagnifier;
        //Debug.Log(delta);
        // move vertex
        this.transform.parent.localPosition += delta;
    }

    public void LMBRelease()
    {
        this.GetComponent<Renderer>().material.color = initMatColor;
    }


}
