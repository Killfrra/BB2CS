#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasBarrelRollBoom : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            SpellToggleSlot = 1,
        };
        int skinID;
        object lifetime; // UNUSED
        Particle troyVar;
        float lifeTime; // UNUSED
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        int[] effect1 = {85, 135, 185, 235, 285};
        int[] effect2 = {3, 3, 3, 3, 3};
        int[] effect3 = {11, 10, 9, 8, 7};
        public GragasBarrelRollBoom(int skinID = default)
        {
            this.skinID = skinID;
        }
        public override void OnActivate()
        {
            TeamId teamofOwner;
            //RequireVar(this.lifetime);
            //RequireVar(this.skinID);
            this.lifetime;
            teamofOwner = GetTeamID(owner);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GragasBarrelRollToggle));
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            if(teamofOwner == TeamId.TEAM_BLUE)
            {
                if(this.skinID == 3)
                {
                    SpellEffectCreate(out this.troyVar, out _, "gragas_giftbarrelfoam.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
                }
                else if(this.skinID == 4)
                {
                    SpellEffectCreate(out this.troyVar, out _, "gragas_barrelfoam_classy.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out this.troyVar, out _, "gragas_barrelfoam.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
                }
            }
            else
            {
                if(this.skinID == 3)
                {
                    SpellEffectCreate(out this.troyVar, out _, "gragas_giftbarrelfoam.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
                }
                else if(this.skinID == 4)
                {
                    SpellEffectCreate(out this.troyVar, out _, "gragas_barrelfoam_classy.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out this.troyVar, out _, "gragas_barrelfoam.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "bottom", default, owner, default, default, true, default, default, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamofOwner;
            int level;
            float nextBuffVars_ASDebuff;
            int gragasSkinID;
            Particle particle; // UNUSED
            float cooldownVar;
            float cDMod;
            float cDModTrue;
            float barrelCD;
            float cDMinusBarrel;
            teamofOwner = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_ASDebuff = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, attacker.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.9f, 1, false, false, (ObjAIBase)owner);
                AddBuff(attacker, unit, new Buffs.GragasExplosiveCaskDebuff(nextBuffVars_ASDebuff), 1, 1, this.effect2[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            gragasSkinID = GetSkinID(owner);
            if(gragasSkinID == 4)
            {
                if(teamofOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out particle, out _, "gragas_barrelboom_classy.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, attacker.Position, owner, default, default, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out _, "gragas_barrelboom_classy.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, attacker.Position, owner, default, default, true, default, default, false);
                }
            }
            else
            {
                if(teamofOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out particle, out _, "gragas_barrelboom.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, attacker.Position, owner, default, default, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out particle, out _, "gragas_barrelboom.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, attacker.Position, owner, default, default, true, default, default, false);
                }
            }
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GragasBarrelRoll));
            cooldownVar = this.effect3[level];
            cDMod = GetPercentCooldownMod(owner);
            cDModTrue = cDMod + 1;
            barrelCD = cooldownVar * cDModTrue;
            cDMinusBarrel = barrelCD - lifeTime;
            SetSlotSpellCooldownTimeVer2(cDMinusBarrel, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(this.troyVar);
            SpellBuffRemove(owner, nameof(Buffs.GragasBarrelRoll), (ObjAIBase)owner);
            ApplyDamage(attacker, attacker, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            this.lifeTime = lifeTime;
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.GragasBarrelRollToggle))
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}