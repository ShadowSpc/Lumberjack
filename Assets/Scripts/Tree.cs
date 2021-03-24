using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject Stick;
    public int HP { get; private set; } = 3;
    void Start()
    {
        GameController.Instanse.AddTree(this);
    }

    public void Damage()
    {
        HP--;
        if(HP == 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameController.Instanse.RemoveTree(this);
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(Stick, GameController.Instanse.SticksContainer);
            go.transform.position = new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1));
            go.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 180), 0));
        }        
        Destroy(gameObject);
    }
}
