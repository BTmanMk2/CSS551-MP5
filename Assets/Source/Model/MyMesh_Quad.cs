using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class MyMesh : MonoBehaviour {

	private float rotation = 0;
	private float rdelta = 0;

	private float xTranslation = 0;
	private float yTranslation = 0;
	private float xScale = 1f;
	private float yScale = 1f;

	void InitializeMeshQuad()
	{
		theMesh.Clear();
		Vector3[] v = new Vector3[(xSize + 1) * (ySize + 1)];   //v is the vertices 
		Vector3[] n = new Vector3[(xSize + 1) * (ySize + 1)];   //n is the normal of each vertices
		int[] t = new int[xSize * ySize * 2 * 3];   //xSize*ySize is the number of triangles
		Vector2[] uv = new Vector2[(xSize + 1) * (ySize + 1)];
		originUV = new Vector2[(xSize + 1) * (ySize + 1)];
		//initialize the vertices
		float xUnit = (float)width / (float)xSize;
		float yUnit = (float)height / (float)ySize;

		int i = 0;
		for (int y = 0; y <= ySize; y++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				//v[i++] = new Vector3(x, 0, y);
				v[i] = new Vector3(x * xUnit, 0, y * yUnit);
				uv[i] = new Vector2(x * 1f / (float)xSize, y * 1f / (float)ySize);
				originUV[i] = uv[i];
				i++;
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
		theMesh.uv = uv;
		//initialize the sphere on each vertex
		InitControllers(v);
		InitNormals(v, n);
	}

	void QuadTextureTRS(ref Vector2[] uv)
	{
	
		float rot = rotation+rdelta;
		Vector2 translation = new Vector2(xTranslation, yTranslation);
		Vector2 scale = new Vector2(xScale, yScale);
		Matrix3x3 matrixTRS = Matrix3x3Helpers.CreateTRS(translation, rot, scale);

		for (int i = 0, y = 0; y <= ySize; y++)
		{
			for (int x = 0; x <= xSize; x++, i++)
			{
				uv[i] = Matrix3x3.MultiplyVector2(matrixTRS, originUV[i]);
			}
		}
	}

	public void SetTexRotation(Vector3 rot)
	{
		rdelta = rot.z;
	}

	public Vector3 GetTexRotation()
	{
		Vector3 v = new Vector3(0, 0, rotation);
		return v;
	}

	public void SetTexTranlation(Vector3 trans)
	{
		xTranslation = trans.x;
		yTranslation = trans.y;
	}


	public Vector3 GetTexTranslation()
	{
		Vector3 v = new Vector3(xTranslation, yTranslation, 0);
		return v;
	}

	public void SetTexScale(Vector3 scale)
	{
		xScale = scale.x;
		yScale = scale.y;
	}

	public Vector3 GetTexScale()
	{
		Vector3 v = new Vector3(xScale, yScale, 0);
		return v;
	}

	public void UnselectRotation()
	{
		rotation += rdelta;
		rdelta = 0;
	}
}
