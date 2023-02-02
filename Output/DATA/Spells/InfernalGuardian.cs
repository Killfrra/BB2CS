#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class InfernalGuardian : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {0, 400, 800, 600, 800};
        int[] effect1 = {0, 25, 50};
        int[] effect2 = {0, 20, 40};
        int[] effect3 = {0, 20, 40};
        int[] effect4 = {0, 400, 800};
        int[] effect5 = {35, 35, 35};
        int[] effect6 = {200, 325, 450};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID;
            int annieSkinID;
            Particle a; // UNUSED
            Pet other1;
            int nextBuffVars_ArmorAmount;
            float damageAmount;
            float aPPreMod;
            float aPPostMod;
            int count;
            int nextBuffVars_MRAmount;
            int nextBuffVars_HealthAmount;
            float nextBuffVars_FinalDamage;
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            annieSkinID = GetSkinID(owner);
            if(annieSkinID == 5)
            {
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out a, out _, "infernalguardian_tar_frost.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "infernalguardian_tar_frost.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
                }
            }
            else
            {
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out a, out _, "InfernalGuardian_tar.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "InfernalGuardian_tar.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
                }
            }
            other1 = SpawnPet("Tibbers", "AnnieTibbers", nameof(Buffs.InfernalGuardian), default, 45, targetPos, this.effect0[level], this.effect1[level]);
            nextBuffVars_ArmorAmount = this.effect2[level];
            nextBuffVars_MRAmount = this.effect3[level];
            nextBuffVars_HealthAmount = this.effect4[level];
            damageAmount = damageAmount + this.effect5[level];
            aPPreMod = GetFlatMagicDamageMod(owner);
            aPPostMod = 0.2f * aPPreMod;
            nextBuffVars_FinalDamage = damageAmount + aPPostMod;
            AddBuff(attacker, attacker, new Buffs.InfernalGuardianTimer(), 1, 1, 45, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.InfernalGuardianBurning(nextBuffVars_ArmorAmount, nextBuffVars_MRAmount, nextBuffVars_HealthAmount, nextBuffVars_FinalDamage), 1, 1, 45, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            charVars.SpellWillStun = false;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania_particle));
            if(count >= 1)
            {
                charVars.SpellWillStun = true;
                SpellBuffRemove(owner, nameof(Buffs.Pyromania_particle), (ObjAIBase)owner, 0);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pyromania(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(charVars.SpellWillStun)
            {
                ApplyStun(attacker, target, charVars.StunDuration);
            }
            ApplyDamage(attacker, target, this.effect6[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class InfernalGuardian : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsPetDurationBuff = true,
        };
    }
}