#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickRavenous : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = {0.4f, 0.4f, 0.4f, 0.4f, 0.4f};
        int[] effect1 = {55, 85, 115, 145, 175};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DrainPercent;
            float distance;
            Vector3 targetPos;
            float baseDamage;
            float bonusAD;
            float damageToDeal;
            TeamId teamID;
            Particle lifestealEffect; // UNUSED
            Particle hitEffect; // UNUSED
            bool nextBuffVars_DrainedBool;
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonRavenous)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.YorickSummonRavenous));
            }
            AddBuff((ObjAIBase)owner, target, new Buffs.YorickRavenousPrimaryTarget(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            BreakSpellShields(target);
            distance = DistanceBetweenObjects("Owner", "Target");
            distance += 250;
            targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            nextBuffVars_DrainPercent = this.effect0[level];
            nextBuffVars_DrainedBool = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            baseDamage = this.effect1[level];
            bonusAD = GetFlatPhysicalDamageMod(owner);
            damageToDeal = baseDamage + bonusAD;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out lifestealEffect, out _, "yorick_ravenousGhoul_activeHeal.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            SpellEffectCreate(out hitEffect, out _, "yorick_ravenousGhoul_cas_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
        }
    }
}