#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneShroudofDarkness : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "C_BUFFBONE_GLB_CENTER_LOC", "Head", "", "", },
            AutoBuffActivateEffect = new[]{ "nocturne_shroudofDarkness_shield.troy", "nocturne_shroudofDarkness_shield_cas_02.troy", "nocturne_shroudofDarkness_shield_cas_ground.troy", "", },
            BuffName = "NocturneShroudofDarknessShield",
            BuffTextureName = "Nocturne_ShroudofDarkness.dds",
        };
        bool willRemove;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            Particle ar; // UNUSED
            if(this.willRemove)
            {
                if(owner.Team != attacker.Team)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else if(duration == 37037)
            {
                SpellEffectCreate(out ar, out _, "nocturne_shroud_deactivateTrigger.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
                this.willRemove = true;
                returnValue = false;
            }
            return returnValue;
        }
        public override void OnUpdateStats()
        {
            if(this.willRemove)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.NocturneShroudofDarknessBuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.NocturneShroudofDarkness), (ObjAIBase)owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.willRemove)
            {
                damageAmount = 0;
            }
        }
        public override void OnBeingSpellHit(SpellScriptMetaData spellVars)
        {
            bool isAttack;
            Particle ar; // UNUSED
            SetTriggerUnit(attacker);
            owner = SetBuffCasterUnit();
            if(owner.Team != attacker.Team)
            {
                isAttack = GetIsAttackOverride();
                if(!isAttack)
                {
                    if(!spellVars.DoesntBreakShields)
                    {
                        this.willRemove = true;
                        SpellEffectCreate(out ar, out _, "nocturne_shroud_deactivateTrigger.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
                    }
                    else if(spellVars.DoesntBreakShields)
                    {
                    }
                    else if(!spellVars.DoesntTriggerSpellCasts)
                    {
                        this.willRemove = true;
                        SpellEffectCreate(out ar, out _, "nocturne_shroud_deactivateTrigger.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class NocturneShroudofDarkness : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        bool willRemove; // UNUSED
        public override void SelfExecute()
        {
            this.willRemove = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.NocturneShroudofDarkness(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell2", 1, owner, false, true, true);
        }
    }
}