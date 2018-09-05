using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RangeDamageCount
{
    public class DamageCount
    {
        //计算伤害
        public static AttackResult CountDamge(SkillData skill,UnitData target)
        {
            AttackResult result = new AttackResult();

            result.resultType = IsDodge(skill,target) ? AttackResult.ResultType.miss : result.resultType;//是否闪避
            if (result.resultType == AttackResult.ResultType.miss) return result;

            result.resultType = IsCritical(skill,result) ? AttackResult.ResultType.critical : result.resultType;//是否暴击

            result.healthNumber = Mathf.Max(1,skill.uniData.mAttData.GetAttValue(0) - target.mAttData.GetAttValue(1));//伤害 = 攻击-防御.最少造成1点伤害

            if (result.resultType == AttackResult.ResultType.critical) result.healthNumber *= 2;

            return result;
        }
        //计算回复---未实现
        public static AttackResult CountHeal(SkillData skill,UnitData target)
        {

            AttackResult result = new AttackResult();
            result.resultType = IsCritical(skill, result) ? AttackResult.ResultType.critical : result.resultType;//是否暴击

            

            return result;
        }




        static bool IsCritical(SkillData skill,AttackResult result)
        {
            int critcal = skill.uniData.mAttData.GetAttValue(9);
            return Random.Range(0, 100) <= critcal ? true : false;
        }


        static bool IsDodge(SkillData skill,UnitData tar)
        {
            //if (tar == null)
            //    return;
            int dodoge = tar.mAttData.GetAttValue(10);
            return Random.Range(0, 100) <= dodoge ? true:false;
        }

        
    }



    public class AttackResult
    {
        public int healthNumber = 0;//负数为伤害，正数为回复
        //攻击结果：是否暴击、闪避等
        public enum ResultType
        {
            normal,
            critical,
            miss,
            unDefence
        }
        public ResultType resultType = new ResultType();

        public AttackResult()
        {
            resultType = ResultType.normal;
        }


    }


}
