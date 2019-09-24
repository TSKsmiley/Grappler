using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{

    public GameObject Blackimg;
    // Start is called before the first frame update
    void Start()
    {
        Blackimg.SetActive(true);
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.8f);
        Blackimg.SetActive(false);
    }
    
}
