using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public ChrControllerBolt target;
    public bool isAttached = false;
    public Vector3 targetOffset;
    private Vector2 mousePoint;
    private Vector2 mousePos;
    private Camera cam;
    

    [SerializeField]
    private float CameraShift = 2;

    private void Start()
    {        
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (isAttached)
        {
            mousePos = Input.mousePosition;
            mousePoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            transform.position = target.transform.position + targetOffset + new Vector3((Mathf.Clamp01(mousePos.x / Screen.width) - 0.5f) * CameraShift, 0, (Mathf.Clamp01(mousePos.y / Screen.height) - 0.5f) * CameraShift);
        }
    }

    public void AttachCamera(ChrControllerBolt player)
    {
        if (isAttached == false)
        {
            target = player;
            // transform.LookAt(target.transform);
            isAttached = true;
        }
    }
}


