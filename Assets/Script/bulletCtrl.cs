using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 물체에 부딪히면 총알 제거
    void OnCollisionEnter(Collision coll) {
        
        Destroy(this.gameObject);

    }
}
