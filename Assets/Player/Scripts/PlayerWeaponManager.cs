using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<WeaponController> startingWeapons = new List<WeaponController>();

    public Transform weaponParentSocket;

    public Camera weaponCamera;

    public UnityAction<WeaponController> onSwitchedToWeapon;

    private WeaponController[] _weaponSlots = new WeaponController[9];

    private int choosedWeapon;


    private void Start()
    {
        choosedWeapon = 0;

        onSwitchedToWeapon += OnWeaponSwitched;

        for (int i = 0; i < startingWeapons.Count; i++)
        {
            {
                AddWeapon(startingWeapons[i], i);
            }
        }


        SwitchWeapon();
    }

    private void Update()
    {
        WeaponController activeWeapon = _weaponSlots[0];
        if (activeWeapon != null)
        {
            activeWeapon.HandleShootInput(PlayerInputHandler.Instance.GetFireInput());
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            _weaponSlots[choosedWeapon].ShowWeapon(false);
            
            switch(choosedWeapon)
            {
                case 0:
                    {
                        _weaponSlots[0].shootDelay = 0.5f;
                        //_weaponSlots[0].weaponMuzzle.localPosition += Vector3.up * 0.1f;
                        _weaponSlots[0].weaponMuzzlePrefab = startingWeapons[1].weaponMuzzlePrefab;
                        _weaponSlots[0].choosedWeaponShoot = 1;
                        _weaponSlots[0].projectilePrefab = startingWeapons[1].projectilePrefab;
                        _weaponSlots[0].oneShoot = startingWeapons[1].oneShoot;
                        _weaponSlots[0].shootSound = startingWeapons[1].shootSound;
                        
                        break;

                    }
                case 1:
                    {
                        _weaponSlots[0].shootDelay = 0.1f;
                        //_weaponSlots[0].weaponMuzzle.localPosition -= Vector3.up * 0.1f;
                        _weaponSlots[0].weaponMuzzle.localPosition += new Vector3 (0,1.5f,1) * 0.1f;
                        _weaponSlots[0].weaponMuzzlePrefab = startingWeapons[2].weaponMuzzlePrefab;
                        _weaponSlots[0].choosedWeaponShoot = 2;
                        _weaponSlots[0].projectilePrefab = startingWeapons[2].projectilePrefab;
                        _weaponSlots[0].oneShoot = startingWeapons[2].oneShoot;
                        _weaponSlots[0].shootSound = startingWeapons[2].shootSound;
                        break;
                    }
                case 2:
                    {
                        _weaponSlots[0].shootDelay = 0.1f;
                        //_weaponSlots[0].weaponMuzzle.localPosition += Vector3.up * 0.1f;
                        _weaponSlots[0].weaponMuzzle.localPosition -= new Vector3(0, 1.5f, 1) * 0.1f;
                        _weaponSlots[0].weaponMuzzlePrefab = startingWeapons[0].weaponMuzzlePrefab;
                        _weaponSlots[0].choosedWeaponShoot = 0;
                        _weaponSlots[0].projectilePrefab = startingWeapons[0].projectilePrefab;
                        _weaponSlots[0].oneShoot = startingWeapons[0].oneShoot;
                        _weaponSlots[0].shootSound = startingWeapons[0].shootSound;
                        
                        break;
                    }
            }
            choosedWeapon++;
            if(choosedWeapon>2)
            {
                choosedWeapon = 0;
            }
            _weaponSlots[choosedWeapon].ShowWeapon(true);
            SwitchWeapon(choosedWeapon);
            
        }
    }

    /*public bool AddWeapon(WeaponController weaponPrefab)
    {
        for(int i=0;i< startingWeapons.Count; i++)
        {

            WeaponController weaponInstance = Instantiate(weaponPrefab, weaponParentSocket);
            weaponInstance.transform.localPosition= Vector3.zero;
            weaponInstance.transform.rotation = Quaternion.identity;

            weaponInstance.owner = gameObject;
            weaponInstance.sourcePrefab = weaponPrefab.gameObject;
            weaponInstance.ShowWeapon(false);

            _weaponSlots[i]= weaponInstance;

            return true;
        }
    }*/

    public bool AddWeapon(WeaponController weaponPrefab, int position)
    {
            if (position >= 0 && position < _weaponSlots.Length && _weaponSlots[position] == null)
            {
                WeaponController weaponInstance = Instantiate(weaponPrefab, weaponParentSocket);
                weaponInstance.transform.localPosition = Vector3.zero;
                weaponInstance.transform.localRotation = Quaternion.identity;

                weaponInstance.owner = gameObject;
                weaponInstance.sourcePrefab = weaponPrefab.gameObject;
                weaponInstance.ShowWeapon(false);

                _weaponSlots[position] = weaponInstance;

                return true;
            }
            else
            {
                return false;
            }
    }

    public void SwitchWeapon(int i=0) 
    {
        SwitchWeaponToIndex(i);
    }

    public void SwitchWeaponToIndex(int index)
    {
        if (index >= 0)
        {
            WeaponController newWeapon = GetWeaponAtSlotIndex(index);

            if(onSwitchedToWeapon!= null)
            {
                onSwitchedToWeapon.Invoke(newWeapon);
            }
        }

    }

    public WeaponController GetWeaponAtSlotIndex(int index)
    {
        if (index >= 0 && index<=_weaponSlots.Length)
        {
            return _weaponSlots[index];
        }
        return null;
    }

    private void OnWeaponSwitched(WeaponController newWeapon)
    {
        if(newWeapon!=null)
        {
            newWeapon.ShowWeapon(true);
        }
    }

}
