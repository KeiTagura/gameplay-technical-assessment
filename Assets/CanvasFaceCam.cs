using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasFaceCam : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Camera cam;
    [SerializeField] float speed = 0.3f;

    void Update()
        {
            Transform myCanvasTransform = canvas.transform;
            Vector3 direction = cam.transform.position - myCanvasTransform.position;
            Quaternion currentRotation = myCanvasTransform.rotation;
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, 0));
            myCanvasTransform.rotation = Quaternion.Lerp(currentRotation, newRotation, Time.deltaTime * speed);
        }
    }
