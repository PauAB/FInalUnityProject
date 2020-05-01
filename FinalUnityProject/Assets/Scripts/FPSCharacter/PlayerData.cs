using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroEnum
{
    DEFAULT
}

[CreateAssetMenu(fileName = "Create", menuName = "PlayerData/Create", order = 0)]
public class PlayerData : ScriptableObject
{
    public float ReloadTime;
    public float TotalBullets;
    public float ShootCooldown;
    public float HP;
    public GameObject Image;
    public GameObject ProjectileType;
    public GameObject[] Abilities;
}
