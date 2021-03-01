using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyweight : MonoBehaviour
{
    public Material[] materials;
    public float targetTime = 5f;
    private Rigidbody body;
    public int ballsNumber = 100;
    private Vector3 position;
    public Rigidbody ball;
    public int number;

    // Start is called before the first frame update
    void Start()

    {   // affichage d'un ballon par instance d'un Flyweight Factory
        var factory = new FlyweightFactory();
        var ballon = factory.getFlyweight(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        ballon.show(new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)), materials[Random.Range(0, materials.Length)]);
        //cube de visée
        body = GameObject.Find("Cube").GetComponent<Rigidbody>();
        body.useGravity = false;
        body.GetComponent<Renderer>().material = materials[Random.Range(0,materials.Length)];
        
        for (int i=0; i<ballsNumber; i++) // affichage des autres ballons
        {
            factory = new FlyweightFactory();
            ballon = factory.getFlyweight(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            ballon.show(new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)), materials[Random.Range(0, materials.Length)]);
        }
    }
    private void OnCollisionEnter(Collision collision) //Détruire les objects qui tombent
    {
        Destroy(collision.collider.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion firerotation = Quaternion.Euler(new Vector3(0, 100, 0) * Time.deltaTime);
        body.MoveRotation(body.GetComponent<Rigidbody>().rotation * firerotation); // cube tournant

        body = GameObject.Find("Cube").GetComponent<Rigidbody>();

        targetTime -= Time.deltaTime; // changeons la couleur du carré toutes les 5 secondes
        if (targetTime <= 0.0f)
        {
            body.GetComponent<Renderer>().material = materials[Random.Range(0,materials.Length)];
            targetTime = 5f;
        }
        
        if (Input.GetMouseButtonDown(0)) //clic gauche
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                var selectionrender = selection.GetComponent<Renderer>();
                if (selectionrender.material.color == body.GetComponent<Renderer>().material.color)
                {
                    Rigidbody tir = Rigidbody.Instantiate(body, transform.position, transform.rotation);
                    tir.useGravity = true;
                    Destroy(tir,2f*Time.deltaTime);
                    selection.GetComponent<Rigidbody>().useGravity = true;
                    body.isKinematic = true;
                }

            }
        }
    }

}

public abstract class FlyWeights
{
    public abstract void show(Vector3 position, Material couleur);
}

public class ConcreteFlyweight : FlyWeights
{

    protected GameObject Object;
    public ConcreteFlyweight (GameObject gameObject) //initialisation d'une instance
    {
        Object = gameObject;
    }
    public override void show(Vector3 positions, Material couleur) //afficher l'objet
    {
        Object.GetComponent<Renderer>().material = couleur;
        Object.AddComponent<Rigidbody>().position = new Vector3(positions.x, positions.y, positions.z);
        Object.GetComponent<Rigidbody>().useGravity = false;
        Object.GetComponent<Rigidbody>().drag = 2f;
    }
}

public class FlyweightFactory
{
    // dico du cache des instances
    private Dictionary<GameObject, FlyWeights> flyDico = new Dictionary<GameObject, FlyWeights>();
    //Nouvelle instance flyweight
    public FlyWeights getFlyweight(GameObject gameObject)
    {
        FlyWeights flyTest = null;
        if (flyDico.ContainsKey (gameObject)) // recherche d'un cache partagé
        {
            flyTest = flyDico[gameObject] as FlyWeights;
            Debug.Log("Returning cache image");
        }
        else
        {// créer et ajouter un fyweight au cache
            flyTest = new ConcreteFlyweight(gameObject);
            flyDico.Add(gameObject, flyTest);
            Debug.Log("Instantiating new object");
        }
        return flyTest;
    }
}