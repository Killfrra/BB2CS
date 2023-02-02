#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SpellShield : BBSpellScript
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
            AddBuff((ObjAIBase)owner, owner, new Buffs.SpellShield(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class SpellShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "SpellBlock_eff.troy", },
            BuffName = "Spell Shield",
            BuffTextureName = "Sivir_SpellBlock.dds",
        };
        bool willRemove;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
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
                Particle ar; // UNUSED
                SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                this.willRemove = true;
                returnValue = false;
            }
            return returnValue;
        }
        public override void OnUpdateStats()
        {
            if(this.willRemove)
            {
                IncPAR(owner, 150);
                SpellBuffRemoveCurrent(owner);
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
            SetTriggerUnit(attacker);
            owner = SetBuffCasterUnit();
            if(owner.Team != attacker.Team)
            {
                bool isAttack;
                isAttack = GetIsAttackOverride();
                if(!isAttack)
                {
                    Particle ar; // UNUSED
                    if(!spellVars.DoesntBreakShields)
                    {
                        this.willRemove = true;
                        SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    }
                    else if(spellVars.DoesntBreakShields)
                    {
                    }
                    else if(!spellVars.DoesntTriggerSpellCasts)
                    {
                        this.willRemove = true;
                        SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    }
                }
            }
        }
    }
}