using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;
    [SerializeField]
    private bool invert;

    [SerializeField]
    private float sensivity;
    [SerializeField]
    private int smooth_Steps = 10;

    private float smooth_Weight=0.4f;

    private Vector2 default_LookLimits = new Vector2(-70f, 80f);

    private Vector2 look_Angle;

    private Vector2 current_MouseLook;
    private Vector2 smooth_Move;

    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        //for windows

        //LockNadUnlockCursor();
        //if (Cursor.lockState == CursorLockMode.Locked)
        //    {
        //        LookAround();
        //    }
        if (Input.GetMouseButton(0))
            LookAround();
    }


    void LockNadUnlockCursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }//LockNadUnlockCursor()


    void LookAround()
    {

        current_MouseLook = new Vector2(Input.GetAxis(Axis.MouseY), Input.GetAxis(Axis.MouseX));

        look_Angle.x += current_MouseLook.x * sensivity * (invert ? 1f : -1f);
        look_Angle.y += current_MouseLook.y * sensivity ;
        look_Angle.x =Mathf.Clamp(look_Angle.x,default_LookLimits.x,default_LookLimits.y);

        lookRoot.localRotation = Quaternion.Euler(look_Angle.x, 0, 0);
        playerRoot.localRotation = Quaternion.Euler(0, look_Angle.y, 0);
    }



}//class
