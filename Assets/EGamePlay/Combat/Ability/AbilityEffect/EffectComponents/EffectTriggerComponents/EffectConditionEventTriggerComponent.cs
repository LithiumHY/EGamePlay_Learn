﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 条件触发组件
    /// </summary>
    public class EffectConditionEventTriggerComponent : Component
    {
        public override bool DefaultEnable { get; set; } = false;
        public string ConditionParamValue { get; set; }


        public override void OnEnable()
        {
            var conditionType = Entity.GetParent<AbilityEffect>().EffectConfig.ConditionType;
            var conditionParam = ConditionParamValue;
            //Log.Debug($"EffectConditionEventTriggerComponent {conditionType} {conditionParam}");
            Entity.GetParent<AbilityEffect>().Parent.As<IAbilityEntity>().ParentEntity.ListenerCondition(conditionType, OnConditionTrigger, conditionParam);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            var conditionType = Entity.GetParent<AbilityEffect>().EffectConfig.ConditionType;
            Entity.GetParent<AbilityEffect>().Parent.As<IAbilityEntity>().ParentEntity.UnListenCondition(conditionType, OnConditionTrigger);
        }

        private void OnConditionTrigger()
        {
            var conditionType = Entity.GetParent<AbilityEffect>().EffectConfig.ConditionType;
            //Log.Error($"EffectConditionEventTriggerComponent OnConditionTrigger {conditionType}");
            GetEntity<EffectTriggerEventBind>().TriggerEffectToParent();
        }
    }
}