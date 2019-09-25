using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{
    LineRenderer lr;
    public Rigidbody2D rbPlayer;
    public GameObject player;

    Vector2 playerOldPos;
    float timer;
    public float timeTillDeath;

    // Audio
    public AudioSource AudioSrc;

    public AudioClip ReloadSound;
    public AudioClip SniperSound;

    bool playerIsDying = false;
    bool reloadSoundHasPlayed = false;
    bool shootSoundHasPlayed = false;

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

                if (!reloadSoundHasPlayed)
                {
                    // Audio
                    AudioSrc.clip = ReloadSound;
                    AudioSrc.Play();
                    reloadSoundHasPlayed = true;
                }

                if (AudioSrc.isPlaying)
                {
                    playerIsDying = true;
                }
                else if (playerIsDying == true && AudioSrc.isPlaying == false)
                {
                    //Sets the lines width to 0.1
                    lr.startWidth = 0.1F;
                    lr.endWidth = 0.1F;

                    if (!shootSoundHasPlayed)
                    {
                        // Audio
                        AudioSrc.clip = SniperSound;
                        AudioSrc.Play();
                        shootSoundHasPlayed = true;
                    }

                    if (!AudioSrc.isPlaying)
                    {
                        // Reset everything
                        playerIsDying = false;
                        reloadSoundHasPlayed = false;
                        shootSoundHasPlayed = false;

                        lr.startWidth = 0.0f;
                        lr.endWidth = 0.0f;

                        timer = 0;

                        player.GetComponent<grapplingHook>().respawnPlayer();
                    }
                }
            }
        }
        else
        {
            // Reset everything
            playerIsDying = false;
            reloadSoundHasPlayed = false;
            shootSoundHasPlayed = false;

            lr.startWidth = 0.0f;
            lr.endWidth = 0.0f;

            timer = 0;
        }

        playerOldPos = rbPlayer.position;
    }
}
