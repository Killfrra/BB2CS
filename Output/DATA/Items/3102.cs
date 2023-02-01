#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3102 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            bool nextBuffVars_WillRemove;
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BansheesVeilTimer)) == 0)
                {
                    nextBuffVars_WillRemove = false;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.BansheesVeil(nextBuffVars_WillRemove), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
        }
        public override void OnActivate()
        {
            ObjAIBase caster;
            bool nextBuffVars_WillRemove;
            if(owner is not Champion)
            {
                caster = GetPetOwner((Pet)owner);
                if(GetBuffCountFromCaster(caster, caster, nameof(Buffs.BansheesVeil)) > 0)
                {
                    nextBuffVars_WillRemove = false;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.BansheesVeil(nextBuffVars_WillRemove), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BansheesVeil)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BansheesVeilTimer)) == 0)
                    {
                        nextBuffVars_WillRemove = false;
                        AddBuff((ObjAIBase)owner, owner, new Buffs.BansheesVeil(nextBuffVars_WillRemove), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                    }
                }
            }
        }
    }
}