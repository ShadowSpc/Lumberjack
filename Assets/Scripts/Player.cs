using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerState
{
    Idle,
    Run, 
    Work
}

public class Player : MonoBehaviour
{
    public static Player Instanse;

    Animator animator;
    Rigidbody rb;

    Tree targetTree;
    Transform targetPoint;    
    public Transform TargetPoint
    {
        get
        {
            return targetPoint;
        }
        set
        {
            targetPoint = value;
            if (value != null)
            {
                transform.LookAt(targetPoint.position);
                State = PlayerState.Run;                
            }
        }
    }

    PlayerState state;
    public PlayerState State
    {
        get
        {
            return state;
        }
        private set
        {
            if(value == PlayerState.Idle)
            {
                animator.SetBool("IsRun", false);
            }
            else if (value == PlayerState.Run)
            {
                animator.SetBool("IsWork", false);
                animator.SetBool("IsRun", true);
            }
            else if (value == PlayerState.Work)
            {
                animator.SetBool("IsWork", true);
            }
            state = value;
        }
    }

    void Awake()
    {
        Instanse = this;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {        
        Invoke("FindTarget", 0.5f);
    }

    void FixedUpdate()
    {
        if (State == PlayerState.Run)
        {
            if (Vector3.Distance(targetPoint.transform.position, transform.position) > 2)
            {
                rb.AddForce(transform.forward * 250);
            }
            else
            {
                if(TargetPoint == GameController.Instanse.BasePoint)
                {
                    State = PlayerState.Idle;
                    FindTarget();
                }
                else
                {
                    targetTree = targetPoint.GetComponent<Tree>();
                    State = PlayerState.Work;
                }                
            }
        }
    }

    public void FindTarget()
    {
        TargetPoint = GameController.Instanse.GetNearTree();
    }

    public void ToBase()
    {
        TargetPoint = GameController.Instanse.BasePoint;
        transform.LookAt(targetPoint.position);
    }

    public void Damage()
    {
        targetTree.Damage();
        if(targetTree == null || targetTree.HP == 0)
        {
            ToBase();
        }
    }
}
