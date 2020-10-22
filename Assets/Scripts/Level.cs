using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //Serialized for debugging
    [SerializeField] int breakable;

    //Cached References
    SceneLoader scene;

    private void Start()
    {
        scene = FindObjectOfType<SceneLoader>();
    }   

    public void CountBreakableBlocks()
    {
        breakable++;
    }

    public void BlockDestroyed()
    {
        breakable--;

        if (breakable <= 0)
        {
            scene.LoadNextScene();
        }
    }
}
