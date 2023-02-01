#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliSmokeBombInternal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        bool willFade;
        Particle abc;
        public override void OnActivate()
        {
            TeamId teamID;
            Fade iD; // UNUSED
            teamID = GetTeamID(owner);
            //RequireVar(this.initialTime);
            //RequireVar(this.timeLastHit);
            this.willFade = true;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) == 0)
            {
                iD = PushCharacterFade(owner, 0.2f, 1.5f);
                this.willFade = false;
                SpellEffectCreate(out this.abc, out _, "akali_invis_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove;
            nextBuffVars_WillRemove = false;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliSBStealth(nextBuffVars_WillRemove), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            }
            if(!this.willFade)
            {
                SpellEffectRemove(this.abc);
            }
        }
        public override void OnDeath()
        {
            if(owner.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}