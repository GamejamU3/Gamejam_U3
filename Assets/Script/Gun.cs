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

    bool hasGun=false;
    [SerializeField] Animator gunAnim;
    public bool gunPlace=false;


    float currentAmmo;
    private bool haveAmmo = true;
    private bool isReloading = false;
    private float bloodTime = 1f;

    [Header("Enemy")]
    public EnemyType enemyType;

    [Header("VFX")]
    [SerializeField] GameObject bloodVfx;
    [SerializeField] GameObject muzzleFlashVfx;

    [Header("Camera")]
    public Camera _camera;

    [Header("Recoil")]
    [SerializeField] private float recoilAmount = 1f; // Geri tepme miktarı
    [SerializeField] private float maxRecoil = 3f; // Maksimum geri tepme miktarı
    [SerializeField] private float recoilRecoverySpeed = 2f; // Geri tepmenin toparlanma hızı


    [Header("Pick Up")]
    [SerializeField] float pickupDistance;
    public float throwForce = 10f;
    private Rigidbody currentObject;

private float currentRecoil = 0f; // Mevcut geri tepme miktarı

    private void Start()
    {
        currentAmmo = currentWeapon.ammoCapacity;
    }

    private void Update()
    {
        //Debug.DrawRay(_camera.transform.position, _camera.transform.forward * currentWeapon.attackRange, Color.green, 0.1f);
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * pickupDistance, Color.red, 0.1f);


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
                PickupObject();
            }

            if (currentObject != null)
            {
                Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * pickupDistance;
                currentObject.transform.position = newPosition;

                // Oyuncu, boşluk çubuğuna tekrar basarak nesneyi bırakır.
                if (Input.GetButtonDown("Fire1"))
                {
                    DropObject();
                }
            }
        }
        
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

        if(Physics.Raycast(_camera.transform.position + recoil ,_camera.transform.forward,out hit, currentWeapon.attackRange))
        {   
            if (hit.transform.CompareTag("Enemy"))
            {
                GameObject blood = Instantiate(bloodVfx,hit.transform); // or SetActive(true)
                //yield return new WaitForSeconds(bloodTime);
                //Destroy(blood); //or SetActive(false);
            } 
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

    void PickupObject()
    {

        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, pickupDistance))
        {   
            Debug.Log(hit.transform.gameObject.tag);
            if (hit.transform.CompareTag("Moveable"))
            {
                currentObject = hit.collider.gameObject.GetComponent<Rigidbody>();
                currentObject.useGravity = false;
            }
        }
    }

    void DropObject()
    {
        currentObject.useGravity = true;
        currentObject.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
        currentObject = null;
    }
}
