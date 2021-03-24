using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        while((Player.Instanse.transform.position - transform.position).sqrMagnitude > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.Instanse.transform.position, 0.4f);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
