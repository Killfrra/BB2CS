#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptShen : BBCharScript
    {
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShenWayOfTheNinjaMarker(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IsNinja(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
        }
    }
}