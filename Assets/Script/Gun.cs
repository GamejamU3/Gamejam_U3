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

private float currentRecoil = 0f; // Mevcut geri tepme miktarı

    private void Start()
    {
        currentAmmo = currentWeapon.ammoCapacity;
    }

    private void Update()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * currentWeapon.attackRange, Color.green, 0.1f);
        /** Tek tek sıkma
        if (Input.GetButtonDown("Fire1"))
        {  
            if(haveAmmo){
                currentRecoil += recoilAmount;
                currentRecoil = Mathf.Clamp(currentRecoil, 0f, maxRecoil);
                StartCoroutine(nameof(Shoot));
            }else{
                if(isReloading==false) StartCoroutine(nameof(Reload));
            }
        }

        currentRecoil = Mathf.Lerp(currentRecoil, 0f, Time.deltaTime * recoilRecoverySpeed);
        // Silahı geri tepme miktarına göre döndürme
        transform.localRotation = Quaternion.Euler(-currentRecoil, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        **/

        if (Input.GetButtonDown("Fire1")) {
            isFiring = true;
        }
        if (Input.GetButtonUp("Fire1")) {
            isFiring = false;
        }

        currentRecoil = Mathf.Lerp(currentRecoil, 0f, Time.deltaTime * recoilRecoverySpeed);
            // Silahı geri tepme miktarına göre döndürme
        transform.localRotation = Quaternion.Euler(-currentRecoil, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }

    private void FixedUpdate() {

        if(currentAmmo > 0){
            if (isFiring && Time.time > nextFireTime) {
                nextFireTime = Time.time + currentWeapon.fireDelay;
                currentRecoil += recoilAmount;
                currentRecoil = Mathf.Clamp(currentRecoil, 0f, maxRecoil);
                StartCoroutine(nameof(Shoot));
            }
        }else{
            if(isReloading==false) StartCoroutine(nameof(Reload));
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
}
