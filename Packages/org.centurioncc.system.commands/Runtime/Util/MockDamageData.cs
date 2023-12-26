using System;
using CenturionCC.System.Utils;
using UdonSharp;
using UnityEngine;

namespace CenturionCC.System.Util
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MockDamageData : DamageData
    {
        private Vector3 _damageOriginPos;
        private Quaternion _damageOriginRot;
        private DateTime _damageOriginTime;
        private int _damagerPlayerId;
        private string _damageType;
        private bool _shouldApplyDamage;

        public override bool ShouldApplyDamage => _shouldApplyDamage;
        public override int DamagerPlayerId => _damagerPlayerId;
        public override Vector3 DamageOriginPosition => _damageOriginPos;
        public override Quaternion DamageOriginRotation => _damageOriginRot;
        public override DateTime DamageOriginTime => _damageOriginTime;
        public override string DamageType => _damageType;

        public void SetData(bool apply, int playerId, Vector3 pos, Quaternion rot, DateTime time, string type)
        {
            _shouldApplyDamage = apply;
            _damagerPlayerId = playerId;
            _damageOriginPos = pos;
            _damageOriginRot = rot;
            _damageOriginTime = time;
            _damageType = type;
        }
    }
}