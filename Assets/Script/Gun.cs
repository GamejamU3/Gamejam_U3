using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Weapon")]
    [Tooltip("Current weapon")]
    public SO_Weapons currentWeapon;

    private bool canFire = true;

    bool isFiring = false;
    float nextFireTime = 0f;

    public bool hasGun=false;
    [SerializeField] Animator gunAnim;
    public bool gunPlace=false;


    float currentAmmo;
    private bool haveAmmo = true;
    private bool isReloading = false;
    private float bloodTime = 0.3f;

    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;


    [SerializeField] float pickupRange;
    [SerializeField] float pickupForce = 150f;
    private Rigidbody currentObject;



    [Header("Enemy")]
    public EnemyType enemyType;

    [Header("VFX")]
    [SerializeField] GameObject impactVfx;
    [SerializeField] ParticleSystem muzzleFlashVfx;

    [Header("Camera")]
    public Camera _camera;

    [Header("Recoil")]
    [SerializeField] private float recoilAmount = 1f; // Geri tepme miktarı
    [SerializeField] private float maxRecoil = 3f; // Maksimum geri tepme miktarı
    [SerializeField] private float recoilRecoverySpeed = 2f; // Geri tepmenin toparlanma hızı


private float currentRecoil = 0f; // Mevcut geri tepme miktarı

    private void Start()
    {
        currentAmmo = currentWeapon.ammoCapacity;
    }

    private void Update()
    {
        //Debug.DrawRay(_camera.transform.position, _camera.transform.forward * currentWeapon.attackRange, Color.green, 0.1f);
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * pickupRange, Color.red, 0.1f);


        if (Input.GetKeyDown(KeyCode.Alpha1)){
            hasGun = !hasGun;
            gunAnim.SetBool("HasGun",hasGun);
        }

        if(hasGun){
            if (Input.GetButtonDown("Fire1")) {
                isFiring = true;
            }
            if (Input.GetButtonUp("Fire1")) {
                isFiring = false;
            }
            currentRecoil = Mathf.Lerp(currentRecoil, 0f, Time.deltaTime * recoilRecoverySpeed);
            // Silahı geri tepme miktarına göre döndürme
            transform.localRotation = Quaternion.Euler(-currentRecoil, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }else{

            if(Input.GetButtonDown("Fire1")){
                if (heldObj == null)
                {
                    RaycastHit holdHit;
                    if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out holdHit,pickupRange))
                    {
                        if (holdHit.transform.CompareTag("Moveable"))
                        {
                            PickedObject(holdHit.transform.gameObject);
                        }
                        
                    }
                }
                else
                {
                    DropObject();
                }
            }

            if (heldObj != null)
            {
                MoveObject();
            }

         
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickedObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity= false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    void DropObject()
    {
       
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObj.transform.parent = null;
        heldObj = null;
       
    }



    private void FixedUpdate() {

        if (gunPlace)
        {
            if (currentAmmo > 0)
            {
                if (isFiring && Time.time > nextFireTime)
                {
                    nextFireTime = Time.time + currentWeapon.fireDelay;
                    currentRecoil += recoilAmount;
                    currentRecoil = Mathf.Clamp(currentRecoil, 0f, maxRecoil);
                    StartCoroutine(nameof(Shoot));
                }
            }
            else
            {
                if (isReloading == false) StartCoroutine(nameof(Reload));
            }
        }
    }

    private IEnumerator Shoot()
    {
        canFire = false;
        RaycastHit hit;
        currentAmmo--;
        Debug.Log(currentAmmo);

        if(currentAmmo <= 0) haveAmmo=false;

        Vector3 recoil = new Vector3(currentRecoil/5,0f,0f);
        //Debug.Log("Camera position:" + _camera.transform.position + ", Recoil Position:" + (_camera.transform.position + recoil));

        if(Physics.Raycast(_camera.transform.position ,_camera.transform.forward,out hit, currentWeapon.attackRange))
        {
            Debug.Log(hit.transform.tag);

            if (hit.transform.CompareTag("Enemy"))
            {
                
                hit.transform.gameObject.GetComponent<BotTask>().die();
                
            }else if (hit.transform.CompareTag("ParkourButton"))
            {
                hit.transform.gameObject.GetComponent<ParkourButton>().ChangePosition();
            }
            else if(hit.transform.CompareTag("Door"))
            {
                hit.transform.gameObject.GetComponent<DoorTask>().open();
            }
            else if (hit.transform.CompareTag("Button"))
            {
                hit.transform.gameObject.GetComponent<ButtonMng>().isaretle();
            }

            muzzleFlashVfx.Emit(1);
            GameObject blood = Instantiate(impactVfx, hit.point, Quaternion.LookRotation(hit.normal));
            yield return new WaitForSeconds(bloodTime);
            Destroy(blood);

        }
        yield return new WaitForSeconds(currentWeapon.fireDelay);
        canFire = true;
    }

    private IEnumerator Reload()
    {
        Debug.Log("Reloading..");
        isReloading = true;
        haveAmmo = false;
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        Debug.Log("Reloaded");
        currentAmmo = currentWeapon.ammoCapacity;
        isReloading=false;
        haveAmmo = true;
    }
}
