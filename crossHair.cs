using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossHair : MonoBehaviour
{

    private Vector2 mousePos;

    private KeyCode mouseZ = KeyCode.Mouse0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(mouseZ))
        {
            Cursor.visible = false;
        }


        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector2.Lerp(transform.position, mousePos, 1);

    }
}
