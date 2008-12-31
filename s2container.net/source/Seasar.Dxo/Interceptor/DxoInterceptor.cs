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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Seasar.Dxo.Annotation;
using Seasar.Dxo.Converter;
using Seasar.Dxo.Exception;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Interceptors;

namespace Seasar.Dxo.Interceptor
{
    /// <summary>
    /// Data Exchange Object�C���^�[�Z�v�^�[
    /// </summary>
    public class DxoInterceptor : AbstractInterceptor
    {
        private string _dateFormat;
        private IDictionary<string, ConversionRuleAttribute> _dxoMapping;

        /// <summary>
        /// DxoMapping�p�̎擾�A�m�e�[�V�����̃R���N�V����
        /// </summary>
        public IDictionary<string, ConversionRuleAttribute> DxoMapping
        {
            get { return _dxoMapping; }
        }

        public event EventHandler<ConvertEventArgs> PrepareConvert;
        public event EventHandler<ConvertEventArgs> ConvertCompleted;
        public event EventHandler<ConvertEventArgs> ConvertFail;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public override object Invoke(IMethodInvocation invocation)
        {
            if (invocation == null)
                throw new ArgumentNullException("invocation");

            MethodBase methodBase = invocation.Method;
            if (methodBase.IsAbstract)
            {
                MethodInfo methodInfo = GetComponentDef(invocation).ComponentType.GetMethod(methodBase.Name);
                Type type = methodInfo.ReturnType;
                if (!type.IsInterface && !type.IsAbstract)
                {
                    object[] args = invocation.Arguments;
                    object dest;
                    if (type != typeof (void))
                    {
                        if (type.IsArray)
                            dest = Array.CreateInstance(type.GetElementType(), 0);
                        else
                            dest = Activator.CreateInstance(type);
                    }
                    else
                    {
                        if (args.Length > 1)
                            dest = args[1];
                        else
                            throw new DxoException();
                    }
                    object source;
                    if (args.Length == 0)
                        throw new DxoException();
                    else
                        source = args[0];

                    _CollectConversionRuleAttribute(methodInfo);
                    _CollectDatePatternMapping(methodInfo);
                    // source���z��̏ꍇ
                    if (source.GetType().IsArray)
                    {
                        return AssignFromArrayToArray(source, dest);
                    }
                    else if (source.GetType().IsGenericType)
                    {
                        if (source.GetType().IsInterface && dest.GetType().IsInterface)
                        {
                            if (source.GetType().Name == "IList" && dest.GetType().Name == "IList")
                                return AssignFromListToList(source, dest);
                            else
                                return AssignTo(source, dest);
                        }
                        else
                        {
                            Type srcType = source.GetType().GetInterface("IList");
                            Type destType = dest.GetType().GetInterface("IList");
                            if (srcType != null && destType != null && srcType == destType)
                                return AssignFromListToList(source, dest);
                            else
                                return AssignTo(source, dest);
                        }
                    }
                    else if(dest.GetType().GetInterface("IDictionary") == typeof(IDictionary))
                   {
                        IPropertyConverter converter = ConverterFactory.GetConverter(source, dest.GetType());
                        converter.Convert(String.Empty, source, ref dest, dest.GetType());

                        return dest;
                    }
                    else if(!source.GetType().IsGenericType && dest.GetType().GetInterface("IList") == typeof(IList))
                    {
                        return AssignFromObjectToList(source, dest);
                    }
                    else
                    {
                        return AssignTo(source, dest);
                    }
                }
                else
                {
                    return invocation.Proceed();
                }
            }
            else
            {
                return invocation.Proceed();
            }
        }

        /// <summary>
        /// �z�񂩂�z��փI�u�W�F�N�g�ւ̃A�T�C�������{����
        /// </summary>
        /// <param name="source">�ϊ����z��</param>
        /// <param name="dest">�ϊ���z��</param>
        protected virtual object AssignFromArrayToArray(object source, object dest)
        {
            object[] sourceObjs = source as object[];

            object[] destObjs = dest as object[];
            if (sourceObjs == null)
                throw new DxoException();
            if (destObjs == null || destObjs.Length == 0)
            {
                Array array = Array.CreateInstance(dest.GetType().GetElementType(), sourceObjs.Length);
                dest = array.Clone();
                destObjs = dest as object[];
            }
            if (destObjs == null || sourceObjs.Length != destObjs.Length)
                throw new DxoException();

            for (int i = 0; i < sourceObjs.Length; i++)
            {
                if (destObjs[i] == null)
                    destObjs[i] = Activator.CreateInstance(dest.GetType().GetElementType(), false);

                AssignTo(sourceObjs[i], destObjs[i]);
            }
            return dest;
        }

