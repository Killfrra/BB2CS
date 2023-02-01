#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EzrealRisingSpellForce : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealRisingSpellForce",
            BuffTextureName = "Ezreal_RisingSpellForce.dds",
        };
        Particle particle;
        int lastCount;
        public override void OnActivate()
        {
            int count;
            float totalAttackSpeed;
            count = GetBuffCountFromAll(owner, nameof(Buffs.EzrealRisingSpellForce));
            totalAttackSpeed = count * 10;
            SetBuffToolTipVar(1, totalAttackSpeed);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.EzrealRisingSpellForce)) > 0)
            {
                if(count == 1)
                {
                    SpellEffectCreate(out this.particle, out _, "Ezreal_glow1.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if(count == 2)
                {
                    SpellEffectCreate(out this.particle, out _, "Ezreal_glow2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if(count == 3)
                {
                    SpellEffectCreate(out this.particle, out _, "Ezreal_glow3.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if(count == 4)
                {
                    SpellEffectCreate(out this.particle, out _, "Ezreal_glow4.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                }
                if(count == 5)
                {
                    if(this.lastCount != 5)
                    {
                        SpellEffectCreate(out this.particle, out _, "Ezreal_glow5.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, default, default, false);
                    }
                }
                this.lastCount = count;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.1f);
        }
    }
}