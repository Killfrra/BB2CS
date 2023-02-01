#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriFoxFireMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AhriFoxFire",
            BuffTextureName = "Ahri_FoxFire.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.AhriFoxFireMissile));
            if(count <= 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.AhriFoxFire), (ObjAIBase)owner, 0);
            }
        }
    }
}
namespace Spells
{
    public class AhriFoxFireMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {30, 60, 90, 120, 150};
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            int count;
            float baseDamage;
            float myAP;
            float myAPBonus;
            float totalDamage;
            float theirSpellBlock;
            float theirSpellBlockPercent;
            float theirSpellBlockRatio;
            float projectedDamage;
            float theirHealth;
            spellVars.Ready++;
            if(spellVars.Ready >= 3)
            {
                count = 0;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach(AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, missilePosition, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    SpellCast((ObjAIBase)owner, unit, default, default, 3, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, missilePosition);
                    count = 1;
                    DestroyMissile(missileNetworkID);
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AhriFoxFireMissile), 1);
                }
                if(count == 0)
                {
                    foreach(AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, missilePosition, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
                    {
                        SpellCast((ObjAIBase)owner, unit, default, default, 3, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, missilePosition);
                        DestroyMissile(missileNetworkID);
                        SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AhriFoxFireMissile), 1);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        baseDamage = this.effect0[level];
                        myAP = GetFlatMagicDamageMod(owner);
                        myAPBonus = myAP * 0.3f;
                        totalDamage = baseDamage + myAPBonus;
                        theirSpellBlock = GetSpellBlock(unit);
                        theirSpellBlockPercent = theirSpellBlock / 100;
                        theirSpellBlockRatio = theirSpellBlockPercent + 1;
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.AhriFoxFireMissileTwo)) > 0)
                        {
                            totalDamage /= 2;
                        }
                        else
                        {
                            if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.AhriFoxFireMissileTagTwo)) > 0)
                            {
                                totalDamage *= 1.5f;
                            }
                        }
                        projectedDamage = totalDamage / theirSpellBlockRatio;
                        theirHealth = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                        if(theirHealth < projectedDamage)
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.AhriFoxFireMissileTag(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.AhriFoxFireMissileTagTwo(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}