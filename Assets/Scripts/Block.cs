using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject vFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] int pointValue;
     
    // Cached References
    Level level;
    GameStatus status;

    // State Variables
    [SerializeField] int timesHit;


    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
        status = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing from array " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayAudio();
        status.AddToScore(pointValue); //Allows you to set the pointValue of the block on the prefab and it gets passed back.  Helps with adding different value blocks.
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerVfx();
    }

    private void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    private void TriggerVfx()
    {
        GameObject sparkles = Instantiate(vFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
