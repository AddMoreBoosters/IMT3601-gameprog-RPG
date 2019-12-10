using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum ProjectileTypes
    {
        Bullet,
        Rocket
    }
    public string weaponName;
    public string soundName;
    public int projectile;
    public float fireRateRPM;
    public float cooldown;
    public float noiseTravelDistance;
    public float spreadDegrees;
}
