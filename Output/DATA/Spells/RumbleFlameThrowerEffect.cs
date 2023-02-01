#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleFlameThrowerEffect : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GatlingGunSelf",
            BuffTextureName = "Corki_GatlingGun.dds",
            SpellToggleSlot = 3,
        };
        Particle test;
        Particle test2;
        public override void OnActivate()
        {
            //RequireVar(this.dangerZone);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleFlameThrowerBuff)) > 0)
            {
                SpellEffectCreate(out this.test, out _, "rumble_gun_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "barrel", default, target, default, default, false, default, default, false);
                SpellEffectCreate(out this.test2, out _, "rumble_gun_lite.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.test, out _, "rumble_gun_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "barrel", default, target, default, default, false, default, default, false);
                SpellEffectCreate(out this.test2, out _, "rumble_gun_lite.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.test);
            SpellEffectRemove(this.test2);
        }
    }
}