using UnityEngine;

public class cameramoves : MonoBehaviour
{
    private float sensitivity = 10f;
    private float maxYangle = 80f;
    private Vector2 currentRotation;

    // Update is called once per frame
    void Update() //déplacement de la vue en focntion des axes de la souris
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y += Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYangle, maxYangle);
        Camera.main.transform.rotation = Quaternion.Euler(-currentRotation.y, currentRotation.x, 0);
        
        // déplacement de la caméra 
        if (Input.GetKey("z"))
        {
            Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x
                , Camera.main.transform.position.y,
                Camera.main.transform.position.z + 5*Time.deltaTime); 
        }
        if (Input.GetKey("s"))
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x
                , Camera.main.transform.position.y,
                Camera.main.transform.position.z - 5 * Time.deltaTime);
        }
        if (Input.GetKey("q"))
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x+5 * Time.deltaTime
                , Camera.main.transform.position.y,
                Camera.main.transform.position.z );
        }
        if (Input.GetKey("d"))
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x -5 * Time.deltaTime
                , Camera.main.transform.position.y,
                Camera.main.transform.position.z);
        }
    }
}
