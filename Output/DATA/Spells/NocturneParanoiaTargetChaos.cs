#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneParanoiaTargetChaos : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "NocturneParanoiaTarget",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        bool delay;
        Particle loop;
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(owner);
            this.delay = false;
            if(teamOfOwner == TeamId.TEAM_PURPLE)
            {
                int count;
                count = GetBuffCountFromAll(owner, nameof(Buffs.NocturneParanoiaTargetOrder));
                if(count > 0)
                {
                    this.delay = true;
                }
                else
                {
                    SpellEffectCreate(out this.loop, out _, "NocturneParanoiaFriend.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, "root", default, owner, default, default, false);
                }
            }
            else
            {
                SpellBuffClear(owner, nameof(Buffs.NocturneParanoiaTargetOrder));
                SpellEffectCreate(out this.loop, out _, "NocturneParanoiaFoe.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, "root", default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(!this.delay)
            {
                SpellEffectRemove(this.loop);
            }
        }
        public override void OnUpdateActions()
        {
            if(this.delay)
            {
                int count;
                count = GetBuffCountFromAll(owner, nameof(Buffs.NocturneParanoiaTargetOrder));
                if(count == 0)
                {
                    SpellEffectCreate(out this.loop, out _, "NocturneParanoiaFriend.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, "root", default, owner, default, default, false);
                    this.delay = false;
                }
            }
        }
    }
}