using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponmove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("z"))
        {
            weapon.transform.transform.position = new Vector3(this.transform.position.x, 
                this.transform.position.y, this.transform.position.z + 1f);
        }
        if (Input.GetKey("q"))
        {
            weapon.transform.transform.position = new Vector3(this.transform.position.x -1f,
                this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey("d"))
        {
            weapon.transform.transform.position = new Vector3(this.transform.position.x +1f,
                this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey("s"))
        {
            weapon.transform.transform.position = new Vector3(this.transform.position.x,
                this.transform.position.y, this.transform.position.z - 1f);
        }
    }
}
