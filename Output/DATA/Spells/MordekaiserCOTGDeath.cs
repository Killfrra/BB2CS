#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCOTGDeath : BBBuffScript
    {
        public override void OnActivate()
        {
            Vector3 pos;
            Pet other1; // UNUSED
            pos = GetRandomPointInAreaUnit(owner, 400, 200);
            other1 = CloneUnitPet(attacker, nameof(Buffs.MordekaiserCOTGPetBuff), 0, pos, 0, 0, false);
        }
    }
}