#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BlindMonkWOne : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {40, 80, 120, 160, 200};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float shieldAbsorb;
            float bonusAP;
            float bonusAP80;
            float nextBuffVars_ShieldAbsorb;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            Vector3 ownerPos;
            Particle p3; // UNUSED
            Vector3 targetPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            shieldAbsorb = this.effect0[level];
            bonusAP = GetFlatMagicDamageMod(owner);
            bonusAP80 = bonusAP * 0.8f;
            shieldAbsorb += bonusAP80;
            nextBuffVars_ShieldAbsorb = shieldAbsorb;
            if(target != attacker)
            {
                ownerPos = GetUnitPosition(owner);
                SpellEffectCreate(out p3, out _, "blindMonk_W_cas_01.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false, false);
                targetPos = GetUnitPosition(target);
                moveSpeed = GetMovementSpeed(owner);
                dashSpeed = moveSpeed + 1350;
                distance = DistanceBetweenObjects("Owner", "Target");
                nextBuffVars_TargetPos = targetPos;
                nextBuffVars_Distance = distance;
                nextBuffVars_dashSpeed = dashSpeed;
                AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkWOneDash(nextBuffVars_ShieldAbsorb, nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.BlindMonkWManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if(GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.Destealth(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BlindMonkWOneShield(nextBuffVars_ShieldAbsorb), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.BlindMonkWManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}