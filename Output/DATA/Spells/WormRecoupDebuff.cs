#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WormRecoupDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WormRecoupDebuff",
            BuffTextureName = "1031_Chain_Vest.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.WormRecouperate1), (ObjAIBase)owner);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecoupDebuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}