#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenLightningRushDamage : BBBuffScript
    {
        float rushDamage;
        public KennenLightningRushDamage(float rushDamage = default)
        {
            this.rushDamage = rushDamage;
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.rushDamage);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            float aPValue;
            float aPMod;
            float totalDamage;
            float minionDamage;
            Particle kennenss; // UNUSED
            teamID = GetTeamID(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.KennenLightningRushMarker)) == 0)
                {
                    aPValue = GetFlatMagicDamageMod(owner);
                    aPMod = aPValue * 0.6f;
                    totalDamage = this.rushDamage + aPMod;
                    minionDamage = totalDamage / 2;
                    AddBuff(attacker, unit, new Buffs.KennenLightningRushMarker(), 1, 1, 2.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    if(unit is Champion)
                    {
                        AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                        SpellEffectCreate(out kennenss, out _, "kennen_lr_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, default, default, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage(attacker, unit, minionDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                        SpellEffectCreate(out kennenss, out _, "kennen_lr_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, default, default, false, false);
                    }
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KennenLREnergy)) == 0)
                    {
                        IncPAR(owner, 40, PrimaryAbilityResourceType.Energy);
                        AddBuff((ObjAIBase)owner, owner, new Buffs.KennenLREnergy(), 1, 1, 2.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}