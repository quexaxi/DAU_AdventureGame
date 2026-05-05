using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class GrenadierSoundController : MonoBehaviour
{
    public AK.Wwise.Event deathEvent;
    public AK.Wwise.Event damageEvent;
    public AK.Wwise.Event footstepEvent;
    public AK.Wwise.Event throwEvent;
    public AK.Wwise.Event punchEvent;
    public AK.Wwise.Event activateShieldEvent;
    public AK.Wwise.Event shootEvent;
    public AK.Wwise.Event attackEvent;
    public AK.Wwise.Event endAttackEvent;
    public AK.Wwise.Event hitEvent;
    public AK.Wwise.Event rechargeEvent;



    public void AnimDeath()
    {
        deathEvent.Post(gameObject);
    }

    public void AnimDamage()
    {
        damageEvent.Post(gameObject);
    }

    public void GrenadierWalk()
    {
        footstepEvent.Post(gameObject);
    }

    public void AnimThrow()
    {
        throwEvent.Post(gameObject);
    }

    public void AnimPunch()
    {
        punchEvent.Post(gameObject);
    }

    public void ActivateShield()
    {
        activateShieldEvent.Post(gameObject);
    }

    public void AnimShoot()
    {
        shootEvent.Post(gameObject);
    }

    public void StartAttack()
    {
        attackEvent.Post(gameObject);
    }

    public void EndAttack()
    {
        endAttackEvent.Post(gameObject);
    }

    public void AnimHit04()
    {
        hitEvent.Post(gameObject);
    }

    public void RechargeGrenade()
    {
        rechargeEvent.Post(gameObject);
    }

}
