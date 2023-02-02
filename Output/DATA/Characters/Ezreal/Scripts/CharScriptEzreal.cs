#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptEzreal : BBCharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            float cURMoveSpeed;
            if(ExecutePeriodically(1, ref this.lastTime2Executed, true))
            {
                float totalDamage;
                float baseDamage;
                float bonusDamage;
                float spell3Display;
                float aP;
                float finalAP;
                float baseDamage;
                float attackDamage;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                totalDamage = GetTotalAttackDamage(owner);
                baseDamage = GetBaseAttackDamage(owner);
                bonusDamage = totalDamage - baseDamage;
                spell3Display = bonusDamage * 1;
                SetSpellToolTipVar(spell3Display, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                aP = GetFlatMagicDamageMod(owner);
                finalAP = aP * 0.2f;
                baseDamage = GetTotalAttackDamage(owner);
                attackDamage = 1 * baseDamage;
                SetSpellToolTipVar(attackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
                SetSpellToolTipVar(finalAP, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            }
            cURMoveSpeed = GetMovementSpeed(owner);
            if(cURMoveSpeed > 390)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.EzrealFastRunAnim)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.EzrealFastRunAnim(), 1, 1, 100000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if(cURMoveSpeed < 390)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.EzrealFastRunAnim)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.EzrealFastRunAnim), (ObjAIBase)owner, 0);
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.EzrealTrueshotBarrage))
            {
                charVars.PercentOfAttack = 1;
                AddBuff((ObjAIBase)owner, owner, new Buffs.CantAttack(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.CastPoint = 1;
            AddBuff((ObjAIBase)owner, owner, new Buffs.EzrealCyberSkinSound(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            TeamId teamID;
            int ezrealSkinID;
            teamID = GetTeamID(owner);
            ezrealSkinID = GetSkinID(owner);
            if(ezrealSkinID == 5)
            {
                Particle a; // UNUSED
                SpellEffectCreate(out a, out _, "Ezreal_cyberezreal_revive.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            TeamId teamID;
            int ezrealSkinID;
            teamID = GetTeamID(attacker);
            ezrealSkinID = GetSkinID(attacker);
            if(ezrealSkinID == 5)
            {
                Particle a; // UNUSED
                if(slot == 0)
                {
                    SpellEffectCreate(out a, out _, "Ezreal_cyberezreal_mysticshot.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
                else if(slot == 1)
                {
                    SpellEffectCreate(out a, out _, "Ezreal_cyberezreal_essenceflux.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
                else if(slot == 2)
                {
                    SpellEffectCreate(out a, out _, "Ezreal_cyberezreal_arcaneshift.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
                else if(slot == 3)
                {
                    SpellEffectCreate(out a, out _, "Ezreal_cyberezreal_trueshotbarrage.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptEzreal : BBBuffScript
    {
    }
}