#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DarkBindingMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "DarkBinding_tar.troy", "", },
            BuffName = "Dark Binding",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class DarkBindingMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 135, 190, 245, 300};
        float[] effect1 = {2, 2.25f, 2.5f, 2.75f, 3};
        float[] effect2 = {2, 2.25f, 2.5f, 2.75f, 3};
        float[] effect3 = {2, 2.25f, 2.5f, 2.75f, 3};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool isStealthed;
            float damageAmount;
            bool canSee;
            isStealthed = GetStealthed(target);
            damageAmount = this.effect0[level];
            if(!isStealthed)
            {
                DestroyMissile(missileNetworkID);
                ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.9f, 1, false, false);
                AddBuff(attacker, target, new Buffs.DarkBindingMissile(), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
            }
            else
            {
                if(target is Champion)
                {
                    DestroyMissile(missileNetworkID);
                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.9f, 1, false, false);
                    AddBuff(attacker, target, new Buffs.DarkBindingMissile(), 1, 1, this.effect2[level], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        DestroyMissile(missileNetworkID);
                        ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.9f, 1, false, false);
                        AddBuff(attacker, target, new Buffs.DarkBindingMissile(), 1, 1, this.effect3[level], BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                    }
                }
            }
        }
    }
}