#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingDecoyStealth : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingDecoyStealth",
            BuffTextureName = "MonkeyKingDecoy.dds",
        };
        public override void OnActivate()
        {
            Fade iD; // UNUSED
            iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            SetGhosted(owner, false);
            PushCharacterFade(owner, 1, 0);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
            SetGhosted(owner, true);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.CastingBreaksStealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if(!spellVars.CastingBreaksStealth)
            {
            }
            else if(!spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnLaunchAttack()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}