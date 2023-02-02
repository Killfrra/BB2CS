#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XenZhaoPuncture : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoPuncture",
            BuffTextureName = "XinZhao_TirelessWarrior.dds",
        };
        float healAmount;
        int[] effect0 = {25, 25, 30, 30, 35, 35, 40, 40, 45, 45, 50, 50, 55, 55, 60, 60, 65, 65};
        int[] effect1 = {25, 25, 30, 30, 35, 35, 40, 40, 45, 45, 50, 50, 55, 55, 60, 60, 65, 65};
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    charVars.ComboCounter++;
                    if(charVars.ComboCounter >= 3)
                    {
                        int level;
                        Particle num; // UNUSED
                        charVars.ComboCounter = 0;
                        level = GetLevel(owner);
                        this.healAmount = this.effect0[level];
                        IncHealth(owner, this.healAmount, owner);
                        SpellEffectCreate(out num, out _, "xenZiou_heal_passive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
                    }
                }
            }
        }
        public override void OnUpdateActions()
        {
            int level;
            level = GetLevel(owner);
            this.healAmount = this.effect1[level];
            SetBuffToolTipVar(1, this.healAmount);
        }
    }
}