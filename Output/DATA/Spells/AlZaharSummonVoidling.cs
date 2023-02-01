#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlZaharSummonVoidling : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Voidling_Ready.troy", },
            BuffName = "AlZaharSummonVoidlingReady",
            BuffTextureName = "AlZahar_VoidlingReady.dds",
            NonDispellable = true,
        };
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            TeamId teamID;
            Vector3 castPos;
            Minion other3;
            int level;
            float bonusDamage;
            float bonusHealth;
            float nextBuffVars_BonusHealth;
            float nextBuffVars_BonusDamage;
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                teamID = GetTeamID(owner);
                castPos = GetPointByUnitFacingOffset(owner, 100, 0);
                other3 = SpawnMinion("Voidling", "MalzaharVoidling", "UncontrollablePet.lua", castPos, teamID, false, false, true, false, false, true, 0, default, false, (Champion)owner);
                level = GetLevel(owner);
                bonusDamage = level * 5;
                bonusHealth = level * 50;
                nextBuffVars_BonusHealth = bonusHealth;
                nextBuffVars_BonusDamage = bonusDamage;
                AddBuff((ObjAIBase)owner, other3, new Buffs.AlZaharVoidling(nextBuffVars_BonusHealth, nextBuffVars_BonusDamage), 1, 1, 21, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                SpellBuffRemove(owner, nameof(Buffs.AlZaharSummonVoidling), (ObjAIBase)owner);
            }
        }
    }
}