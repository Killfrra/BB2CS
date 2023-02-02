#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class IreliaHitenStyle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.IreliaHitenStyle), (ObjAIBase)owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, owner, new Buffs.IreliaHitenStyleCharged(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class IreliaHitenStyle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", "", },
            BuffName = "IreliaHitenStyle",
            BuffTextureName = "Irelia_HitenStyleReady.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle ireliaHitenStyle1;
        Particle ireliaHitenStyle2;
        int[] effect0 = {10, 14, 18, 22, 26};
        public override void OnActivate()
        {
            TeamId ireliaTeamID;
            OverrideAnimation("Attack1", "Attack1b", owner);
            OverrideAnimation("Attack2", "Attack2b", owner);
            OverrideAnimation("Crit", "Critb", owner);
            OverrideAnimation("Idle1", "Idle1b", owner);
            OverrideAnimation("Run", "Runb", owner);
            ireliaTeamID = GetTeamID(owner);
            SpellEffectCreate(out this.ireliaHitenStyle1, out _, "irelia_hitenStyle_passive.troy", default, ireliaTeamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false);
            SpellEffectCreate(out this.ireliaHitenStyle2, out _, "irelia_hitenStlye_passive_glow.troy", default, ireliaTeamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.ireliaHitenStyle1);
            SpellEffectRemove(this.ireliaHitenStyle2);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float healthRestoration;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            healthRestoration = this.effect0[level];
            IncHealth(owner, healthRestoration, owner);
        }
    }
}