using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MosnterAnimator : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update

 


    void Start()
    {
        anim = GetComponent<Animator>();

        Walk(true);

        
    }

    internal void stop(){

    }

    // Update is called once per frame
    void Update()
    {

        
       

    }

    void Walk(bool walk){
        Debug.Log(walk);
        anim.SetBool("Walk",walk);
    }
}
