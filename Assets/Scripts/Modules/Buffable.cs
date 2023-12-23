using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffable : Damageable
{
    public enum StatusEffect
    {
        // ================================== BUFFS ==================================

        /// <summary> Does not take any damage. </summary>
        INVINCIBLE,
        /// <summary> Increased move speed/attack speed. Reduced cooldowns. </summary>
        HASTE,
        /// <summary> Walks through enemies and non-static obstacles. </summary>
        PHASING,
        /// <summary> Cannot be reduced to below 1 health. </summary>
        UNKILLABLE,
        /// <summary> Healed a small amount over time. </summary>
        REGEN,
        /// <summary> Not affected by the motion of other objects or crowd control. </summary>
        UNSTOPPABLE,

        // ================================== DEBUFFS start at 100 ==================================

        /// <summary> Takes normal damage over time. Ignores shields. Stackable.</summary>
        BLEEDING = 100,
        /// <summary> Takes fire damage over time. Stackable. </summary>
        BURNING,
        /// <summary> Cannot move or attack. </summary>
        FROZEN,
        /// <summary> Moves slow. Takes more damage against cold attacks? </summary>
        CHILLED,
        /// <summary> Takes poison damage over time. Does not stack. Ignores shields. Lasts a long time. </summary>
        POISONED,
        /// <summary> Cannot move or act. Short duration. </summary>
        STUNNED,
        /// <summary> Cannot move, but can still act. </summary>
        ROOTED
    }

    public class Status
    {
        public StatusEffect effect;
        public float duration;
        public int stacks;

        public Status(StatusEffect effect, float duration, int stacks)
        {
            this.effect = effect;
            this.duration = duration;
            this.stacks = stacks;
        }
    }

    public List<Status> EffectList = new();


    public void AddEffect(StatusEffect effect, float duration, int stacks)
    {
        Status newStatus = new(effect, duration, stacks);
        EffectList.Add(newStatus);
    }
    // TODO: CURRENTLY ON: Make a global timer that ticks effects OR run each effect on its own timer...?

    public static bool IsBuff(StatusEffect effect) => (int)effect < 100;
    public static bool IsDebuff(StatusEffect effect) => (int)effect >= 100;
    // Start is called before the first frame update


    public void WhatAmI(StatusEffect e)
    {
        Debug.Log($"IS {e} A BUFF?: {IsBuff(e)}");
        Debug.Log($"IS {e} A DEBUFF?: {IsDebuff(e)}");
    }
}
