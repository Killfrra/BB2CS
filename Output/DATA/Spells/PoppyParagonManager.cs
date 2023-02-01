#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyParagonManager : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int count;
            Particle a; // UNUSED
            Particle b; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonIcon(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            count = GetBuffCountFromAll(owner, nameof(Buffs.PoppyParagonStats));
            if(count == 10)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonParticle(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            else
            {
                SpellEffectCreate(out a, out _, "poppydam_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "hammer_b", default, target, default, default, false);
                SpellEffectCreate(out b, out _, "poppydef_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger", default, target, default, default, false);
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int count;
            Particle a; // UNUSED
            Particle b; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonStats(), 10, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonIcon(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            count = GetBuffCountFromAll(owner, nameof(Buffs.PoppyParagonStats));
            if(count == 10)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonParticle(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            else
            {
                SpellEffectCreate(out a, out _, "poppydam_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "hammer_b", default, target, default, default, false);
                SpellEffectCreate(out b, out _, "poppydef_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger", default, target, default, default, false);
            }
        }
    }
}