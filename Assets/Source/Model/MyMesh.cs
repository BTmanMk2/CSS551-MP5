using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
	// length of edges
    public int xSize = 4;
    public int ySize = 2;

    private Mesh theMesh = null;

    // Use this for initialization
    void Start ()
    {
	    Debug.Assert(mControllerPrefab != null);

        theMesh = GetComponent<MeshFilter>().mesh;
        InitializeMeshQuad();

    }

    // Update is called once per frame
    void Update () {
        //Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;
        int[] t = theMesh.triangles;
        for (int i = 0; i < mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, t, n);
        theMesh.vertices = v;
        theMesh.normals = n;
    }

    void SetTriangles(int xSize, int ySize, int[] triangles)
    {
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
    }

    void InitializeMeshQuad()
    {
        theMesh.Clear();
        Vector3[] v = new Vector3[(xSize + 1) * (ySize + 1)];//v is the vertices 
        Vector3[] n = new Vector3[(xSize + 1) * (ySize + 1)];//n is the normal of each vetices
        int[] t = new int[xSize * ySize * 2 * 3];//xSize*ySize is the number of triangles
        //initialize the vertices
	    int i = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //v[i] = new Vector3(x, y,0);
                v[i++] = new Vector3(x, 0, y);
            }
        }
        //initialize the triangles based on vertices
        SetTriangles(xSize, ySize, t);
        //initialize the origial normals
	    i = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                n[i++] = new Vector3(0, 0, 1);
            }
        }
        //transfer values into mesh
        theMesh.vertices = v;
        theMesh.triangles = t;
        theMesh.normals = n;
        //initialize the sphere on each vertex
        InitControllers(v);
	    InitNormals(v, n);
	}

	void SetXSize(int x)
	{
		xSize = x;
		InitializeMeshQuad();
	}

	void SetYSize(int y)
	{
		ySize = y;
		InitializeMeshQuad();
	}
}
