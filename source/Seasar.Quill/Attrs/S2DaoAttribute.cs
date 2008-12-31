#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using Seasar.Quill.Util;

namespace Seasar.Quill.Attrs
{
    /// <summary>
    /// 属性を使ってDaoInterceptorをかけるための属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface |
       AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class S2DaoAttribute : Attribute
    {
        private Type _daoSettingType = null;

        public virtual Type DaoSettingType
        {
            get
            {
                return _daoSettingType;
            }
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// (標準の設定を使います)
        /// </summary>
        public S2DaoAttribute()
        {
            QuillConfig config = QuillConfig.GetInstance();
            _daoSettingType = config.GetDaoSettingType();
            SettingUtil.ValidateDaoSettingType(_daoSettingType);
        }

        /// <summary>
        /// カスタムコンストラクタ
        /// (指定したDao設定クラスを使います)
        /// (IDaoSetting実装クラスではない場合実行時例外を投げます）
        /// </summary>
        /// <param name="handlerType"></param>
        public S2DaoAttribute(Type settingType)
        {
            SettingUtil.ValidateDaoSettingType(settingType);
            _daoSettingType = settingType;
        }
    }
}
