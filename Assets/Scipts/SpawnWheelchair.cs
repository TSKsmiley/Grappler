using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWheelchair : MonoBehaviour
{

    public GameObject wheelchair;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {

        Instantiate(wheelchair, transform.position, transform.rotation);
        yield return new WaitForSeconds(10);
        
        StartCoroutine(Spawn());
    }
}
