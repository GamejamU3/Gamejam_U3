using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class SO_Weapons : ScriptableObject
{
    public string weapoName;
    public string weaponDescription;
    public float weaponDamage;
    public int ammoCapacity;
    public float reloadTime;
    public float fireDelay;
    public float fireSpeed;
    public float attackRange;
}
