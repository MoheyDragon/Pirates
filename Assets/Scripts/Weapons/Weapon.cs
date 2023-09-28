using UnityEngine;
public partial class Weapon :MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] float SheathDuration=.75f;
    [SerializeField] float weaponHitRadius = 3;
    [SerializeField] int damage=10;
    [SerializeField] WeaponPockets[] weaponPockets;
    Transform []weaponPocketsTransforms;
    GameObject[] weaponParts;
    public void SetupWeapon(CharacterAnimator characterAnimator)
    {
        AssignPockets(characterAnimator);
        CreateWeaponInstances();
    } 
    private void AssignPockets(CharacterAnimator characterAnimator)
    {
        weaponPocketsTransforms = new Transform[weaponPockets.Length];
        for (int i = 0; i < weaponPocketsTransforms.Length; i++)
        {
            weaponPocketsTransforms[i] = characterAnimator.GetPocketTransformByIndex((int)weaponPockets[i]);
        }
    }
    private void CreateWeaponInstances()
    {
        weaponParts = new GameObject[weaponPockets.Length];
        for (int i = 0; i < weaponParts.Length; i++)
        {
            weaponParts[i] = Instantiate(weaponPrefab, weaponPocketsTransforms[i]);
        }
    }
    public void HandleDamage()
    {
        Collider[] colliderArray = Physics.OverlapSphere(weaponParts[0].transform.position, weaponHitRadius);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                target.OnHit(damage);
            }
        }
    }
    public void Draw(Transform[] characterHands)
    {
        for (int i = 0; i < weaponParts.Length; i++)
        {
            weaponParts[i].transform.SetParent(characterHands[i]);
            ResetWeaponPosition(i);
        }
    }
    public void Sheath()
    {
        for (int i = 0; i < weaponParts.Length; i++)
        {
            weaponParts[i].transform.SetParent(weaponPocketsTransforms[i]);
            ResetWeaponPosition(i);
        }
    }
    private void ResetWeaponPosition(int handIndex)
    {
        LeanTween.moveLocal(weaponParts[handIndex], Vector3.zero, SheathDuration);
        LeanTween.rotateLocal(weaponParts[handIndex], Vector3.zero, SheathDuration);
    }
}
