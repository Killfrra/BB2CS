#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SkarnerVirulentSlash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle partname; // UNUSED
        int[] effect0 = {25, 40, 55, 70, 85};
        int[] effect1 = {24, 36, 48, 60, 72};
        float[] effect2 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void SelfExecute()
        {
            float baseDamage;
            float procDamage;
            float nextBuffVars_SlowPercent;
            float ratioVar; // UNUSED
            TeamId teamID;
            int count;
            bool championHit;
            baseDamage = this.effect0[level];
            procDamage = this.effect1[level];
            nextBuffVars_SlowPercent = this.effect2[level];
            ratioVar = 0.3f;
            teamID = GetTeamID(owner);
            PlayAnimation("Spell1", 0, owner, false, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.SkarnerVirulentSlash));
            if(count == 0)
            {
                SpellEffectCreate(out this.partname, out _, "Skarner_Crystal_Slash_Mini_Nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.partname, out _, "Skarner_Crystal_Slash_Buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            }
            championHit = false;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle temp; // UNUSED
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0.8f, false, false, attacker);
                championHit = true;
                if(count == 0)
                {
                    SpellEffectCreate(out temp, out _, "chogath_basic_attack_01.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                else
                {
                    ApplyDamage(attacker, unit, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                    SpellEffectCreate(out temp, out _, "Skarner_Crystal_Slash_Tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SkarnerVirulentSlashSlow(nextBuffVars_SlowPercent), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
            if(championHit)
            {
                AddBuff(attacker, attacker, new Buffs.SkarnerVirulentSlash(), 1, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                if(count == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.SkarnerVirulentSlashEnergy1(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class SkarnerVirulentSlash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "SkarnerVirulentSlash",
            BuffTextureName = "SkarnerVirulentSlash.dds",
            SpellFXOverrideSkins = new[]{ "ReefMalphite", },
        };
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.SkarnerVirulentSlashEnergy1));
            SpellBuffClear(owner, nameof(Buffs.SkarnerVirulentSlashEnergy2));
        }
    }
}