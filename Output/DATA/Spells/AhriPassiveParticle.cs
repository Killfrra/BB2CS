#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriPassiveParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "AhriSoulCrusher",
            BuffTextureName = "Ahri_SoulEater.dds",
            PersistsThroughDeath = true,
        };
        bool particleAlive; // UNUSED
        Particle particle1;
        public override void OnActivate()
        {
            this.particleAlive = false;
            if(!owner.IsDead)
            {
                TeamId teamID;
                teamID = GetTeamID(owner);
                SpellEffectCreate(out this.particle1, out _, "Ahri_Passive.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false, false, false, false, false);
                this.particleAlive = true;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnDeath()
        {
            SpellEffectRemove(this.particle1);
            this.particleAlive = false;
        }
        public override void OnResurrect()
        {
            bool particleAlive; // UNITIALIZED
            if(particleAlive)
            {
                SpellEffectRemove(this.particle1);
                this.particleAlive = false;
            }
            SpellEffectCreate(out this.particle1, out _, "Ahri_Passive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false, false, false, false, false);
        }
    }
}