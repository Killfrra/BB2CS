#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancMIApplicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Fade iD; // UNUSED
        public override void OnActivate()
        {
            Vector3 ownerPos;
            Particle fadeParticle; // UNUSED
            this.iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
            ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out fadeParticle, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, default, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            this.iD = PushCharacterFade(owner, 1, 0);
            if(!owner.IsDead)
            {
                Vector3 pos1;
                int level; // UNUSED
                Pet other1;
                Particle fadeParticle; // UNUSED
                pos1 = GetRandomPointInAreaUnit(owner, 250, 50);
                level = 1;
                other1 = CloneUnitPet(owner, nameof(Buffs.LeblancMI), 8, pos1, 0, 0, true);
                AddBuff(other1, other1, new Buffs.LeblancPassiveCooldown(), 1, 1, 60, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)owner, other1, new Buffs.LeblancMIFull(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out fadeParticle, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, default, other1, default, default, false, false, false, false, false);
                SpellEffectCreate(out fadeParticle, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}