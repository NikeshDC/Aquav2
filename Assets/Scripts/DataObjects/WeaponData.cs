using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "NewWeapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _firingStrength;
    public float firingStrength => _firingStrength;

    [SerializeField] private float _reloadTime;
    public float reloadTime => _reloadTime;

    [SerializeField] private int _projectileDamage;
    public int projectileDamage => _projectileDamage;
}
