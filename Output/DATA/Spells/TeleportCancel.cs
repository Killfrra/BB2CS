#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TeleportCancel : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SummonerTeleport)) > 0)
            {
                charVars.TeleportCancelled = true;
                SpellBuffRemove(owner, nameof(Buffs.SummonerTeleport), (ObjAIBase)owner, 0);
            }
        }
    }
}