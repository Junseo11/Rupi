using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCtrl : MonoBehaviour
{
    public float playerSpeed = 10.0f;   // 플레이어 이동속도

    CharacterController character;      // 마우스로 캐릭터 컨트롤???? 아닌듯
    public float mouseSpeed= 3.0f;      // 마우스 민감도
    float rotationX;                    // 좌우 시점
    float rotationY;                    // 위아래 시점



    void Start()
    {
        character = this.GetComponent<CharacterController>();   //?
    }

    void Update()
    {
        PlayerMove();
        MouseLook();

        //? 업데이트에 넣는 이유는? 마우스커서 중앙좌표에 고정, 커서 안보임
        Cursor.lockState = CursorLockMode.Locked;
        
    }


    // 플레이어 이동
    void PlayerMove(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float moveX = horizontal * Time.deltaTime * playerSpeed;
        float moveZ = vertical * Time.deltaTime * playerSpeed;

        this.transform.Translate(new Vector3(moveX, 0, moveZ));
    }

    //마우스로 플레이어 시점 변경
    void MouseLook(){

        //좌우 회전 => 플레이어 회전
        rotationX += Input.GetAxis("Mouse X") * mouseSpeed;
        this.transform.eulerAngles = new Vector3(0, rotationX, 0);

        //위아래 회전 => 카메라만 회전
        rotationY += Input.GetAxis("Mouse Y") * mouseSpeed;
        rotationY = Mathf.Clamp(rotationY, -70.0f, 70.0f);  //각도 제한
        Camera.main.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }

    // void OnGUI(){

    // }

}