        /// <summary>
        /// IList����IList�փI�u�W�F�N�g�ւ̃A�T�C�������{����
        /// </summary>
        /// <param name="source">�ϊ����z��</param>
        /// <param name="dest">�ϊ���z��</param>
        protected virtual object AssignFromListToList(object source, object dest)
        {
            IList srcList = source as IList;
            IList destList = dest as IList;
            if (srcList != null && destList != null)
            {
                foreach (object srcObj in srcList)
                {
                    Type[] types = dest.GetType().GetGenericArguments();
                    object destObj = Activator.CreateInstance(types[0], false);
                    AssignTo(srcObj, destObj);
                    destList.Add(destObj);
                }
            }
            return dest;
        }

        /// <summary>
        /// Object����IList�փI�u�W�F�N�g�ւ̃A�T�C�������{����
        /// </summary>
        /// <param name="source">�ϊ��I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ���z��</param>
        protected virtual object AssignFromObjectToList(object source, object dest)
        {
            IList destList = dest as IList;
            if (destList != null)
            {
                Type[] types = dest.GetType().GetGenericArguments();
                object destObj = Activator.CreateInstance(types[0], false);
                AssignTo(source, destObj);
                destList.Add(destObj);
            }

            return dest;
        }

        /// <summary>
        /// �I�u�W�F�N�g�ւ̃A�T�C�������{���܂�
        /// </summary>
        /// <param name="source">�ϊ����̃I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ��Ώۂ̃I�u�W�F�N�g</param>
        protected virtual object AssignTo(object source, object dest)
        {
            PropertyInfo[] properties = source.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                _TryExchangeSameNameProperty(property, source, dest, dest.GetType());
            }
            return dest;
        }

        /// <summary>
        /// DxoMapping�p������S�Ď擾���āA�����ɕێ����܂�
        /// </summary>
        /// <param name="method">���s���\�b�h���</param>
        private void _CollectConversionRuleAttribute(MethodInfo method)
        {
            if (_dxoMapping == null)
                _dxoMapping = new Dictionary<string, ConversionRuleAttribute>();

            if (_dxoMapping.Count > 0)
                _dxoMapping.Clear();

            lock (_dxoMapping)
            {
                object[] attrs = method.GetCustomAttributes(typeof (ConversionRuleAttribute), false);
                foreach (ConversionRuleAttribute attr in attrs)
                {
                    if (attr != null && !String.IsNullOrEmpty(attr.PropertyName))
                        _dxoMapping.Add(attr.PropertyName, attr);
                }
            }
        }

        /// <summary>
        /// DatePattern�p������S�Ď擾���āA�����ɕێ����܂��B
        /// </summary>
        /// <param name="method">���s���\�b�h���</param>
        private void _CollectDatePatternMapping(MemberInfo method)
        {
            _dateFormat = String.Empty;
            object[] attrs = method.GetCustomAttributes(typeof (DatePatternAttribute), false);
            foreach (DatePatternAttribute attr in attrs)
            {
                if (attr != null && !String.IsNullOrEmpty(attr.Format))
                    _dateFormat = attr.Format;
            }
        }

