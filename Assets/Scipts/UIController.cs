using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {   
        anim = GetComponent<Animator>();
    }

    public void ButtonPlay()
    {
        anim.Play("MenuFall");
    }

    public void ButtonQuit()
    {
        Application.Quit(); 
    }
}
