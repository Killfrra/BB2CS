#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyDiplomaticImmunity : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "",
            BuffName = "PoppyDiplomaticImmunity",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
        };
        Particle particle;
        Particle particle2;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.PoppyDITarget)) > 0)
                {
                    returnValue = true;
                }
                else
                {
                    if(type == BuffType.COMBAT_ENCHANCER)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            ObjAIBase caster;
            teamOfOwner = GetTeamID(owner);
            caster = SetBuffCasterUnit();
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_BLUE, 500, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, owner, default, default, false);
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, default, true))
                {
                    if(unit == caster)
                    {
                        SpellEffectCreate(out this.particle2, out _, "DiplomaticImmunity_tar.troy", default, TeamId.TEAM_BLUE, 500, 0, TeamId.TEAM_PURPLE, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                    else
                    {
                        SpellEffectCreate(out this.particle2, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_BLUE, 500, 0, TeamId.TEAM_PURPLE, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                }
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_PURPLE, 500, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, owner, default, default, false);
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, default, true))
                {
                    if(unit == caster)
                    {
                        SpellEffectCreate(out this.particle2, out _, "DiplomaticImmunity_tar.troy", default, TeamId.TEAM_PURPLE, 500, 0, TeamId.TEAM_BLUE, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                    else
                    {
                        SpellEffectCreate(out this.particle2, out _, "DiplomaticImmunity_buf.troy", default, TeamId.TEAM_BLUE, 500, 0, TeamId.TEAM_BLUE, default, unit, true, owner, default, default, owner, default, default, false);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.PoppyDITarget)) > 0)
            {
            }
            else
            {
                damageAmount -= damageAmount;
            }
        }
    }
}
namespace Spells
{
    public class PoppyDiplomaticImmunity : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {6, 7, 8};
        int[] effect1 = {6, 7, 8};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, target, new Buffs.PoppyDITarget(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyDITargetDmg(), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}