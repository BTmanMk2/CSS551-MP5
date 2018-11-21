using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

	public enum MeshType
	{
		Quad,
		Cylinder
	}

	public MeshType mType = MeshType.Quad;

	// number of edges, number+1 of vertices on edges
    public int xSize = 4;
    public int ySize = 4;

	// w&h for quad
	int width = 5;
	int height = 5;

	// height and radius for cylinder
	public float Theta = 90f;
	private float defaultRadius = 3f;
	private float[] rads;

	protected Mesh theMesh = null;

    // Use this for initialization
    void Awake ()
    {
	    Debug.Assert(mControllerPrefab != null);
        theMesh = GetComponent<MeshFilter>().mesh;
	    if (mType == MeshType.Quad)
	    {
		    InitializeMeshQuad();
		}
	    else
	    {
		    InitializeMeshCylinder();

	    }

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
	    if (mType == MeshType.Cylinder)
	    {
			UpdateRadius();
	    }
    }

    protected void SetTriangles(int xSize, int ySize, int[] triangles)
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

	public void SetXSize(int x)
	{
		xSize = x;
		if (mType == MeshType.Quad)
		{
			InitializeMeshQuad();
		}
		else
		{
			InitializeMeshCylinder();
		}
		
	}

	public void SetYSize(int y)
	{
		ySize = y;
		if (mType == MeshType.Quad)
		{
			InitializeMeshQuad();
		}
		else
		{
			InitializeMeshCylinder();
		}
	}

	public MeshType GetMeshType()
	{
		return mType;
	}
}
