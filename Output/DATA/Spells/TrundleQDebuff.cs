#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleQDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleQDebuff",
            BuffTextureName = "Trundle_Bite.dds",
        };
        int sapVar;
        float negSapVar;
        Particle sappedDebuff;
        public TrundleQDebuff(int sapVar = default, float negSapVar = default)
        {
            this.sapVar = sapVar;
            this.negSapVar = negSapVar;
        }
        public override void OnActivate()
        {
            float nextBuffVars_SapVar;
            //RequireVar(this.negSapVar);
            //RequireVar(this.sapVar);
            nextBuffVars_SapVar = this.sapVar;
            IncFlatPhysicalDamageMod(owner, this.negSapVar);
            SpellEffectCreate(out this.sappedDebuff, out _, "TrundleQDebuff_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            AddBuff(attacker, attacker, new Buffs.TrundleQ(nextBuffVars_SapVar), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.sappedDebuff);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.negSapVar);
        }
    }
}