﻿using System;
using System.Collections.Generic;

namespace Seasar.Quill.Typical.Creation
{
    public class PreparedSingletonFactory
    {
        private static IDictionary<Type, object> _preparedInstances
            = new Dictionary<Type, object>();

        public static void Initialize(IDictionary<Type, object> preparedInstances)
        {
            _preparedInstances = preparedInstances;
        }

        public static void Prepare(Type targetType, object instance)
        {
            _preparedInstances[targetType] = instance;
        }

        public static bool IsPrepared(Type targetType)
        {
            return _preparedInstances.ContainsKey(targetType);
        }

        public static bool IsCached(Type targetType)
        {
            return (IsPrepared(targetType) || SingletonFactory.IsCached(targetType));
        }

        public static TARGET GetInstance<TARGET>() where TARGET : new()
        {
            return (TARGET)GetInstance(typeof(TARGET));
        }

        public static object GetInstance(Type targetType)
        {
            if (_preparedInstances.ContainsKey(targetType))
            {
                return _preparedInstances[targetType];
            }
            return SingletonFactory.GetInstance(targetType);
        }
    }
}
