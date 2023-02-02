#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneSilveredDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VayneSilverDebuff",
            BuffTextureName = "Vayne_SilveredBolts.dds",
        };
        bool doOnce2;
        Particle globeTwo;
        public override void OnActivate()
        {
            ObjAIBase caster;
            TeamId teamID;
            TeamId teamIDTarget; // UNUSED
            int count;
            caster = SetBuffCasterUnit();
            teamID = GetTeamID(caster);
            teamIDTarget = GetTeamID(target);
            count = GetBuffCountFromCaster(owner, caster, nameof(Buffs.VayneSilveredDebuff));
            if(count == 1)
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, caster.Position, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, nameof(Buffs.VayneSilveredDebuff), true))
                {
                    SpellBuffRemove(unit, nameof(Buffs.VayneSilveredDebuff), caster, 0);
                    SpellBuffRemove(unit, nameof(Buffs.VayneSilveredDebuff), caster, 0);
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.VayneSilverParticle1(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else if(count == 2)
            {
                SpellBuffRemove(owner, nameof(Buffs.VayneSilverParticle1), (ObjAIBase)owner, 0);
                this.doOnce2 = true;
                SpellEffectCreate(out this.globeTwo, out _, "vayne_W_ring2.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.VayneSilverParticle1), (ObjAIBase)owner, 0);
            if(this.doOnce2)
            {
                SpellEffectRemove(this.globeTwo);
                this.doOnce2 = false;
            }
        }
    }
}