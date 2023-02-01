#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaAriaofPerseveranceUpgrade : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int levelDamage;
            float bonusDamage;
            float totalDamage;
            float nextBuffVars_TotalDamage;
            float attackDamage;
            levelDamage = GetLevel(owner);
            bonusDamage = levelDamage * 9;
            totalDamage = bonusDamage + 14;
            nextBuffVars_TotalDamage = totalDamage;
            attackDamage = GetTotalAttackDamage(owner);
            ApplyDamage((ObjAIBase)owner, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.SonaPowerChordDebuff(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
            AddBuff(attacker, target, new Buffs.SonaAriaPCDeathRecapFix(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaPowerChord), (ObjAIBase)owner, 0);
        }
    }
}