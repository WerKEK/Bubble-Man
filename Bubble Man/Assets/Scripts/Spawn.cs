using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField] GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawner", 1f);
    }

    void Spawner()
    {
        Instantiate(gm, transform.position, Quaternion.identity);
    }
}
