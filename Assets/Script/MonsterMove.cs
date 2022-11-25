using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterMove : MonoBehaviour
{

    public GameObject target;
    public UnityEngine.AI.NavMeshAgent agent;

    public static bool isSafe=false; // 안전지대에 들어가 있는지 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


      
        MoveToTarget();
        
    }


    void MoveToTarget(){
        

        if(isSafe==false){
            agent.SetDestination(target.transform.position); //타겟의 위치로 목적지
        }
        else
        {
            agent.SetDestination(new Vector3(0,0,0)); //제자리걸음
        }
        
        
    }
}
