using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{
    LineRenderer lr;
    public Rigidbody2D rbPlayer;

    Vector2 playerOldPos;
    float timer;
    public float timeTillDeath;

    // Audio
    public AudioSource AudioSrc;

    public AudioClip ReloadSound;
    public AudioClip SniperSound;

    bool playerIsDying = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();



        //Sets the lines width to 0
        lr.startWidth = 0F;
        lr.endWidth = 0F;
       
    }

    // Update is called once per frame
    void Update()
    {

        lr.SetPosition(0, this.transform.position);

        lr.SetPosition(1, rbPlayer.transform.position);
        
        if (playerOldPos == rbPlayer.position)
        {
            timer += Time.deltaTime;

            //Debug.Log(timer);

            if (timer >= timeTillDeath)
            {
                // Audio
                AudioSrc.clip = ReloadSound;
                AudioSrc.Play();

                if (AudioSrc.isPlaying)
                {
                    playerIsDying = true;
                }
                else if (playerIsDying == true && AudioSrc.isPlaying == false)
                {
                    //Sets the lines width to 0
                    lr.startWidth = 0.1F;
                    lr.endWidth = 0.1F;

                    // Audio
                    AudioSrc.clip = SniperSound;
                    AudioSrc.Play();

                    if (!AudioSrc.isPlaying)
                    {

                    }
                }
            }
        }
        else
        {
            timer = 0;
        }

        playerOldPos = rbPlayer.position;
    }
}
