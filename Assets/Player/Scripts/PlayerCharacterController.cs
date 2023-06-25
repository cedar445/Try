using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance;

    public Camera playerCamera;//玩家视角相机
    public float gravityDownForce = 20f;//重力
    public float maxSpeedOnGround = 8f;//最大速度
    public float moveSharpnessOnGround = 15f;//响应时间
    public float rotationSpeed = 200f;//相机旋转速度
    public float jumpHeight = 10f;

    public AudioSource moveSound;
    public AudioClip moveClip;
    private float checkTime = 0;
    private Vector3 lastPosition;

    //////////////////////////////////////////
    [SerializeField] private Camera weaponcamera;
    ///////////////////////////////////////

    public float cameraHeightRatio = 0.9f;//相机相对于player高度的比例

    private CharacterController _characterController;
    private PlayerInputHandler _inputHandler;
    private float playerHeight = 1.8f;//player高度
    private float cameraVerticalAngle = 0f;
    private float temp = 0;

    public Vector3 CharacterVelocity { get; set; }//速度的属性
    
    private void Awake()
    {
        Instance = this;
        moveSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        
        _characterController.enableOverlapRecovery = true;//防止碰撞重叠重复检测导致闪烁

        UpdateCharacterHeight();
    }

    private void Update()
    {
        /////////////////////////////////
        Ray ray = weaponcamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //指示线打印-用于测试
            Debug.DrawLine(ray.origin, raycastHit.point, Color.red);
        }
        ///////////////////////////////////////////
        

        PlayerMove();
        
        if(Time.time - checkTime > 0.5)
        {
            checkTime = Time.time;
            lastPosition = transform.position;
        }

        temp += Time.deltaTime;
        if(_characterController.isGrounded)
        {
            if ((transform.position-lastPosition).sqrMagnitude > 0.1f && temp >= 0.5)
            {                
                moveSound.PlayOneShot(moveClip);
                temp = 0;
            }
        }

    }

    private void UpdateCharacterHeight()//高度更新
    {
        _characterController.height = playerHeight;
        _characterController.center = Vector3.up * _characterController.height * 0.5f;//player中心

        playerCamera.transform.localPosition = Vector3.up * _characterController.height * 0.9f;//相机位置更新
    }

    // ReSharper restore Unity.ExpensiveCode
    private void PlayerMove()//实现player移动
    {
        //相机水平移动
        transform.Rotate(new Vector3(0, _inputHandler.GetMouseHorizontal() * rotationSpeed, 0), Space.Self);
        
        //相机垂直移动
        cameraVerticalAngle += _inputHandler.GetMouseVertical() * rotationSpeed;

        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        playerCamera.transform.localEulerAngles = new Vector3(-cameraVerticalAngle, 0, 0);
        


        // Move 
        Vector3 worldLocation = transform.TransformVector(_inputHandler.GetMoveInput());//将局部坐标转化为世界坐标

        if (_characterController.isGrounded)
        {
            Vector3 targetVelocity = worldLocation * maxSpeedOnGround;//最大速度向量
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                CharacterVelocity += Vector3.up * jumpHeight;
            }
            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity, moveSharpnessOnGround * Time.deltaTime);
            //插值，实现速度平滑过渡
        }
        else
        {
            CharacterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;//自由落体
        }

        _characterController.Move(CharacterVelocity * Time.deltaTime);

        
    }

}
