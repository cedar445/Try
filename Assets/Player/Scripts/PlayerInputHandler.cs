using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
   public static PlayerInputHandler Instance;
   public float lookSensitivity = 1f;//�ӽ�������
   
   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;//���ָ��̶�����Ļ����
      //Cursor.visible = false;//�������ָ��
   }

   public Vector3 GetMoveInput()//player�ƶ�λ�õı���
   {
      Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
      move = Vector3.ClampMagnitude(move, 1);
      return move;
   }

   public float GetMouseHorizontal()//��ȡ���ˮƽλ��
   {
      return GetMouseAxis("Mouse X");
   }

   public float GetMouseVertical()//��ȡ��괹ֱλ��
    {
      return GetMouseAxis("Mouse Y");
   }

   public float GetMouseAxis(string s)//��ȡ���������
   {
      float i = Input.GetAxisRaw(s);
      i *= lookSensitivity * 0.01f;

      return i;
   }

    public bool GetFireInput()
    {
        return Input.GetButton("Fire");
    }
}
