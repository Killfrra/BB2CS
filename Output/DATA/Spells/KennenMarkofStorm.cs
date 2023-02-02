#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenMarkofStorm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "KennenMarkOfStorm",
            BuffTextureName = "Kennen_MarkOfStorm.dds",
        };
        bool doOnce;
        int count;
        Particle globeTwo;
        public override void OnActivate()
        {
            TeamId teamID;
            int level; // UNUSED
            bool doOnce; // UNUSED
            teamID = GetTeamID(attacker);
            this.doOnce = false;
            level = GetLevel(owner);
            this.count = GetBuffCountFromAll(owner, nameof(Buffs.KennenMarkofStorm));
            doOnce = false;
            if(this.count == 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.KennenParticleHolder(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(this.count == 2)
            {
                this.doOnce = true;
                SpellBuffRemove(owner, nameof(Buffs.KennenParticleHolder), (ObjAIBase)owner);
                SpellEffectCreate(out this.globeTwo, out _, "kennen_mos2.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            }
            if(this.count >= 3)
            {
                Particle part; // UNUSED
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.KennenMoSDiminish)) == 0)
                {
                    BreakSpellShields(owner);
                    IncPAR(attacker, 25, PrimaryAbilityResourceType.Energy);
                    SpellEffectCreate(out part, out _, "kennen_mos_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
                    ApplyStun(attacker, owner, 1.25f);
                    SpellBuffRemoveStacks(owner, attacker, nameof(Buffs.KennenMarkofStorm), 0);
                    if(target is Champion)
                    {
                        AddBuff(attacker, owner, new Buffs.KennenMoSDiminish(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
                else
                {
                    BreakSpellShields(owner);
                    IncPAR(attacker, 25, PrimaryAbilityResourceType.Energy);
                    SpellEffectCreate(out part, out _, "kennen_mos_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
                    ApplyStun(attacker, owner, 0.6f);
                    SpellBuffRemoveStacks(owner, attacker, nameof(Buffs.KennenMarkofStorm), 0);
                    if(target is Champion)
                    {
                        AddBuff(attacker, owner, new Buffs.KennenMoSDiminish(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.doOnce)
            {
                SpellEffectRemove(this.globeTwo);
            }
        }
    }
}