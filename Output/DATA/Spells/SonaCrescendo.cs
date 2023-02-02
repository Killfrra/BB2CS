﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SonaCrescendo : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {50, 83.3f, 116.6f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageAmount;
            nextBuffVars_DamageAmount = this.effect0[level];
            BreakSpellShields(target);
            AddBuff(attacker, target, new Buffs.SonaCrescendo(nextBuffVars_DamageAmount), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SonaCrescendo : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "head", },
            AutoBuffActivateEffect = new[]{ "SonaCrescendo_buf.troy", "Stun_glb.troy", },
            BuffName = "SonaCrescendo",
            BuffTextureName = "Sona_Crescendo.dds",
            PopupMessage = new[]{ "game_floatingtext_Stunned", },
            SpellFXOverrideSkins = new[]{ "GuqinSona", },
        };
        float damageAmount;
        float lastTimeExecuted;
        public SonaCrescendo(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            SetCanCast(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            //RequireVar(this.damageAmount);
            ApplyAssistMarker(attacker, owner, 10);
            OverrideAnimation("Idle1", "Dance", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage(attacker, owner, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.266f, 1, false, false, attacker);
            SetCanCast(owner, true);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            ClearOverrideAnimation("Idle1", owner);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.75f, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.266f, 1, false, false, attacker);
            }
        }
    }
}