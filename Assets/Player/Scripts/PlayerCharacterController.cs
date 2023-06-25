using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance;

    public Camera playerCamera;//����ӽ����
    public float gravityDownForce = 20f;//����
    public float maxSpeedOnGround = 8f;//����ٶ�
    public float moveSharpnessOnGround = 15f;//��Ӧʱ��
    public float rotationSpeed = 200f;//�����ת�ٶ�
    public float jumpHeight = 10f;

    public AudioSource moveSound;
    public AudioClip moveClip;
    private float checkTime = 0;
    private Vector3 lastPosition;

    //////////////////////////////////////////
    [SerializeField] private Camera weaponcamera;
    ///////////////////////////////////////

    public float cameraHeightRatio = 0.9f;//��������player�߶ȵı���

    private CharacterController _characterController;
    private PlayerInputHandler _inputHandler;
    private float playerHeight = 1.8f;//player�߶�
    private float cameraVerticalAngle = 0f;
    private float temp = 0;

    public Vector3 CharacterVelocity { get; set; }//�ٶȵ�����
    
    private void Awake()
    {
        Instance = this;
        moveSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        
        _characterController.enableOverlapRecovery = true;//��ֹ��ײ�ص��ظ���⵼����˸

        UpdateCharacterHeight();
    }

    private void Update()
    {
        /////////////////////////////////
        Ray ray = weaponcamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //ָʾ�ߴ�ӡ-���ڲ���
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

    private void UpdateCharacterHeight()//�߶ȸ���
    {
        _characterController.height = playerHeight;
        _characterController.center = Vector3.up * _characterController.height * 0.5f;//player����

        playerCamera.transform.localPosition = Vector3.up * _characterController.height * 0.9f;//���λ�ø���
    }

    // ReSharper restore Unity.ExpensiveCode
    private void PlayerMove()//ʵ��player�ƶ�
    {
        //���ˮƽ�ƶ�
        transform.Rotate(new Vector3(0, _inputHandler.GetMouseHorizontal() * rotationSpeed, 0), Space.Self);
        
        //�����ֱ�ƶ�
        cameraVerticalAngle += _inputHandler.GetMouseVertical() * rotationSpeed;

        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        playerCamera.transform.localEulerAngles = new Vector3(-cameraVerticalAngle, 0, 0);
        


        // Move 
        Vector3 worldLocation = transform.TransformVector(_inputHandler.GetMoveInput());//���ֲ�����ת��Ϊ��������

        if (_characterController.isGrounded)
        {
            Vector3 targetVelocity = worldLocation * maxSpeedOnGround;//����ٶ�����
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                CharacterVelocity += Vector3.up * jumpHeight;
            }
            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity, moveSharpnessOnGround * Time.deltaTime);
            //��ֵ��ʵ���ٶ�ƽ������
        }
        else
        {
            CharacterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;//��������
        }

        _characterController.Move(CharacterVelocity * Time.deltaTime);

        
    }

}
