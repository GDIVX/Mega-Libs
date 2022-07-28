using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //calculate the offset between mouse click and transform
        Vector3 mousedown_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousedown_position.z = transform.position.z;

        offset = mousedown_position - transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 new_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        new_position.z = transform.position.z;
        transform.position = new_position - offset;
    }

    private void OnMouseUp()
    {   
        Debug.Log("on mouse up");
    }
}
