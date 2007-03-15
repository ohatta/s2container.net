#region Copyright
/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
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
using System.Reflection;
using System.Windows.Forms;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Framework.Container;
using Seasar.Windows.Attr;

#if NET_1_1
    using System.Collections;
    using System.Collections.Specialized;
#else
    using System.Collections.Generic;
#endif

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
        private IS2Container container_;

        /// <summary>
        /// �Ԃ�l�p�v���p�e�B��
        /// </summary>
        private string returnPropertyName_;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="container">DI�R���e�i</param>
        public FormInterceptor(IS2Container container)
        {
            container_ = container;
            returnPropertyName_ = "";
        }

        /// <summary>
        /// �Ԃ�l�p�v���p�e�B��
        /// </summary>
        public string Property
        {
            get { return returnPropertyName_; }
            set { returnPropertyName_ = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns>DialogResult</returns>
        /// <remarks>Default�̖߂�l��DialgResult.No</remarks>
        public override object Invoke(IMethodInvocation invocation)
        {
            DialogResult retOfReplace = DialogResult.No;

            object ret = retOfReplace;

            string propertyName;

            // ���\�b�h�̈����l�̎擾
            object[] args = invocation.Arguments;
            ParameterInfo[] pis = invocation.Method.GetParameters();

#if NET_1_1
            Hashtable hashOfParams = CollectionsUtil.CreateCaseInsensitiveHashtable();
            IList listOfParams = new ArrayList();
            Hashtable hashOfPropNames = CollectionsUtil.CreateCaseInsensitiveHashtable();
#else
            IDictionary<string, object> hashOfParams = new Dictionary<string, object>();
            IList<string> listOfParams = new List<string>();
            IDictionary<string, string> hashOfPropNames = new Dictionary<string, string>();
#endif
       
            foreach (ParameterInfo pi in pis)
            {
                hashOfParams.Add(pi.Name.ToLower(), args[pi.Position]);
                listOfParams.Add(pi.Name.ToLower());
            }

            // WindowsForm�̕\��
            object[] attributes = invocation.Method.GetCustomAttributes(false);
            foreach (object o in attributes)
            {
                if ( o is TargetFormAttribute )
                {
                    // �߂�l�̃v���p�e�B�����擾����
                    TargetFormAttribute attribute = (TargetFormAttribute) o;
                    Type formType = attribute.FormType;
                    Form form = (Form) container_.GetComponent(formType);

                    if ( attribute.ReturnPropertyName != "" )
                        propertyName = attribute.ReturnPropertyName;
                    else
                    {
                        if ( returnPropertyName_ != null )
                            propertyName = returnPropertyName_;
                        else
                            propertyName = "";
                    }
                    
                    // �t�H�[���ɒl���Z�b�g����
                    PropertyInfo[] infos = form.GetType().GetProperties();
                    foreach (PropertyInfo info in infos)
                    {
                        hashOfPropNames.Add(info.Name.ToLower(), info.Name);
                    }
                    
                    for ( int i = 0; i < listOfParams.Count; i++ )
                    {
#if NET_1_1
                        if ( hashOfPropNames.ContainsKey((string) listOfParams[i]) )
                        {
                            string propName = (string) hashOfPropNames[(string) listOfParams[i]];
                            PropertyInfo property = form.GetType().GetProperty(propName);
                            property.SetValue(form, hashOfParams[(string) listOfParams[i]], null);
                        }
#else
                        if ( hashOfPropNames.ContainsKey(listOfParams[i]) )
                        {
                            string propName = hashOfPropNames[listOfParams[i]];
                            PropertyInfo property = form.GetType().GetProperty(propName);
                            property.SetValue(form, hashOfParams[listOfParams[i]], null);
                        }                        
#endif

                    }

                    // WindowsForm�̕\��
                    if ( attribute.Mode == ModalType.Modal )
                    {
                        retOfReplace = form.ShowDialog();
                        ret = retOfReplace;

                        // WindowsForm����߂�l�p�v���p�e�B����l���擾����
                        if ( propertyName != "" )
                        {
                            PropertyInfo propInfo = form.GetType().GetProperty(propertyName);
                            if ( propInfo != null )
                                ret = propInfo.GetValue(form, null);
                        }
                    }
                    else
                        form.Show();
                }
            }

            return ret;
        }
    }
}