#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HalloweenUrfWarwick : BBBuffScript
    {
        Particle a;
        Particle b;
        Particle c;
        Particle d;
        Particle e;
        Particle f;
        Particle g;
        Particle h;
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.HalloweenUrfCD(), 1, 1, 9, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            SpellEffectCreate(out this.a, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            SpellEffectCreate(out this.b, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "fish_main", default, owner, default, default, false);
            SpellEffectCreate(out this.c, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_tail", default, owner, default, default, false);
            SpellEffectCreate(out this.d, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_tail_3", default, owner, default, default, false);
            SpellEffectCreate(out this.e, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_uparm", default, owner, default, default, false);
            SpellEffectCreate(out this.f, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_uparm", default, owner, default, default, false);
            SpellEffectCreate(out this.g, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_flipper", default, owner, default, default, false);
            SpellEffectCreate(out this.h, out _, "ghostUrf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_flipper", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, true);
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.b);
            SpellEffectRemove(this.c);
            SpellEffectRemove(this.d);
            SpellEffectRemove(this.e);
            SpellEffectRemove(this.f);
            SpellEffectRemove(this.g);
            SpellEffectRemove(this.h);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, false);
        }
    }
}