        /// <summary>
        /// �C�ӂ̃v���p�e�B��Ώۂ̃I�u�W�F�N�g�̃v���p�e�B�ɕϊ����܂�
        /// </summary>
        /// <param name="sourceInfo">�ΏۂƂȂ��Ă���v���p�e�B���</param>
        /// <param name="source">�ΏۂƂȂ��Ă���v���p�e�B�����I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ��Ώۂ̃I�u�W�F�N�g</param>
        /// <param name="destType">�ϊ��Ώۂ̃I�u�W�F�N�g�̌^</param>
        private void _TryExchangeSameNameProperty(PropertyInfo sourceInfo, object source, object dest, Type destType)
        {
            try
            {
                PropertyInfo destInfo;
                string targetPropertyName = sourceInfo.Name;
                bool existProperty = _dxoMapping.ContainsKey(sourceInfo.Name);
                if (existProperty)
                {
                    string targetName = _dxoMapping[sourceInfo.Name].TargetPropertyName;
                    if (String.IsNullOrEmpty(targetPropertyName))
                        targetPropertyName = sourceInfo.Name;
                    else
                        targetPropertyName = targetName;
                }

                //�������O�̃v���p�e�B���ϊ��擯��v���p�e�B�ɂ��邩?
                destInfo = destType.GetProperty(targetPropertyName);
                if (destInfo != null && destInfo.CanRead && destInfo.CanWrite)
                {
                    _ConvertProperty(sourceInfo, source, dest, destInfo, existProperty);
                }
                    // �قȂ�ꍇ�A�ċA�Œ�������
                else
                {
                    object srcValue = sourceInfo.GetValue(source, null);
                    if (srcValue != null)
                    {
                        // �ϊ����𒲍�����
                        if (srcValue.GetType().Namespace != "System")
                        {
                            AssignTo(srcValue, dest);
                        }
                            // �ϊ���𒲍�����
                        else
                        {
                            PropertyInfo[] properties = destType.GetProperties();
                            foreach (PropertyInfo property in properties)
                            {
                                object destValue = property.GetValue(dest, null);
                                if (destValue != null && destValue.GetType().BaseType != typeof (ValueType) &&
                                    destValue.GetType().Namespace != "System")
                                    AssignTo(source, destValue);
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                throw new DxoException(e.Message, e);
            }
        }

        /// <summary>
        /// �������O�̃v���p�e�B���ϊ��擯��v���p�e�B�ɂ���Ƃ��ɃR���o�[�g����
        /// </summary>
        /// <param name="sourceInfo">�ΏۂƂȂ��Ă���v���p�e�B���</param>
        /// <param name="source">�ΏۂƂȂ��Ă���v���p�e�B�����I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ��Ώۂ̃I�u�W�F�N�g</param>
        /// <param name="destInfo">�ϊ��Ώۂ̃I�u�W�F�N�g���</param>
        /// <param name="existProperty">�����̑��݃t���O</param>
        private void _ConvertProperty(PropertyInfo sourceInfo, object source, object dest, PropertyInfo destInfo,
                                      bool existProperty)
        {
            object sourceValue = sourceInfo.GetValue(source, null);
            object destValue = destInfo.GetValue(dest, null);

            if (sourceValue != null)
            {
                IPropertyConverter converter;
                if (existProperty)
                {
                    ConversionRuleAttribute attr = _dxoMapping[sourceInfo.Name];
                    if (attr.Ignore)
                    {
                        converter = null;
                    }
                    else
                    {
                        if (attr.PropertyConverter != null)
                            converter = Activator.CreateInstance(attr.PropertyConverter) as IPropertyConverter;
                        else
                            converter = ConverterFactory.GetConverter(sourceValue, destInfo.PropertyType);
                    }
                }
                else
                {
                    //�R���o�[�^���t�@�N�g������擾
                    converter = ConverterFactory.GetConverter(sourceValue, destInfo.PropertyType);
                }
                if (converter != null)
                {
                    if (!String.IsNullOrEmpty(_dateFormat))
                        converter.Format = _dateFormat;

                    _AttachConverterEvent(converter);
                    try
                    {
                        //�R���o�[�^�ɂ��ϊ�
                        converter.Convert(sourceInfo.Name, sourceValue, ref destValue, destInfo.PropertyType);
                        destInfo.SetValue(dest, destValue, null);
                    }
                    finally
                    {
                        _DetachConverterEvent(converter);
                    }
                }
            }
        }

        /// <summary>
        /// �R���o�[�^�̃C�x���g���A�^�b�`���܂�
        /// </summary>
        /// <param name="converter">���݂̃R���o�[�^</param>
        private void _AttachConverterEvent(IPropertyConverter converter)
        {
            converter.PrepareConvert += this.PrepareConvert;
            converter.ConvertCompleted += this.ConvertCompleted;
            converter.ConvertFail += this.ConvertFail;
        }

        /// <summary>
        /// �R���o�[�^�̃C�x���g���f�^�b�`���܂�
        /// </summary>
        /// <param name="converter">���݂̃R���o�[�^���Z�b�g</param>
        private void _DetachConverterEvent(IPropertyConverter converter)
        {
            converter.PrepareConvert -= this.PrepareConvert;
            converter.ConvertCompleted -= this.ConvertCompleted;
            converter.ConvertFail -= this.ConvertFail;
        }
    }
}