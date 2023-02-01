#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RunePrison : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Rune Prison",
            BuffTextureName = "Ryze_PowerOverwhelming.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        Particle asdf1;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out this.asdf1, out _, "RunePrison_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SpellEffectRemove(this.asdf1);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class RunePrison : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {1, 1.25f, 1.5f, 1.75f, 2};
        int[] effect1 = {60, 95, 130, 165, 200};
        float[] effect2 = {0.5f, 0.5f, 0.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            TeamId teamID;
            float pAR;
            float aoEDamage;
            float manaDamage;
            float totalDamage;
            Particle part; // UNUSED
            AddBuff(attacker, target, new Buffs.RunePrison(), 1, 1, this.effect0[level], BuffAddType.RENEW_EXISTING, BuffType.CHARM, 0, true, false, false);
            baseDamage = this.effect1[level];
            teamID = GetTeamID(attacker);
            pAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            aoEDamage = this.effect2[level];
            manaDamage = pAR * 0.05f;
            totalDamage = manaDamage + baseDamage;
            aoEDamage *= totalDamage;
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DesperatePower)) > 0)
            {
                SpellEffectCreate(out part, out _, "DesperatePower_aoe.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(target != unit)
                    {
                        SpellEffectCreate(out part, out _, "ManaLeach_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                        ApplyDamage(attacker, unit, aoEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}