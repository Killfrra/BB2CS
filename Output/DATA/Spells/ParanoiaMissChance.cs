#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ParanoiaMissChance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Paranoia",
            BuffTextureName = "Fiddlesticks_Paranoia.dds",
        };
        public override void OnActivate()
        {
            IncPermanentFlatSpellBlockMod(owner, -10);
            SpellEffectCreate(out _, out _, "ConsecrationAura_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatSpellBlockMod(owner, 10);
        }
        public override void OnUpdateActions()
        {
            float dist;
            if(attacker.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                dist = DistanceBetweenObjects("Attacker", "Owner");
                if(dist >= 800)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}