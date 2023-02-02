#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingCloneApplicator : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.MonkeyKingCloneFull)) == 0)
            {
                if(!owner.IsDead)
                {
                    Vector3 pos1;
                    int level; // UNUSED
                    Pet other1;
                    Particle fadeParticle; // UNUSED
                    pos1 = GetPointByUnitFacingOffset(owner, 100, 0);
                    level = 1;
                    other1 = CloneUnitPet(owner, nameof(Buffs.MonkeyKingClone), 25000, pos1, 0, 0, false);
                    AddBuff(other1, owner, new Buffs.MonkeyKingCloneSpellCast(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, other1, new Buffs.MonkeyKingCloneFull(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellEffectCreate(out fadeParticle, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, default, other1, default, default, false, false, false, false, false);
                    SpellEffectCreate(out fadeParticle, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                }
            }
        }
    }
}