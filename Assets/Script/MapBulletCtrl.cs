using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBulletCtrl : MonoBehaviour
{
    private Renderer renderer;

    void Start()
    {
        renderer = this.GetComponent<Renderer>();
    }

    void Update()
    {
        
    }

    // 마우스가 총알 위에 있을 때
    private void OnMouseEnter(){
        renderer.material.SetColor("_EmissionColor", Color.grey * 1.2f);  //발광도 설정
    }

    // 마우스가 총알 위에서 벗어났을때
    private void OnMouseExit(){
        renderer.material.SetColor("_EmissionColor", Color.clear);
    }
}
