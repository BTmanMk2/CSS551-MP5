using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class MyMesh : MonoBehaviour {

	void InitializeMeshQuad()
	{
		theMesh.Clear();
		Vector3[] v = new Vector3[(xSize + 1) * (ySize + 1)];   //v is the vertices 
		Vector3[] n = new Vector3[(xSize + 1) * (ySize + 1)];   //n is the normal of each vertices
		int[] t = new int[xSize * ySize * 2 * 3];   //xSize*ySize is the number of triangles

		//initialize the vertices
		float xUnit = (float)width / (float)xSize;
		float yUnit = (float)height / (float)ySize;

		int i = 0;
		for (int y = 0; y <= ySize; y++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				//v[i++] = new Vector3(x, 0, y);
				v[i++] = new Vector3(x * xUnit, 0, y * yUnit);
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

}
