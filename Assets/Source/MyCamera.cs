using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {

    public GameObject mLookLocator;

    private Vector3 LMBPos = Vector3.zero;
    private Vector3 RMBPos = Vector3.zero;

    //private Transform cameraOrigin;

    private Vector3 defaultCamPos;
    private Quaternion defaultCamRot;
    private Vector3 defaultLocatorPos;

    private Vector3 locatorOrigin;
    private Vector3 locatorOffset = Vector3.zero;

    // Use this for initialization
    void Start () {
        Debug.Assert(mLookLocator != null);
        locatorOrigin = mLookLocator.transform.localPosition;
        defaultLocatorPos = locatorOrigin;
        //cameraOrigin = transform;
        defaultCamPos = transform.localPosition;
        defaultCamRot = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
	    transform.LookAt(mLookLocator.transform);
    }

    public void LMBPress(Vector3 pos)
    {
        //Debug.Log("LMB Pos:" + pos);
        LMBPos = pos;
    }

    public void LMBMove(Vector3 pos)
    {
        Vector3 delta = pos - LMBPos;
        LMBPos = pos;
        // rotate
        Quaternion qy = Quaternion.AngleAxis(-0.1f * delta.y, transform.right);
        Quaternion qx = Quaternion.AngleAxis(0.1f * delta.x, transform.up);

        float s = Mathf.Sign(transform.position.y - mLookLocator.transform.position.y);

        Vector3 front = mLookLocator.transform.position - transform.position;
        front.y = 0;
        float yAngle = Mathf.Acos(Vector3.Dot(front.normalized, transform.forward))
                       * s * Mathf.Rad2Deg;
        yAngle += -0.1f * delta.y;
        if (Mathf.Abs(yAngle) > 85)
        {
            qy = Quaternion.identity;
        }

        Matrix4x4 r = Matrix4x4.TRS(Vector3.zero, qx * qy, Vector3.one);
        Matrix4x4 invP = Matrix4x4.TRS(-mLookLocator.transform.position, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);
        transform.localPosition = newCameraPos;
        transform.LookAt(mLookLocator.transform);

    }

    public void LMBRelease()
    {

    }

    public void RMBPress(Vector3 pos)
    {
        RMBPos = pos;
    }

    public void RMBMove(Vector3 pos)
    {
        Vector3 delta = pos - RMBPos;
        delta.z = 0;
        RMBPos = pos;

        Vector3 movement = transform.up * delta.y + transform.right * delta.x;
        movement *= 0.05f;
        transform.localPosition += movement;
        locatorOrigin += movement;
        mLookLocator.transform.position = locatorOrigin + locatorOffset;

    }

    public void RMBRelease()
    {

    }

    public void MouseScroll(float s)
    {
        Vector3 movement = transform.forward * s * 2;
        transform.localPosition += movement;
    }

    public void SetLocatorOffset(ref Vector3 offset)
    {
        locatorOffset = offset;
        mLookLocator.transform.position = locatorOrigin + locatorOffset;
    }

    public Vector3 GetLocatorOffset()
    {
        return locatorOffset;
    }

    public void Reset()
    {
        transform.localPosition = defaultCamPos;
        transform.localRotation = defaultCamRot;
        locatorOffset = Vector3.zero;
        mLookLocator.transform.localPosition = defaultLocatorPos;
        locatorOrigin = defaultLocatorPos;
    }
}
