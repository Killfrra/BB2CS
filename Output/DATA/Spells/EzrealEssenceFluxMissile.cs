#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EzrealEssenceFluxMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
            SpellFXOverrideSkins = new[]{ "CyberEzreal", },
        };
        float attackSpeedModNegative;
        public EzrealEssenceFluxMissile(float attackSpeedModNegative = default)
        {
            this.attackSpeedModNegative = attackSpeedModNegative;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedModNegative);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeedModNegative);
        }
    }
}
namespace Spells
{
    public class EzrealEssenceFluxMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {0, 0, 0, 0, 0};
        float[] effect1 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        float[] effect2 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        int[] effect3 = {80, 130, 180, 230, 280};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_AttackSpeedMod;
            float nextBuffVars_AttackSpeedModNegative;
            float abilityPower;
            float abilityPowerMod; // UNUSED
            TeamId casterID;
            TeamId casterID2;
            Particle asdf; // UNUSED
            float attackSpeedMod;
            AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + this.effect0[level], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            abilityPower = GetFlatMagicDamageMod(attacker);
            abilityPowerMod = abilityPower * 0.7f;
            casterID = GetTeamID(attacker);
            casterID2 = GetTeamID(target);
            SpellEffectCreate(out asdf, out _, "Ezreal_essenceflux_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "root", default, target, default, default, true, false, false, false, false);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackSpeedMod = this.effect1[level];
            nextBuffVars_AttackSpeedMod = this.effect2[level];
            if(casterID == casterID2)
            {
                ApplyAssistMarker(attacker, target, 10);
                AddBuff(attacker, target, new Buffs.EzrealEssenceFlux(nextBuffVars_AttackSpeedMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                nextBuffVars_AttackSpeedModNegative = attackSpeedMod * -1;
                BreakSpellShields(target);
                ApplyDamage(attacker, target, this.effect3[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                AddBuff(attacker, target, new Buffs.EzrealEssenceFluxMissile(nextBuffVars_AttackSpeedModNegative), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}