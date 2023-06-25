using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ProjectileStandard : MonoBehaviour
{
    public float maxLifeTime = 5f;
    public float speed = 300f;
    
    
    public Transform root;
    public Transform tip;
    public float radius = 0.01f;
    public LayerMask hittableLayers = -1; //Åö×²¼ì²âÍ¼²ã

    public GameObject impactVFX;
    public float impactVFXLifeTime = 5f;
    public float impactVFXSpawnOffset = 0.1f;

    public float trajectoryCorrectionDistance = 5f;

    private ProjectileBase _projectileBase;
    private Vector3 _velocity;
    private Vector3 _lastRootPosition;
    
    private bool _hasTrajectoryCorrected;
    private Vector3 _correctionVector;
    private Vector3 _consumedCorrectionVector;

    //private float downTime = 0;

    private void OnEnable()
    {
        _projectileBase = GetComponent<ProjectileBase>();
        _projectileBase.onShoot += OnShoot;
        Destroy(gameObject, maxLifeTime);
    }

    private void OnShoot()
    {
        _lastRootPosition = root.position;
        _velocity += transform.forward * speed;

        PlayerWeaponManager playerWeaponManager =  _projectileBase.owner.GetComponent<PlayerWeaponManager>();

        if(playerWeaponManager != null )
        {
            _hasTrajectoryCorrected = false;

            Transform weaponCameraTransform = playerWeaponManager.weaponCamera.transform;

            Vector3 cameraToMuzzle = _projectileBase.initialPosition - weaponCameraTransform.position;

            _correctionVector = Vector3.ProjectOnPlane(-cameraToMuzzle, weaponCameraTransform.forward);
        }
        
    }

    private void Update()
    {
        
        transform.position += _velocity * Time.deltaTime;//ÒÆ¶¯
        

        transform.forward = _velocity.normalized;//·½Ïò

        
        if (!_hasTrajectoryCorrected && _consumedCorrectionVector.sqrMagnitude < _correctionVector.sqrMagnitude) 
        {
            Vector3 correctionLeft = _correctionVector - _consumedCorrectionVector;
            float distanceThisFrame = (root.position - _lastRootPosition).magnitude;
            Vector3 correctionThisFrame = (distanceThisFrame / trajectoryCorrectionDistance) * _correctionVector;
            correctionThisFrame  = Vector3.ClampMagnitude(correctionThisFrame,correctionLeft.magnitude);
            _consumedCorrectionVector += correctionThisFrame;

            if (Mathf.Abs(_consumedCorrectionVector.sqrMagnitude - _correctionVector.sqrMagnitude) < Mathf.Epsilon)
            {
                _hasTrajectoryCorrected = true;
            }

            transform.position += correctionThisFrame;
        }


        //»÷ÖÐÅÐ¶¨
        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = Mathf.Infinity;
        bool foundHit = false;


        Vector3 displacementSinceLastFrame = tip.position - _lastRootPosition;
        RaycastHit[] hits = Physics.SphereCastAll(_lastRootPosition, radius,
            displacementSinceLastFrame.normalized,
            displacementSinceLastFrame.magnitude,
            hittableLayers, QueryTriggerInteraction.Collide);

        foreach (RaycastHit hit in hits)
        {
            if (IsHitValid(hit) && hit.distance < closestHit.distance)
            {
                closestHit = hit;
                foundHit=true;
            }
        }

        if(foundHit)
        {
            if(closestHit.distance<=0)
            {
                closestHit.point = root.position;
                closestHit.normal = -transform.forward;
            }

            OnHit(closestHit.point,closestHit.normal);
        }
    }

    private bool IsHitValid(RaycastHit hit)
    {
        if (hit.collider.isTrigger)
        {
            return false;
        }
        return true;
    }

    private void OnHit(Vector3 point,Vector3 normal)
    {
        if(impactVFX!=null)
        {
            GameObject impactVFXInstance = Instantiate(impactVFX, point + normal * impactVFXSpawnOffset,
                Quaternion.LookRotation(normal));

            if (impactVFXLifeTime > 0)
            {
                Destroy(impactVFXInstance, impactVFXLifeTime);
            }
        }

        //Debug.Log("Hit!!!!!!!!!!");

        Destroy(gameObject);
    }
}
