#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerHaste : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Haste.troy", },
            BuffName = "Haste",
            BuffTextureName = "Summoner_haste.dds",
        };
        float moveSpeedMod;
        public SummonerHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            SetGhosted(owner, true);
        }
    }
}
namespace Spells
{
    public class SummonerHaste : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            float moveSpeedMod;
            float baseCooldown;
            float cooldownMultiplier;
            if(avatarVars.OffensiveMastery == 1)
            {
                moveSpeedMod = 0.35f;
            }
            else
            {
                moveSpeedMod = 0.27f;
            }
            moveSpeedMod *= 100;
            SetSpellToolTipVar(moveSpeedMod, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
            baseCooldown = 210;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (Champion)attacker);
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            float cooldownMultiplier;
            float baseCooldown;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 210 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle ar; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            SpellEffectCreate(out ar, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            nextBuffVars_MoveSpeedMod = 0.27f;
            if(avatarVars.OffensiveMastery == 1)
            {
                nextBuffVars_MoveSpeedMod = 0.35f;
            }
            AddBuff((ObjAIBase)owner, target, new Buffs.SummonerHaste(nextBuffVars_MoveSpeedMod), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}