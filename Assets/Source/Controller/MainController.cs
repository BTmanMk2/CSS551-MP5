using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour
{

    public MyCamera mCamera;
    private Camera mCamera4RayCast;

    public MyMesh mMesh;

    // selected mover
    private VertexTranslate mSelectedVertexTranslate;

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

                // click on vertex ctrller or mover 
                if (hit)
                {
                    GameObject hitObject = hitInfo.transform.gameObject;

                    VertexNode vNode = hitObject.GetComponent<VertexNode>();
                    // click on vertex ctrl
                    if (vNode != null)
                    {
                        // switch mover on
                    }

                    // click on mover axis
                    else
                    {
                        mSelectedVertexTranslate = hitObject.GetComponent<VertexTranslate>();
                        if (mSelectedVertexTranslate != null)
                        {
                            // set initial translate position
                            

                            Vector3 point = new Vector3();
                            Vector2 mousePos = new Vector2
                            {
                                x = Input.mousePosition.x,
                                y = Input.mousePosition.y
                                //mCamera4RayCast.pixelHeight - 
                            };

                            point = mCamera4RayCast.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,
                                mCamera4RayCast.nearClipPlane));
                            mSelectedVertexTranslate.LMBPress(point);

                        }
                    }
                }
                else
                {
                    // switch mover off

                }
            }
            // drag support
            else if (Input.GetMouseButton(0))
            {
                if (mSelectedVertexTranslate)
                {
                    Vector3 point = new Vector3();
                    Vector2 mousePos = new Vector2
                    {
                        x = Input.mousePosition.x,
                        y = Input.mousePosition.y
                        //mCamera4RayCast.pixelHeight -
                    };

                    point = mCamera4RayCast.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,
                        mCamera4RayCast.nearClipPlane));

                    mSelectedVertexTranslate.LMBMove(point);
                }
                
            }
            // release LMB
            else if (Input.GetMouseButtonUp(0))
            {
                if (mSelectedVertexTranslate != null)
                {
                    mSelectedVertexTranslate.LMBRelease();
                }
                mSelectedVertexTranslate = null;
            }

        }
        // release LCTRL
        else
        {
            mMesh.VertexCtrlOff();
        }
    }
}