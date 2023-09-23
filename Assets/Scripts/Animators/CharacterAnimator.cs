using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] StarterAssetsInputs input;
    [Space]
    [SerializeField] Transform[] characterHands;
    [SerializeField] Transform[] weapons;
    [SerializeField] Transform weaponsScabbard;
    [SerializeField] float SheathDuration=0.5f;
    int currentWeaponSelectedIndex;

    private void Start()
    {
        animator = GetComponent<Animator>();
        input.Attack += OnAttackPressed;
        input.Draw += OnDrawPressed;
    }
    private string AttackAnimatorParameter = "Attack";
    private void OnAttackPressed()
    {
        if (animatorInAction)
            return;

        if (IsWeaponDrawn)
            Attack();
        else
            DrawWeapon();
    }
    private bool IsWeaponDrawn;
    private bool animatorInAction;
    private string DrawAnimatorParameter = "Draw";
    private string SheathAnimatorParameter = "Sheath";
    private void OnDrawPressed()
    {
        if (animatorInAction) 
            return;

        if (IsWeaponDrawn)
            SheathWeapon();
        else
            DrawWeapon();
    }
    private void Attack()
    {
        animatorInAction = true;
        animator.SetTrigger(AttackAnimatorParameter);
    }
    private void DrawWeapon()
    {
        animatorInAction = true;
        animator.SetTrigger(DrawAnimatorParameter);
        IsWeaponDrawn = true;
    }
    private void SheathWeapon()
    {
        animatorInAction = true;
        animator.SetTrigger(SheathAnimatorParameter);
        IsWeaponDrawn = false;
    }
    public void MoveWeaponToCombatSlot(int handIndex)
    {
        EnterCombatMode(handIndex+1);
        weapons[handIndex].SetParent(characterHands[handIndex]);
        ResetWeaponPosition(handIndex);
    }
    public void MoveWeaponToIdleSlot(int handIndex)
    {
        ExitCombatMode(handIndex+1);
        weapons[handIndex].SetParent(weaponsScabbard);
        ResetWeaponPosition(handIndex);
    }
    public void AnimationFinished()
    {
        animatorInAction = false;
    }
    private void EnterCombatMode(int layerIndex)
    {
        ChangeLayerWeight(layerIndex, 1);
    }
    private void ExitCombatMode(int layerIndex)
    {
        ChangeLayerWeight(layerIndex, 0);
    }
    private void ChangeLayerWeight(int layerIndex,float targetWeight)
    {
        LeanTween.value(gameObject, 1, 0, SheathDuration).setEaseInOutSine().setOnUpdate((float value) =>
        animator.SetLayerWeight(layerIndex, targetWeight));
    }
    private void ResetWeaponPosition(int handIndex)
    {
        LeanTween.moveLocal(weapons[handIndex].gameObject, Vector3.zero, SheathDuration);
        LeanTween.rotateLocal(weapons[handIndex].gameObject, Vector3.zero, SheathDuration);
    }
}
