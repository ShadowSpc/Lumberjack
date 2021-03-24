using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController Instanse;

    public Transform BasePoint;
    public Transform TreesContainer;
    public Transform SticksContainer;
    public GameObject TreePref;

    List<Tree> trees = new List<Tree>();

    bool isDrag;
    Vector2 startMovePosition;
    Vector2 prevMovePosition;

    void Awake()
    {
        Instanse = this;
    }

    void Update()
    {
        if(isDrag)
        {
            Vector2 position = Input.mousePosition;
            Vector2 deltaPos = position - prevMovePosition;
            transform.position = transform.position + new Vector3(-deltaPos.x, 0, -deltaPos.y) * 0.01f;
            prevMovePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            startMovePosition = Input.mousePosition;
            prevMovePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            Vector2 position = Input.mousePosition;
            if((position - startMovePosition).sqrMagnitude < 1000f)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject go = Instantiate(TreePref, TreesContainer);
                    go.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    if (Player.Instanse.State == PlayerState.Idle)
                    {
                        Player.Instanse.TargetPoint = go.transform;
                    }
                }
            }
        }
    }

    public void AddTree(Tree tree)
    {
        trees.Add(tree);
    }

    public void RemoveTree(Tree tree)
    {
        trees.Remove(tree);
    }

    public Transform GetNearTree()
    {
        if (trees.Count > 0)
        {
            float minDistance = float.PositiveInfinity;
            Tree tree = null;
            foreach (var item in trees)
            {
                if (Vector3.Distance(item.transform.position, Player.Instanse.transform.position) < minDistance)
                {
                    tree = item;
                    minDistance = Vector3.Distance(item.transform.position, Player.Instanse.transform.position);
                }
            }
            return tree.transform;
        }
        else
            return null;
    }

}
