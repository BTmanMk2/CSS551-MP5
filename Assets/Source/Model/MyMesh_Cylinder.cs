using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class MyMesh : MonoBehaviour
{

	void InitializeMeshCylinder()
	{
		theMesh.Clear();
		rads = new float[ySize + 1];
		for (int m = 0; m < ySize + 1; m++)
		{
			rads[m] = defaultRadius;
		}
		Vector3[] v = new Vector3[(xSize + 1) * (ySize + 1)];   //v is the vertices 
		Vector3[] n = new Vector3[(xSize + 1) * (ySize + 1)];   //n is the normal of each vertices
		int[] t = new int[xSize * ySize * 2 * 3];   //xSize*ySize is the number of triangles

		ThetaRotation(ref v);
		SetTriangles(xSize, ySize, t);
		//initialize the origial normals
		int i = 0;
		for (int y = 0; y <= ySize; y++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				n[i++] = new Vector3(0, 0, 1);
			}
		}

		theMesh.vertices = v;
		theMesh.triangles = t;
		theMesh.normals = n;
		//initialize the sphere on each vertex
		InitControllers(v);
		InitNormals(v, n);
	}

	void ThetaRotation(ref Vector3[] v)
	{
		float yUnit = (float)height / (float)ySize;
		float thetaUnit = (float)Theta / (float)xSize;
		for (int i = 0; i <= xSize; i++)
		{
			for (int j = 0; j <= ySize; j++)
			{
				float tempRad = (thetaUnit * i) * Mathf.Deg2Rad;
				float tempCos = Mathf.Cos(tempRad);
				float tempSin = Mathf.Sin(tempRad);
				float tempX = rads[j] * tempCos;
				float tempZ = rads[j] * tempSin;
				v[i + j * (xSize + 1)] = new Vector3(tempX, yUnit * j, tempZ);
			}
		}
	}

	public void SetRotation(float degree)
	{
		Theta = degree;
		float thetaUnit = (float) Theta / (float) xSize;
		Vector3[] v = theMesh.vertices;
		for (int i = 0; i <= xSize; i++)
		{
			for (int j = 0; j <= ySize; j++)
			{
				float tempRad = (thetaUnit * i) * Mathf.Deg2Rad;
				float tempCos = Mathf.Cos(tempRad);
				float tempSin = Mathf.Sin(tempRad);
				float tempX = rads[j] * tempCos;
				float tempZ = rads[j] * tempSin;
				v[i + j * (xSize + 1)].x = tempX;
				v[i + j * (xSize + 1)].z = tempZ;
			}
		}

		theMesh.vertices = v;
	}
}
