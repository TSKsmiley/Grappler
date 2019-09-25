using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public GameObject BlackImage;

    // Start is called before the first frame update
    void Start()
    {
        BlackImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        
        StartCoroutine(Loader());
    }

    IEnumerator Loader()
    {
        yield return new WaitForSeconds(0.1f);
        BlackImage.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(1);
    }
}
