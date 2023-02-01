#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrMonkeyAgility : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrPassiveBuff",
            BuffTextureName = "BlindMonk_FistsOfFury.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrMonkeyAgilityBuff(), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
    }
}