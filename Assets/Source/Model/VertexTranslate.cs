using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTranslate : MonoBehaviour
{
    [System.Serializable]
    public enum Axis {X,Y,Z};

    public Axis axis;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Axis GetMyAxis()
    {
        return axis;
    }
}
