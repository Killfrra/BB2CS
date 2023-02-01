#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MissFortuneBullets : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {65, 95, 125, 185, 230};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            int count1;
            TeamId teamID;
            float baseDamage;
            float totalDamage;
            float baseAtkDmg;
            float bonusDamage;
            float aPPreMod;
            float aPPostMod;
            float aDAPBonus;
            float finalDamage;
            Particle asdf; // UNUSED
            count = GetBuffCountFromAll(target, nameof(Buffs.MissfortuneBulletHolder));
            if(count <= 7)
            {
                count1 = GetBuffCountFromAll(target, nameof(Buffs.MissFortuneWaveHold));
                if(count1 < 1)
                {
                    teamID = GetTeamID(owner);
                    AddBuff(attacker, target, new Buffs.MissFortuneWaveHold(), 2, 1, 0.05f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.MissfortuneBulletHolder(), 9, 1, 6, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    baseDamage = this.effect0[level];
                    totalDamage = GetTotalAttackDamage(owner);
                    baseAtkDmg = GetBaseAttackDamage(owner);
                    bonusDamage = totalDamage - baseAtkDmg;
                    bonusDamage *= 0.45f;
                    aPPreMod = GetFlatMagicDamageMod(owner);
                    aPPostMod = 0.2f * aPPreMod;
                    aDAPBonus = bonusDamage + aPPostMod;
                    finalDamage = baseDamage + aDAPBonus;
                    ApplyDamage((ObjAIBase)owner, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    SpellEffectCreate(out asdf, out _, "missFortune_bulletTime_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
            }
        }
    }
}