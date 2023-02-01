#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxPrismaFieldStealth : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "LuxPrismWrap",
            BuffTextureName = "LuxPrismaWrap.dds",
        };
        bool willRemove;
        Particle akaliStealth;
        public override void OnActivate()
        {
            TeamId teamID;
            Fade iD; // UNUSED
            teamID = GetTeamID(owner);
            //RequireVar(this.willRemove);
            SpellEffectCreate(out this.akaliStealth, out _, "akali_twilight_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
            SetGhosted(owner, true);
            this.willRemove = false;
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            SetGhosted(owner, false);
            PushCharacterFade(owner, 1, 0);
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
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.CastingBreaksStealth)
            {
                this.willRemove = true;
            }
            else if(!spellVars.DoesntTriggerSpellCasts)
            {
                this.willRemove = true;
            }
        }
        public override void OnDeath()
        {
            if(owner.IsDead)
            {
                this.willRemove = true;
            }
        }
        public override void OnLaunchAttack()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}