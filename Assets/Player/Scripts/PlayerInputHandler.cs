using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
   public static PlayerInputHandler Instance;
   public float lookSensitivity = 1f;//视角灵敏度
   
   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;//鼠标指针固定在屏幕中心
      //Cursor.visible = false;//隐藏鼠标指针
   }

   public Vector3 GetMoveInput()//player移动位置的变量
   {
      Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
      move = Vector3.ClampMagnitude(move, 1);
      return move;
   }

   public float GetMouseHorizontal()//获取鼠标水平位置
   {
      return GetMouseAxis("Mouse X");
   }

   public float GetMouseVertical()//获取鼠标垂直位置
    {
      return GetMouseAxis("Mouse Y");
   }

   public float GetMouseAxis(string s)//获取鼠标虚拟轴
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
