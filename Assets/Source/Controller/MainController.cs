using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour
{

    public MyCamera mCamera;
    private Camera mCamera4RayCast;

    public MyMesh mMesh;

    // selected mover axis
    private GameObject mSelectedAxis;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(mCamera != null);
        Debug.Assert(mMesh!=null);
        mCamera4RayCast = mCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMouseEvent();
    }

    void ProcessMouseEvent()
    {
        // L ALT camera ctrl
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // LMB
            if (Input.GetMouseButtonDown(0))
            {
                mCamera.LMBPress(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                mCamera.LMBMove(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mCamera.LMBRelease();
            }

            //RMB
            else if (Input.GetMouseButtonDown(1))
            {
                mCamera.RMBPress(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                mCamera.RMBMove(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                mCamera.RMBRelease();
            }

            // wheel
            else if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0.00000001)
            {
                mCamera.MouseScroll(Input.GetAxis("Mouse ScrollWheel"));
            }

        }

        // L CTRL vertices ctrl switch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            mMesh.VertexCtrlOn();

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            
            if (Input.GetMouseButtonDown(0))
            {
                
                RaycastHit hitInfo = new RaycastHit();

                // clickable objects are on layer 9
                bool hit = Physics.Raycast(mCamera4RayCast.ScreenPointToRay(
                        Input.mousePosition), out hitInfo, Mathf.Infinity, 1 << 9);

                if (hit)
                {
                    GameObject hitObject = hitInfo.transform.gameObject;

                    VertexCtrl vCtrl = hitObject.GetComponent<VertexCtrl>();
                    if (vCtrl != null)
                    {
                        // switch mover on
                    }
                    else
                    {
                        VertexTranslate vTranslate = hitObject.GetComponent<VertexTranslate>();
                        if (vTranslate != null)
                        {
                            // set initial translate position
                            mSelectedAxis = hitObject;

                            Vector3 point = new Vector3();
                            Event currentEvent = Event.current;
                            Vector2 mousePos = new Vector2();

                            // Get the mouse position from Event.
                            // Note that the y position from Event is inverted.
                            mousePos.x = currentEvent.mousePosition.x;
                            mousePos.y = mCamera4RayCast.pixelHeight - currentEvent.mousePosition.y;

                            point = mCamera4RayCast.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,
                                mCamera4RayCast.nearClipPlane));


                        }
                    }
                    



                }
            }else if (Input.GetMouseButton(0))
            {
                RaycastHit hitInfo = new RaycastHit();

                // clickable objects are on layer 9
                bool hit = Physics.Raycast(mCamera4RayCast.ScreenPointToRay(
                        Input.mousePosition), out hitInfo, Mathf.Infinity, 1 << 9);

                if (hit)
                {
                    GameObject hitObject = hitInfo.transform.gameObject;

                }
            }else if (Input.GetMouseButtonUp(0))
            {

            }

        }
        else
        {
            mMesh.VertexCtrlOff();
        }
    }
}
