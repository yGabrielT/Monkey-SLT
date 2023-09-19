using UnityEngine;

[CreateAssetMenu(fileName = "SOGun", menuName = "ScriptableObjects/SOGun", order = 0)]
public class SOGun : ScriptableObject {
    
    public GameObject BulletPrefab;
    public Sprite GunSprite;
    public float BulletSpeed = 10f;
    public float DestroyTimer = 3f;
    public float FireRate = .3f;

    public bool isShotgun = false;

    public float ShotgunSpread = 25;

}
