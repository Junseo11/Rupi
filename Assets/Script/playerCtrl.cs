using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCtrl : MonoBehaviour
{

    public float playerSpeed = 7.0f;    // 플레이어 이동속도
    public float mouseSpeed= 3.0f;      // 마우스 민감도
    private float rotationX;            // 좌우 시점
    private float rotationY;            // 위아래 시점

    public Transform gun;               // 총(fps prefab)
    public Transform bullet;            // 총알
    public Transform spawnPoint;        // 총알 스판 포인트
    public float bulletPower = 25000.0f;
    private int bulletCount = 0;        // 소유한 총알 수 (총알을 클릭했을 때, 총알을 소유하도록)

    // bool gunMotion = false;          // 총반동
    // float guntime;

    public GameObject gameover;         // 게임오버

    AudioSource walkSound;              // 걷는소리
    AudioSource getBulletSound;         // 총알 줍는 소리

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;                          // cursor배치
        GameObject spawnPoint = GameObject.Find("bulletSpawn");            // 총알 스판포인트
        GameObject gunBody = GameObject.Find("Gun");
        walkSound = this.GetComponent<AudioSource>();                      // 걷는 소리
        getBulletSound = gunBody.transform.GetComponent<AudioSource>();    // 총알얻는소리
    }

    void Update()
    {
        PlayerMove();
        PlayerLook();
        Shoot();
        GetBullet();
    }


    // 플레이어 걷기, 달리기
    private void PlayerMove(){

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
    private void PlayerLook(){

        //좌우 회전 => 플레이어 회전
        rotationX += Input.GetAxis("Mouse X") * mouseSpeed;
        this.transform.eulerAngles = new Vector3(0, rotationX, 0);
        
        //위아래 회전 => 카메라만 회전
        rotationY += Input.GetAxis("Mouse Y") * mouseSpeed;
        rotationY = Mathf.Clamp(rotationY, -60.0f, 60.0f);  //각도 제한
        Camera.main.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }


    // 사격
    private void Shoot(){
        // 우클릭 조준
        if(Input.GetMouseButton(1)){     

            gun.localPosition = new Vector3(0, -3.8f, -0.25f);
            gun.localEulerAngles = new Vector3(0, 0, 0);

            if(Input.GetMouseButtonDown(0) && bulletCount > 0){     // 좌클릭, 소유한 총알이 있을때 발사

                //조준점으로 발사
                Transform prefab_bullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                Ray screen_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 shooting_ray = screen_ray.direction;

                prefab_bullet.GetComponent<Rigidbody>().AddForce(shooting_ray * bulletPower);
                gun.GetComponent<AudioSource>().Play();
                bulletCount -= 1;

                // gunAction(); 보류               
            }
        }else {
            // if(!gunMotion){
            //     gun.localEulerAngles = new Vector3(0, 0, -35);
            // }
            gun.localEulerAngles = new Vector3(0, 0, -35);
            gun.localPosition = new Vector3(-1.5f, -3.3f, 0f);
        }
    }
    

    // 총알줍기
    void GetBullet(){
        
        if(Input.GetMouseButtonDown(0)){

            RaycastHit hit;
            bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            if(isHit){
                if(hit.transform.gameObject.tag == "bullet"){                  
                    Destroy(hit.transform.gameObject);
                    bulletCount += 1;
                    getBulletSound.Play();
                }
            }

        }
    }



    // 총 반동
    // IEnumerator gunAction(){
    //     guntime += Time.deltaTime;

    //     if(guntime < 2.0f){
    //         gunMotion = true;
    //     }else{
    //         gunMotion = false;
    //     }

    //     while (gunMotion)
    //     {
    //         gun.localEulerAngles = new Vector3(-10.0f, 0, 0);
    //         yield return null;
    //     }        
        
    // }


}
