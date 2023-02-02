﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CurseoftheSadMummy : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {150, 250, 350};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.CurseoftheSadMummy(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class CurseoftheSadMummy : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "CurseBandages.troy", },
            BuffName = "CurseoftheSadMummy",
            BuffTextureName = "SadMummy_BandAidThingy.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
    }
}