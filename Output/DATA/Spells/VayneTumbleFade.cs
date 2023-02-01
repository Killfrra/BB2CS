#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneTumbleFade : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VayneInquisitionStealth",
            BuffTextureName = "MasterYi_Vanish.dds",
        };
        public override void OnActivate()
        {
            PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            PushCharacterFade(owner, 1, 0);
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.CastingBreaksStealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if(!spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}