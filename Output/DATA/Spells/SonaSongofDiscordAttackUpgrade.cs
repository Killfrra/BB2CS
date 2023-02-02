#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaSongofDiscordAttackUpgrade : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int levelDamage;
            float bonusDamage;
            float totalDamage;
            float nextBuffVars_TotalDamage;
            float attackDamage;
            float nextBuffVars_MoveSpeedMod;
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
                    nextBuffVars_MoveSpeedMod = -0.4f;
                    AddBuff((ObjAIBase)owner, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            AddBuff(attacker, target, new Buffs.SonaSongPCDeathRecapFix(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaPowerChord), (ObjAIBase)owner, 0);
        }
    }
}