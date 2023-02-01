#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WillOfTheAncientsSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "WillOfTheAncientsFriendly",
            BuffTextureName = "2008_Tome_of_Combat_Mastery.dds",
        };
        float aP_Buff;
        float spellVamp_Buff;
        Particle willPlaceholder;
        public WillOfTheAncientsSelf(float aP_Buff = default, float spellVamp_Buff = default)
        {
            this.aP_Buff = aP_Buff;
            this.spellVamp_Buff = spellVamp_Buff;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aP_Buff);
            //RequireVar(this.spellVamp_Buff);
            SpellEffectCreate(out this.willPlaceholder, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.willPlaceholder);
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.aP_Buff);
            IncPercentSpellVampMod(owner, this.spellVamp_Buff);
        }
    }
}