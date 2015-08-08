#region Copyright
/*
 * Copyright 2005-2015 the Seasar Foundation and the Others.
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
using System.Collections.Generic;
using System.Windows.Forms;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Framework.Container;
using Seasar.Framework.Util;
using Seasar.Windows.Attr;

namespace Seasar.Windows.AOP.Interceptors
{
    /// <summary>
    /// �w�肳�ꂽForm��\������Interceptor
    /// </summary>
    public class FormInterceptor : AbstractInterceptor
    {
        /// <summary>
        /// DI�R���e�i
        /// </summary>
        private readonly IS2Container _container;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="container">DI�R���e�i</param>
        public FormInterceptor(IS2Container container)
        {
            _container = container;
            Property = string.Empty;
        }

        /// <summary>
        /// �Ԃ�l�p�v���p�e�B��
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns>DialogResult</returns>
        /// <remarks>Default�̖߂�l��DialgResult.No</remarks>
        public override object Invoke(IMethodInvocation invocation)
        {
            var retOfReplace = DialogResult.No;

            object ret = retOfReplace;

            // ���\�b�h�̈����l�̎擾
            var args = invocation.Arguments;
            var pis = invocation.Method.GetParameters();

            IDictionary<string, object> hashOfParams = new Dictionary<string, object>();
            IList<string> listOfParams = new List<string>();
            IDictionary<string, string> hashOfPropNames = new Dictionary<string, string>();

            foreach (var pi in pis)
            {
                hashOfParams.Add(pi.Name.ToLower(), args[pi.Position]);
                listOfParams.Add(pi.Name.ToLower());
            }

            // WindowsForm�̕\��
            var attributes = invocation.Method.GetCustomAttributes(false);
            foreach (var o in attributes)
            {
                var formAttribute = o as TargetFormAttribute;
                if (formAttribute != null)
                {
                    // �߂�l�̃v���p�e�B�����擾����
                    var attribute = formAttribute;
                    var formType = attribute.FormType;
                    var form = (Form) _container.GetComponent(formType);
                    if (form == null)
                        throw new NullReferenceException(SWFMessages.ASWF0001);
                    

                    string propertyName;
                    if (attribute.ReturnPropertyName != string.Empty)
                    {
                        propertyName = attribute.ReturnPropertyName;
                    }
                    else
                    {
                        propertyName = Property ?? string.Empty;
                    }

                    // �t�H�[���ɒl���Z�b�g����
                    var infos = form.GetExType().GetProperties();
                    foreach (var info in infos)
                    {
                        hashOfPropNames.Add(info.Name.ToLower(), info.Name);
                    }

                    for (var i = 0; i < listOfParams.Count; i++)
                    {
                        if (hashOfPropNames.ContainsKey(listOfParams[i]))
                        {
                            var propName = hashOfPropNames[listOfParams[i]];
                            var property = form.GetExType().GetProperty(propName);
                            property.SetValue(form, hashOfParams[listOfParams[i]], null);
                        }
                    }

                    // WindowsForm�̕\��
                    if (attribute.Mode == ModalType.Modal)
                    {
                        retOfReplace = form.ShowDialog();
                        ret = retOfReplace;

                        // WindowsForm����߂�l�p�v���p�e�B����l���擾����
                        if (propertyName != string.Empty)
                        {
                            var propInfo = form.GetExType().GetProperty(propertyName);
                            if (propInfo != null)
                                ret = propInfo.GetValue(form, null);
                        }
                    }
                    else
                    {
                        form.Show();
                    }
                }
            }

            return ret;
        }
    }
}
