using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class MyMesh: MonoBehaviour {
	LineSegment[] mNormals;

	Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }


    void SetFacenormal(Vector3[] v, int xsize, int ysize, Vector3[] trinormals)
    {
        for (int tn = 0, vi = 0, y = 0; y < ysize; y++, vi++)
        {
            for (int x = 0; x < xsize; x++, tn += 2, vi++)
            {
                trinormals[tn] = FaceNormal(v, vi, xsize + 1 + vi, 1 + vi);
                trinormals[tn + 1] = FaceNormal(v, vi + 1, xsize + 1 + vi, xsize + 2 + vi);
            }
        }
    }


    List<int> FindTriangles(int[] t, int tempv)
    {
        List<int> tempresult=new List<int>();
        tempresult.Clear();
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i] == tempv) { tempresult.Add(i / 3); }
        }
        return tempresult;
    }


    Vector3 TrianglesToNormals(List<int> triangles,Vector3[] triNormals)
    {
        Vector3 tempn=new Vector3();
        for(int i = 0; i < triangles.Count; i++)
        {
            tempn+= triNormals[triangles[i]];
        }
        tempn=tempn.normalized;
        return tempn;
    }

    void ComputeNormals(Vector3[] v,int[] t, Vector3[] n)
    {
        Vector3[] triNormals = new Vector3[xSize * ySize*2];//each triangle's normal
        SetFacenormal(v, xSize, ySize, triNormals);

        //now we need to use triNormals to update each vertex's normal
        for (int i = 0; i < v.Length; i++)
        {
            List<int> templist = FindTriangles(t,i);
            n[i]=TrianglesToNormals( templist, triNormals);
        }
        UpdateNormals(v, n);
    }


    
    //initialize the cylinder on the sphere of each vertex
    void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.05f);
            mNormals[i].transform.SetParent(this.transform);
        }
        UpdateNormals(v, n);
    }


    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] + 1.0f * n[i]);
        }
    }
}
