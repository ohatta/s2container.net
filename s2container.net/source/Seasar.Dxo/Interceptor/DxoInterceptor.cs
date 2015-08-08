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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Seasar.Dxo.Annotation;
using Seasar.Dxo.Converter;
using Seasar.Dxo.Exception;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Framework.Util;

namespace Seasar.Dxo.Interceptor
{
    /// <summary>
    /// Data Exchange Object�C���^�[�Z�v�^�[
    /// </summary>
    public class DxoInterceptor : AbstractInterceptor
    {
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
                throw new ArgumentNullException(nameof(invocation));

            var methodBase = invocation.Method;
            if (methodBase.IsAbstract)
            {
                var dxoMapping = CreateDxoMapping();
                
                var methodInfo = GetComponentDef(invocation).ComponentType.GetMethod(methodBase.Name);
                var returnType = methodInfo.ReturnType;
                if (!returnType.IsInterface && !returnType.IsAbstract)
                {
                    var args = invocation.Arguments;
                    object dest;
                    if (returnType != typeof (void))
                    {
                        if (returnType.IsArray)
                            dest = ArrayUtil.NewInstance(returnType.GetElementType(), 0);
//                            dest = Array.CreateInstance(returnType.GetElementType(), 0);
                        else
                            dest = ClassUtil.NewInstance(returnType);
//                            dest = Activator.CreateInstance(returnType);
                    }
                    else
                    {
                        if (args.Length > 1)
                            dest = args[1];
                        else
                            throw new DxoException();
                    }
                    if (args.Length == 0)
                        throw new DxoException();
                    var source = args[0];

                    CollectConversionRuleAttribute(dxoMapping, methodInfo);
                    var dateFormat = GetDatePatternFormat(methodInfo);
                    // source���z��̏ꍇ
                    if (source.GetExType().IsArray)
                    {
                        return AssignFromArrayToArray(dxoMapping, source, dest, dateFormat);
                    }
                    else if (source.GetExType().IsGenericType)
                    {
                        if (source.GetExType().IsInterface && dest.GetExType().IsInterface)
                        {
                            if (source.GetExType().Name == "IList" && dest.GetExType().Name == "IList")
                                return AssignFromListToList(dxoMapping, source, dest, dateFormat);
                            else
                                return AssignTo(dxoMapping, source, dest, 0, dateFormat);
                        }
                        else
                        {
                            var srcType = source.GetExType().GetInterface("IList");
                            var destType = dest.GetExType().GetInterface("IList");
                            if (srcType != null && destType != null && srcType == destType)
                                return AssignFromListToList(dxoMapping, source, dest, dateFormat);
                            else
                                return AssignTo(dxoMapping, source, dest, 0, dateFormat);
                        }
                    }
                    else if(dest.GetExType().GetInterface("IDictionary") == typeof(IDictionary))
                   {
                        var converter = ConverterFactory.GetConverter(source, dest.GetExType());
                        converter.Convert(String.Empty, source, ref dest, dest.GetExType());

                        return dest;
                    }
                    else if(!source.GetExType().IsGenericType && dest.GetExType().GetInterface("IList") == typeof(IList))
                    {
                        return AssignFromObjectToList(dxoMapping, source, dest, dateFormat);
                    }
                    else
                    {
                        return AssignTo(dxoMapping, source, dest, 0, dateFormat);
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
        /// �ϊ����[�����̊i�[�ꏊ�𐶐�
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<string, ConversionRuleAttribute> CreateDxoMapping()
        {
            return new Dictionary<string, ConversionRuleAttribute>();
        }

        /// <summary>
        /// �z�񂩂�z��փI�u�W�F�N�g�ւ̃A�T�C�������{����
        /// </summary>
        /// <param name="dxoMapping">Dxo�ϊ����</param>
        /// <param name="source">�ϊ����z��</param>
        /// <param name="dest">�ϊ���z��</param>
        /// <param name="dateFormat">���t����</param>
        protected virtual object AssignFromArrayToArray(IDictionary<string, ConversionRuleAttribute> dxoMapping, 
            object source, object dest, string dateFormat)
        {
            var sourceObjs = source as object[];

            var destObjs = dest as object[];
            if (sourceObjs == null)
                throw new DxoException();
            if (destObjs == null || destObjs.Length == 0)
            {
                var array = ArrayUtil.NewInstance(dest.GetExType().GetElementType(), sourceObjs.Length);
//                var array = Array.CreateInstance(dest.GetExType().GetElementType(), sourceObjs.Length);
                dest = array.Clone();
                destObjs = dest as object[];
            }
            if (destObjs == null || sourceObjs.Length != destObjs.Length)
                throw new DxoException();

            for (var i = 0; i < sourceObjs.Length; i++)
            {
                if (destObjs[i] == null)
                    destObjs[i] = ClassUtil.NewInstance(dest.GetExType().GetElementType());
//                    destObjs[i] = Activator.CreateInstance(dest.GetExType().GetElementType(), false);

                AssignTo(dxoMapping, sourceObjs[i], destObjs[i], 0, dateFormat);
            }
            return dest;
        }

        /// <summary>
        /// IList����IList�փI�u�W�F�N�g�ւ̃A�T�C�������{����
        /// </summary>
        /// <param name="dxoMapping">Dxo�ϊ����</param>
        /// <param name="source">�ϊ����z��</param>
        /// <param name="dest">�ϊ���z��</param>
        /// <param name="dateFormat">���t����</param>
        protected virtual object AssignFromListToList(IDictionary<string, ConversionRuleAttribute> dxoMapping, 
            object source, object dest, string dateFormat)
        {
            var srcList = source as IList;
            var destList = dest as IList;
            if (srcList != null && destList != null)
            {
                foreach (var srcObj in srcList)
                {
                    var types = dest.GetExType().GetGenericArguments();
                    var destObj = ClassUtil.NewInstance(types[0]);
//                    var destObj = Activator.CreateInstance(types[0], false);
                    AssignTo(dxoMapping, srcObj, destObj, 0, dateFormat);
                    destList.Add(destObj);
                }
            }
            return dest;
        }

        /// <summary>
        /// Object����IList�փI�u�W�F�N�g�ւ̃A�T�C�������{����
        /// </summary>
        /// <param name="dxoMapping">Dxo�ϊ����</param>
        /// <param name="source">�ϊ��I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ���z��</param>
        /// <param name="dateFormat">���t����</param>
        protected virtual object AssignFromObjectToList(IDictionary<string, ConversionRuleAttribute> dxoMapping, 
            object source, object dest, string dateFormat)
        {
            var destList = dest as IList;
            if (destList != null)
            {
                var types = dest.GetExType().GetGenericArguments();
                var destObj = ClassUtil.NewInstance(types[0]);
//                var destObj = Activator.CreateInstance(types[0], false);
                AssignTo(dxoMapping, source, destObj, 0, dateFormat);
                destList.Add(destObj);
            }

            return dest;
        }

        /// <summary>
        /// �I�u�W�F�N�g�ւ̃A�T�C�������{���܂�
        /// </summary>
        /// <param name="dxoMapping">Dxo�ϊ����</param>
        /// <param name="source">�ϊ����̃I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ��Ώۂ̃I�u�W�F�N�g</param>
        /// <param name="cnt">�l�X�g�J�E���^�[</param>
        /// <param name="dateFormat">���t����</param>
        protected virtual object AssignTo(IDictionary<string, ConversionRuleAttribute> dxoMapping, object source, object dest, int cnt, string dateFormat)
        {
            if (cnt < 2)
            {
                var properties = source.GetExType().GetProperties();
                foreach (var property in properties)
                {
                    _TryExchangeSameNameProperty(dxoMapping, property, source, dest, dest.GetExType(), cnt, dateFormat);
                }
            }
            return dest;
        }

        /// <summary>
        /// DxoMapping�p������S�Ď擾���āA�����ɕێ����܂�
        /// </summary>
        /// <param name="dxoMapping">Dxo�ϊ����</param>
        /// <param name="method">���s���\�b�h���</param>
        protected virtual void CollectConversionRuleAttribute(IDictionary<string, ConversionRuleAttribute> dxoMapping, MethodInfo method)
        {
            var attrs = method.GetCustomAttributes(typeof(ConversionRuleAttribute), false);
            foreach (ConversionRuleAttribute attr in attrs)
            {
                if (!String.IsNullOrEmpty(attr?.PropertyName))
                {
                    dxoMapping.Add(attr.PropertyName, attr);
                }
            }
        }

        /// <summary>
        /// DatePattern�p������S�Ď擾���āA�����ɕێ����܂��B
        /// </summary>
        /// <param name="method">���s���\�b�h���</param>
        protected virtual string GetDatePatternFormat(MemberInfo method)
        {
            var dateFormat = String.Empty;
            var attrs = method.GetCustomAttributes(typeof (DatePatternAttribute), false);
            foreach (DatePatternAttribute attr in attrs)
            {
                if (!String.IsNullOrEmpty(attr?.Format))
                    dateFormat = attr.Format;
            }
            return dateFormat;
        }

        /// <summary>
        /// �C�ӂ̃v���p�e�B��Ώۂ̃I�u�W�F�N�g�̃v���p�e�B�ɕϊ����܂�
        /// </summary>
        /// <param name="dxoMapping">Dxo�ϊ����</param>
        /// <param name="sourceInfo">�ΏۂƂȂ��Ă���v���p�e�B���</param>
        /// <param name="source">�ΏۂƂȂ��Ă���v���p�e�B�����I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ��Ώۂ̃I�u�W�F�N�g</param>
        /// <param name="destType">�ϊ��Ώۂ̃I�u�W�F�N�g�̌^</param>
        /// <param name="cnt">�l�X�g�J�E���^�[</param>
        /// <param name="dateFormat">���t����</param>
        private void _TryExchangeSameNameProperty(IDictionary<string, ConversionRuleAttribute> dxoMapping,
            PropertyInfo sourceInfo, object source, object dest, Type destType, int cnt, string dateFormat)
        {
            try
            {
                cnt++;
                var targetPropertyName = sourceInfo.Name;
                var existProperty = dxoMapping.ContainsKey(sourceInfo.Name);
                if (existProperty)
                {
                    var targetName = dxoMapping[sourceInfo.Name].TargetPropertyName;
                    if (String.IsNullOrEmpty(targetPropertyName))
                    {
                        targetPropertyName = sourceInfo.Name;
                    }
                    else
                    {
                        targetPropertyName = targetName;
                    }
                }

                //�������O�̃v���p�e�B���ϊ��擯��v���p�e�B�ɂ��邩?
                var destInfo = destType.GetProperty(targetPropertyName);
                if (destInfo != null && destInfo.CanRead && destInfo.CanWrite)
                {
                    _ConvertProperty(dxoMapping, sourceInfo, source, dest, destInfo, existProperty, dateFormat);
                }
                    // �قȂ�ꍇ�A�ċA�Œ�������
                else
                {
                    var srcValue = PropertyUtil.GetValue(source, source.GetExType(), sourceInfo.Name);
//                    var srcValue = sourceInfo.GetValue(source, null);
                    if (srcValue != null)
                    {
                        // �ϊ����𒲍�����
                        if (srcValue.GetExType().Namespace != "System")
                        {
                            AssignTo(dxoMapping, srcValue, dest, cnt, dateFormat);
                        }
                            // �ϊ���𒲍�����
                        else
                        {
                            var properties = destType.GetProperties();
                            foreach (var property in properties)
                            {

                                var destValue = PropertyUtil.GetValue(dest, dest.GetExType(), property.Name);
//                                var destValue = property.GetValue(dest, null);
                                if (destValue != null && destValue.GetExType().BaseType != typeof(ValueType) &&
                                    destValue.GetExType().Namespace != "System")
                                {
                                    AssignTo(dxoMapping, source, destValue, cnt, dateFormat);
                                }
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
        /// <param name="dxoMapping">Dxo�ϊ����</param>
        /// <param name="sourceInfo">�ΏۂƂȂ��Ă���v���p�e�B���</param>
        /// <param name="source">�ΏۂƂȂ��Ă���v���p�e�B�����I�u�W�F�N�g</param>
        /// <param name="dest">�ϊ��Ώۂ̃I�u�W�F�N�g</param>
        /// <param name="destInfo">�ϊ��Ώۂ̃I�u�W�F�N�g���</param>
        /// <param name="existProperty">�����̑��݃t���O</param>
        /// <param name="dateFormat">���t����</param>
        private void _ConvertProperty(IDictionary<string, ConversionRuleAttribute> dxoMapping, PropertyInfo sourceInfo, 
            object source, object dest, PropertyInfo destInfo, bool existProperty, string dateFormat)
        {
//            var sourceValue = sourceInfo.GetValue(source, null);
//            var destValue = destInfo.GetValue(dest, null);
            var sourceValue = PropertyUtil.GetValue(source, source.GetExType(), sourceInfo.Name);
            var destValue = PropertyUtil.GetValue(dest, dest.GetExType(), destInfo.Name);

            if (sourceValue != null)
            {
                IPropertyConverter converter;
                if (existProperty)
                {
                    var attr = dxoMapping[sourceInfo.Name];
                    if (attr.Ignore)
                    {
                        converter = null;
                    }
                    else
                    {
                        if (attr.PropertyConverter != null)
                            converter = ClassUtil.NewInstance(attr.PropertyConverter) as IPropertyConverter;
//                            converter = Activator.CreateInstance(attr.PropertyConverter) as IPropertyConverter;
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
                    converter.Format = (dateFormat ?? String.Empty);
                    _AttachConverterEvent(converter);
                    try
                    {
                        //�R���o�[�^�ɂ��ϊ�
                        converter.Convert(sourceInfo.Name, sourceValue, ref destValue, destInfo.PropertyType);
                        PropertyUtil.SetValue(dest, dest.GetExType(), destInfo.Name, destInfo.PropertyType, destValue);
//                        destInfo.SetValue(dest, destValue, null);
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
            converter.PrepareConvert += PrepareConvert;
            converter.ConvertCompleted += ConvertCompleted;
            converter.ConvertFail += ConvertFail;
        }

        /// <summary>
        /// �R���o�[�^�̃C�x���g���f�^�b�`���܂�
        /// </summary>
        /// <param name="converter">���݂̃R���o�[�^���Z�b�g</param>
        private void _DetachConverterEvent(IPropertyConverter converter)
        {
            converter.PrepareConvert -= PrepareConvert;
            converter.ConvertCompleted -= ConvertCompleted;
            converter.ConvertFail -= ConvertFail;
        }
    }
}
