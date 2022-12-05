using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCtrl : MonoBehaviour
{

    public float playerSpeed = 7.0f;    // 플레이어 이동속도
    public float mouseSpeed= 3.0f;      // 마우스 민감도
    float rotationX;                    // 좌우 시점
    float rotationY;                    // 위아래 시점

    public Transform gun;               // 총
    public Transform bullet;            // 총알
    public Transform spawnPoint;        // 총알 스판 포인트
    public float bullet_power = 30000.0f;
    int bulletCount = 1;                // 소유한 총알 수 (총알을 클릭했을 때, 총알을 소유하도록)

    public GameObject gameover;         // 게임오버

    AudioSource walkSound;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;               // cursor 숨기고 정가운데로 배치
        GameObject spawnPoint = GameObject.Find("bulletSpawn"); // 총알 스판포인트
        walkSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerMove();
        PlayerLook();

        Shoot();

    }

    // 사격
    void Shoot(){

        // 우클릭 조준
        if(Input.GetMouseButton(1)){     

            gun.localPosition = new Vector3(0, -3.8f, -0.25f);
            gun.localEulerAngles = new Vector3(0, 0, 0);

            if(Input.GetMouseButtonDown(0) && bulletCount > 0){     // 좌클릭, 소유한 총알이 있을때 발사

                //조준점으로 발사
                Transform prefab_bullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                Ray screen_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 shooting_ray = screen_ray.direction;

                prefab_bullet.GetComponent<Rigidbody>().AddForce(shooting_ray * bullet_power);
                gun.GetComponent<AudioSource>().Play();

                while (guntime)
                {
                    gun.localEulerAngles = new Vector3(-10.0f, 0, 0);
                }
                
                // if(Input.GetMouseButtonUp(0)){
                    
                // }
            }
            

        }else {
            gun.localPosition = new Vector3(-1.5f, -3.3f, 0f);
            gun.localEulerAngles = new Vector3(0, 0, -35);
        }

    }


    // 플레이어 걷기, 달리기
    void PlayerMove(){

        if(Input.GetButton("Run")){     //달리기 (몇초이상 못달리게, 플레이어소리)
            playerSpeed = 15.0f;
        }else {
            playerSpeed = 7.0f;  
        }        

        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
            if(!walkSound.isPlaying){
                walkSound.Play();
            }
        }else {
            walkSound.Stop();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float moveX = horizontal * Time.deltaTime * playerSpeed;
        float moveZ = vertical * Time.deltaTime * playerSpeed;
        this.transform.Translate(new Vector3(moveX, 0, moveZ));
    }


    //마우스로 플레이어 시점 변경
    void PlayerLook(){

        //좌우 회전 => 플레이어 회전
        rotationX += Input.GetAxis("Mouse X") * mouseSpeed;
        this.transform.eulerAngles = new Vector3(0, rotationX, 0);
        // this.transform.rotation = Quaternion.Euler(0, rotationX, 0); 안됨

        //위아래 회전 => 카메라만 회전
        rotationY += Input.GetAxis("Mouse Y") * mouseSpeed;
        rotationY = Mathf.Clamp(rotationY, -60.0f, 60.0f);  //각도 제한
        Camera.main.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        // Camera.main.transform.rotation = Quaternion.Euler(-rotationY, 0, 0);
    }


    // void OnTriggerStay(Collider other){  //오두막에 플레이어가 충돌하는동안 

    //     if(other.gameObject.tag=="Map"){
            
    //         MonsterMove.isSafe=true; //몬스터 무브.cs의 issafe를 true로 변경
    //     }
    //     else{
    //         Debug.Log("out");
            
    //     }
       
    // }

    // void OnTriggerExit(Collider other){ //오두막에서 나가면 issafe false
    //     if(other.gameObject.tag=="Map"){
    //         MonsterMove.isSafe=false;

    // }
    // }

    // void OnCollisionEnter(Collision coll){      //몬스터랑 플레이어랑 접촉했는지

    //     if(coll.gameObject.tag=="Monster"){
    //         this.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //         Instantiate(gameover);
    //     }

    // }

    
}
