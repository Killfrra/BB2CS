#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliSBStealth : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "AkaliTwilightShroud",
            BuffTextureName = "AkaliTwilightShroud.dds",
        };
        Particle akaliStealth;
        bool willRemove;
        float moveSpeedBuff;
        int[] effect0 = {0, 0, 0, 0, 0};
        public AkaliSBStealth(bool willRemove = default)
        {
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Fade iD; // UNUSED
            int level;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.akaliStealth, out _, "akali_twilight_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
            SetGhosted(owner, true);
            this.willRemove = false;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.moveSpeedBuff = this.effect0[level];
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            SetGhosted(owner, false);
            PushCharacterFade(owner, 1, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliHoldStealth(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            SpellEffectRemove(this.akaliStealth);
        }
        public override void OnUpdateStats()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
            IncPercentMovementSpeedMod(owner, this.moveSpeedBuff);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellName != nameof(Spells.ShadowWalk))
            {
                if(spellVars.CastingBreaksStealth)
                {
                    this.willRemove = true;
                }
                else if(!spellVars.CastingBreaksStealth)
                {
                }
                else if(!spellVars.DoesntTriggerSpellCasts)
                {
                    this.willRemove = true;
                }
            }
        }
        public override void OnDeath()
        {
            if(owner.IsDead)
            {
                this.willRemove = true;
            }
        }
    }
}