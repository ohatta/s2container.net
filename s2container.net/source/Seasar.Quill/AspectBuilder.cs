#region Copyright
/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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
using System.Reflection;
using Seasar.Extension.ADO;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Impl;
using Seasar.Quill.Attrs;
using Seasar.Quill.Dao;
using Seasar.Quill.Dao.Interceptor;
using Seasar.Quill.Database.DataSource.Impl;
using Seasar.Quill.Database.Tx;
using Seasar.Quill.Exception;
using Seasar.Quill.Util;

namespace Seasar.Quill
{
    /// <summary>
    /// Aspect��`���\�z����N���X
    /// </summary>
    /// <remarks>
    /// <para>
    /// Aspect��K�p����ꍇ�̓C���^�[�t�F�[�X�E�N���X�������̓��\�b�h��
    /// <see cref="Seasar.Quill.Attrs.AspectAttribute"/>(����)��
    /// �ݒ肳��Ă���K�v������B
    /// </para>
    /// <para>
    /// S2Dao�̋@�\��(dicon�Ȃ���)�g�p����ɂ̓C���^�[�t�F�[�X�E�N���X�������̓��\�b�h��
    /// <see cref="Seasar.Quill.Attrs.S2DaoAttribute"/>(����)��
    /// �ݒ肳��Ă���K�v������B
    /// </para>
    /// <para>
    /// Transaction��(dicon�Ȃ���)�g�p����ɂ̓C���^�[�t�F�[�X�E�N���X�������̓��\�b�h��
    /// <see cref="Seasar.Quill.Attrs.TransactionAttribute"/>(����)��
    /// �ݒ肳��Ă���K�v������B
    /// </para>
    /// </remarks>
    public class AspectBuilder
    {
        // AspectBuilder���Ŏg�p����QuillContainer
        // (Interceptor���擾����ׂɎg�p����)
        protected QuillContainer container;

        /// <summary>
        /// AspectBuilder�����������邽�߂̃R���X�g���N�^
        /// </summary>
        /// <param name="container">AspectBuilder���Ŏg�p����QuillContainer</param>
        public AspectBuilder(QuillContainer container)
        {
            // AspectBuilder���Ŏg�p���邽�߂�QuillContainer��
            // �R���X�g���N�^��������󂯎��
            this.container = container;
        }

        /// <summary>
        /// �w�肳�ꂽ<code>type</code>�̑������m�F����
        /// Aspect��`�̔z����쐬����
        /// </summary>
        /// <param name="type">Aspect��`���m�F����Type</param>
        /// <returns>�쐬���ꂽAspect��`�̔z��</returns>
        public virtual IAspect[] CreateAspects(Type type)
        {
            // Aspect�̃��X�g
            List<IAspect> aspectList = new List<IAspect>();

            // type�Ő錾����Ă��郁�\�b�h���擾����
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | 
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            CreateFromAspectAttribute(type, methods, aspectList);
            CreateFromTransactionAttribute(type, methods, aspectList);
            CreateFromS2DaoAttribute(type, methods, aspectList);

            // Aspect�̔z���Ԃ�
            return aspectList.ToArray();
        }

        /// <summary>
        /// Aspect��������A�X�y�N�g���쐬����
        /// </summary>
        /// <param name="targetType">Aspect��K�p���郁�\�b�h�����^</param>
        /// <param name="methods">Aspect��K�p���郁�\�b�h</param>
        /// <param name="aspectList">�K�p����Aspect�̃��X�g</param>
        protected virtual void CreateFromAspectAttribute(
            Type targetType, MethodInfo[] methods, List<IAspect> aspectList)
        {
            // Type�Ɏw�肳�ꂽAspect���w�肷�鑮�����擾����
            AspectAttribute[] attrsByType = AttributeUtil.GetAspectAttrs(targetType);

            // Type�Ɏw�肳��Ă���Aspect�̌������AAspect�����X�g�ɒǉ�����
            foreach (AspectAttribute attrByType in attrsByType)
            {
                // Pointcut�ɑS�Ẵ��\�b�h���ǉ�����Ă���Aspect���쐬����
                IAspect aspect = CreateAspect(attrByType);

                // �쐬����Aspect�����X�g�ɒǉ�����
                aspectList.Add(aspect);
            }

            // Method�Ɏw�肳��Ă���Aspect�����X�g�ɒǉ�����
            aspectList.AddRange(CreateAspectList(methods));
        }

        /// <summary>
        /// Transaction��������A�X�y�N�g���쐬����
        /// </summary>
        /// <param name="targetType">Aspect��K�p���郁�\�b�h�����^</param>
        /// <param name="methods">Aspect��K�p���郁�\�b�h</param>
        /// <param name="aspectList">�K�p����Aspect�̃��X�g</param>
        protected virtual void CreateFromTransactionAttribute(
            Type targetType, MethodInfo[] methods, List<IAspect> aspectList)
        {
            //  Type�Ɏw�肳�ꂽ�g�����U�N�V�������w�肷�鑮�����擾����
            TransactionAttribute txAttrByType = AttributeUtil.GetTransactionAttr(targetType);
            if (txAttrByType != null)
            {
                IAspect txAspect = CreateTxAspect(txAttrByType);
                if (txAspect != null)
                {
                    aspectList.Add(txAspect);
                }
            }

            // Method�Ɏw�肳��Ă���Transaction�pAspect�����X�g�ɒǉ�����
            aspectList.AddRange(CreateTxAspectList(methods));
        }

        /// <summary>
        /// S2Dao��������A�X�y�N�g���쐬����
        /// </summary>
        /// <param name="targetType">Aspect��K�p���郁�\�b�h�����^</param>
        /// <param name="methods">Aspect��K�p���郁�\�b�h</param>
        /// <param name="aspectList">�K�p����Aspect�̃��X�g</param>
        protected virtual void CreateFromS2DaoAttribute(
            Type targetType, MethodInfo[] methods, List<IAspect> aspectList)
        {
            //  Type�Ɏw�肳�ꂽDaoInterceptor���w�肷�鑮�����擾����
            S2DaoAttribute daoAttrByType = AttributeUtil.GetS2DaoAttr(targetType);

            if (daoAttrByType != null)
            {
                //  �f�[�^�\�[�X�I��Interceptor����`����Ă���ꍇ��
                //  ���Aspect��o�^
                IAspect dataSourceSelctAspect = GetDataSourceSelectAspect(daoAttrByType, targetType);
                if (dataSourceSelctAspect != null)
                {
                    aspectList.Add(dataSourceSelctAspect);
                }

                //  S2DaoInterceptor��o�^
                IAspect daoAspectToClass = CreateS2DaoAspect(daoAttrByType);
                if (daoAspectToClass != null)
                {
                    aspectList.Add(daoAspectToClass);
                }
            }

            // Method�Ɏw�肳��Ă���Transaction�pAspect�����X�g�ɒǉ�����
            IList<IAspect> daoAspectsToMethod = CreateS2DaoAspectList(methods);
            if (daoAspectsToMethod != null && daoAspectsToMethod.Count > 0)
            {
                aspectList.AddRange(daoAspectsToMethod);
            }
        }

        /// <summary>
        /// �f�[�^�\�[�X�I��Interceptor�̎擾
        /// </summary>
        /// <param name="daoAttr">S2Dao����</param>
        /// <param name="targetMember">Interceptor��������Ώ�</param>
        /// <returns>�f�[�^�\�[�X�I��Interceptor</returns>
        protected virtual IMethodInterceptor GetDataSourceSelectInterceptor(S2DaoAttribute daoAttr,
            MemberInfo targetMember)
        {
            DataSourceSelectInterceptor dsInterceptor = null;
            Type daoSettingType = daoAttr.DaoSettingType;
            if (daoSettingType != null)
            {
                IDaoSetting daoSetting = (IDaoSetting)ComponentUtil.GetComponent(container, daoSettingType);

                string dataSourceName = daoSetting.DataSourceName;
                //  �f�[�^�\�[�X������`����Ă����Interceptor������ĕԂ�
                if (string.IsNullOrEmpty(dataSourceName) == false)
                {
                    dsInterceptor = (DataSourceSelectInterceptor)ComponentUtil.GetComponent(
                        container, typeof(DataSourceSelectInterceptor));

                    // �����o�ƃf�[�^�\�[�X����Ή��t����
                    dsInterceptor.DaoDataSourceMap[targetMember] = dataSourceName;

                    //  �f�[�^�\�[�X�����ݒ�̏ꍇ�̓Z�b�g����
                    if (dsInterceptor.DataSourceProxy == null)
                    {
                        SelectableDataSourceProxyWithDictionary ds =
                            (SelectableDataSourceProxyWithDictionary)ComponentUtil.GetComponent(
                            container, typeof(SelectableDataSourceProxyWithDictionary));
                        dsInterceptor.DataSourceProxy = ds;
                    }
                }
            }
            return dsInterceptor;
        }

        /// <summary>
        /// �f�[�^�\�[�X�I��Aspect�̎擾
        /// </summary>
        /// <param name="daoAttr">S2Dao����</param>
        /// <param name="targetType">Aspect��������Ώ�</param>
        /// <returns>�f�[�^�\�[�X�I��Aspect</returns>
        protected virtual IAspect GetDataSourceSelectAspect(S2DaoAttribute daoAttr, MemberInfo targetMember)
        {
            IMethodInterceptor dataSourceSelectInterceptor = GetDataSourceSelectInterceptor(
                daoAttr, targetMember);

            IAspect dataSourceSelectAspect = null;
            if(dataSourceSelectInterceptor != null)
            {
                dataSourceSelectAspect = new AspectImpl(dataSourceSelectInterceptor);
            }
            return dataSourceSelectAspect;
        }

        /// <summary>
        /// �w�肳�ꂽ���\�b�h����Aspect���L���ƂȂ�Aspect��`�̃��X�g���쐬����
        /// </summary>
        /// <param name="methods">���\�b�h���̔z��</param>
        /// <returns>�w�肳�ꂽ���\�b�h��Aspect���L���ƂȂ�Aspect��`�̃��X�g</returns>
        protected virtual IList<IAspect> CreateAspectList(MethodInfo[] methods)
        {
            // Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[����
            IDictionary<IMethodInterceptor, List<string>> methodNames =
                new Dictionary<IMethodInterceptor, List<string>>();

            // Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[����
            foreach (MethodInfo method in methods)
            {
                // Aspect�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
                AddMethodNamesForPointcut(methodNames, method);
            }

            // Aspect�̃��X�g
            List<IAspect> aspectList = new List<IAspect>();

            // Interceptor�̌������AAspect���쐬����
            foreach (IMethodInterceptor interceptor in methodNames.Keys)
            {
                // Interceptor�ƃ��\�b�h���̔z�񂩂�Aspect���쐬����
                IAspect aspect = CreateAspect(
                    interceptor, methodNames[interceptor].ToArray());

                // Aspect�̃��X�g�ɒǉ�����
                aspectList.Add(aspect);
            }

            // Aspect�̃��X�g��Ԃ�
            return aspectList.ToArray();
        }

        /// <summary>
        /// �S�Ẵ��\�b�h��Aspect���L���ƂȂ�Aspect��`���쐬����
        /// </summary>
        /// <param name="aspectAttr">Aspect��ݒ肷�鑮��</param>
        /// <returns>�S�Ẵ��\�b�h��Aspect���L���ƂȂ�Aspect��`</returns>
        protected virtual IAspect CreateAspect(AspectAttribute aspectAttr)
        {
            // Interceptor���쐬����
            IMethodInterceptor interceptor = GetMethodInterceptor(aspectAttr);

            // Interceptor����Aspect���쐬����
            // (Pointcut�͎w�肵�Ȃ��̂őS�Ẵ��\�b�h���ΏۂƂȂ�)
            IAspect aspect = new AspectImpl(interceptor);

            // Aspect��Ԃ�
            return aspect;
        }

        /// <summary>
        /// �C���^�[�Z�v�^�[�ƃ��\�b�h���w�肵��Aspect��`���쐬����
        /// </summary>
        /// <param name="interceptor">�C���^�[�Z�v�^�[</param>
        /// <param name="methodNames">Aspect��K�p���郁�\�b�h���̔z��</param>
        /// <returns>�w�肳�ꂽ���\�b�h��Aspect���L���ƂȂ�Aspect��`</returns>
        protected virtual IAspect CreateAspect(
            IMethodInterceptor interceptor, string[] methodNames)
        {
            // Pointcut���쐬����
            IPointcut pointcut = new PointcutImpl(methodNames);

            // Interceptor��Pointcut����Aspect���쐬����
            IAspect aspect = new AspectImpl(interceptor, pointcut);

            // Aspect��Ԃ�
            return aspect;
        }

        /// <summary>
        /// Aspect�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
        /// </summary>
        /// <param name="methods">�ΏۃN���X�̃��\�b�h���</param>
        /// <returns>�K�p����Aspect�̃��X�g</returns>
        protected virtual IAspect[] CreateTxAspectList(MethodInfo[] methods)
        {
            IDictionary<IMethodInterceptor, List<string>> methodNames =
                new Dictionary<IMethodInterceptor, List<string>>();
            foreach ( MethodInfo method in methods )
            {
                TransactionAttribute txAttr = AttributeUtil.GetTransactionAttrByMethod(method);
                if ( txAttr != null )
                {
                    AddMethodNamesForTxPointcut(methodNames, method.Name, txAttr);
                }
            }

            // Aspect�̃��X�g
            List<IAspect> txList = new List<IAspect>();

            // Interceptor�̌������AAspect���쐬����
            foreach ( IMethodInterceptor interceptor in methodNames.Keys )
            {
                // Interceptor�ƃ��\�b�h���̔z�񂩂�Aspect���쐬����
                IAspect aspect = CreateAspect(
                    interceptor, methodNames[interceptor].ToArray());

                // Aspect�̃��X�g�ɒǉ�����
                txList.Add(aspect);
            }

            // Aspect�̃��X�g��Ԃ�
            return txList.ToArray();
        }

        /// <summary>
        /// �S�Ẵ��\�b�h��Aspect���L���ƂȂ�Aspect��`���쐬����
        /// </summary>
        /// <param name="txAttr">Aspect��ݒ肷�鑮��</param>
        /// <returns>�S�Ẵ��\�b�h��Aspect���L���ƂȂ�Aspect��`</returns>
        protected virtual IAspect CreateTxAspect(TransactionAttribute txAttr)
        {
            // Interceptor���쐬����
            IMethodInterceptor interceptor = GetMethodInterceptor(txAttr);

            // Interceptor����Aspect���쐬����
            // (Pointcut�͎w�肵�Ȃ��̂őS�Ẵ��\�b�h���ΏۂƂȂ�)
            IAspect aspect = new AspectImpl(interceptor);

            // Aspect��Ԃ�
            return aspect;
        }

        /// <summary>
        /// ���\�b�h��񂩂�ǉ����邽�߂�Aspect�̃��X�g���쐬����
        /// </summary>
        /// <param name="methods">�ΏۃN���X�̃��\�b�h���</param>
        /// <returns>�K�p����Aspect�̃��X�g</returns>
        protected virtual IAspect[] CreateS2DaoAspectList(MethodInfo[] methods)
        {
            // �f�[�^�\�[�X�I��Interceptor�K�p���\�b�h���R���N�V����
            IDictionary<IMethodInterceptor, List<string>> dataSourceSelectMethodNames =
                new Dictionary<IMethodInterceptor, List<string>>();
            // S2DaoInterceptor�K�p���\�b�h���R���N�V����
            IDictionary<IMethodInterceptor, List<string>> daoMethodNames =
                new Dictionary<IMethodInterceptor, List<string>>();
            //  ���\�b�h����Intarceptor�̑Ή��t��
            foreach (MethodInfo method in methods)
            {
                S2DaoAttribute daoAttr = AttributeUtil.GetS2DaoAttrByMethod(method);
                if (daoAttr != null)
                {
                    AddMethodNamesForDataSourceSelectPointcut(dataSourceSelectMethodNames, method, daoAttr);
                    AddMethodNamesForS2DaoPointcut(daoMethodNames, method.Name, daoAttr);
                }
            }

            // Aspect�̃��X�g
            List<IAspect> daoAspectList = new List<IAspect>();

            //  �f�[�^�\�[�X�I��Interceptor�͐�ɓo�^
            foreach (IMethodInterceptor dataSourceSelectInterceptor in dataSourceSelectMethodNames.Keys)
            {
                // Interceptor�ƃ��\�b�h���̔z�񂩂�Aspect���쐬����
                IAspect dataSourceSelectAspect = CreateAspect(dataSourceSelectInterceptor,
                    dataSourceSelectMethodNames[dataSourceSelectInterceptor].ToArray());

                // Aspect�̃��X�g�ɒǉ�����
                daoAspectList.Add(dataSourceSelectAspect);
            }

            // Interceptor�̌������AAspect���쐬����
            foreach (IMethodInterceptor daoInterceptor in daoMethodNames.Keys)
            {
                // Interceptor�ƃ��\�b�h���̔z�񂩂�Aspect���쐬����
                IAspect daoAspect = CreateAspect(
                    daoInterceptor, daoMethodNames[daoInterceptor].ToArray());

                // Aspect�̃��X�g�ɒǉ�����
                daoAspectList.Add(daoAspect);
            }

            // Aspect�̃��X�g��Ԃ�
            return daoAspectList.ToArray();
        }

        /// <summary>
        /// �S�Ẵ��\�b�h��Aspect���L���ƂȂ�Aspect��`���쐬����
        /// </summary>
        /// <param name="daoAttr">Aspect��ݒ肷�鑮��</param>
        /// <returns>�S�Ẵ��\�b�h��Aspect���L���ƂȂ�Aspect��`</returns>
        protected virtual IAspect CreateS2DaoAspect(S2DaoAttribute daoAttr)
        {
            // Interceptor���쐬����
            IMethodInterceptor interceptor = GetMethodInterceptor(daoAttr);

            // Interceptor����Aspect���쐬����
            // (Pointcut�͎w�肵�Ȃ��̂őS�Ẵ��\�b�h���ΏۂƂȂ�)
            IAspect aspect = new AspectImpl(interceptor);

            // Aspect��Ԃ�
            return aspect;
        }

        /// <summary>
        /// Aspect�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
        /// </summary>
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="method">���\�b�h���</param>
        protected void AddMethodNamesForPointcut(
            IDictionary<IMethodInterceptor, List<string>> methodNames, MethodInfo method)
        {
            // ���\�b�h�Ɏw�肳��Ă���Aspect�������擾����
            AspectAttribute[] aspectAttrs =
                AttributeUtil.GetAspectAttrsByMethod(method);

            // Aspect�����̌������APointcut���쐬����ׂ̃��\�b�h����ǉ�����
            foreach (AspectAttribute aspectAttr in aspectAttrs)
            {
                // Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
                AddMethodNamesForPointcut(methodNames, method.Name, aspectAttr);
            }
        }

        /// <summary>
        /// Aspect�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
        /// </summary>
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="methodName">���\�b�h��</param>
        /// <param name="aspectAttr">Aspect����</param>
        protected void AddMethodNamesForPointcut(
            IDictionary<IMethodInterceptor, List<string>> methodNames,
             string methodName, AspectAttribute aspectAttr)
        {
            // �C���^�[�Z�v�^�[���擾����
            IMethodInterceptor interceptor = GetMethodInterceptor(aspectAttr);

            if (!methodNames.ContainsKey(interceptor))
            {
                // �n�߂Ă�Interceptor�̏ꍇ��string�̃��X�g������������
                methodNames.Add(interceptor, new List<string>());
            }

            // ���\�b�h����ǉ�����
            methodNames[interceptor].Add(methodName);
        }

        /// <summary>
        /// Aspect�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
        /// </summary>
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="methodName">���\�b�h��</param>
        /// <param name="txAttr">Aspect����</param>
        protected void AddMethodNamesForTxPointcut(
            IDictionary<IMethodInterceptor, List<string>> methodNames,
             string methodName, TransactionAttribute txAttr)
        {
            // �C���^�[�Z�v�^�[���擾����
            IMethodInterceptor interceptor = GetMethodInterceptor(txAttr);
            if ( !methodNames.ContainsKey(interceptor) )
            {
                // �n�߂Ă�Interceptor�̏ꍇ��string�̃��X�g������������
                methodNames.Add(interceptor, new List<string>());
            }

            // ���\�b�h����ǉ�����
            methodNames[interceptor].Add(methodName);
        }

        /// <summary>
        /// S2Dao�����̃f�[�^�\�[�X����`���m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
        /// </summary>
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="method">Aspect��K�p���郁�\�b�h���</param>
        /// <param name="daoAttr">S2Dao����</param>
        protected void AddMethodNamesForDataSourceSelectPointcut(
            IDictionary<IMethodInterceptor, List<string>> methodNames, 
            MethodInfo method, S2DaoAttribute daoAttr)
        {
            //  �f�[�^�\�[�X�I��Interceptor�̎擾
            IMethodInterceptor dataSourceSelectInterceptor =
                GetDataSourceSelectInterceptor(daoAttr, method);

            if (dataSourceSelectInterceptor != null)
            {
                if (!methodNames.ContainsKey(dataSourceSelectInterceptor))
                {
                    // �n�߂Ă�Interceptor�̏ꍇ��string�̃��X�g������������
                    methodNames.Add(dataSourceSelectInterceptor, new List<string>());
                }
                methodNames[dataSourceSelectInterceptor].Add(method.Name);
            }
        }

        /// <summary>
        /// S2Dao�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
        /// </summary>
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="methodName">���\�b�h��</param>
        /// <param name="daoAttr">S2Dao����</param>
        protected void AddMethodNamesForS2DaoPointcut(
            IDictionary<IMethodInterceptor, List<string>> methodNames,
             string methodName, S2DaoAttribute daoAttr)
        {
            // �C���^�[�Z�v�^�[���擾����
            IMethodInterceptor interceptor = GetMethodInterceptor(daoAttr);

            if (!methodNames.ContainsKey(interceptor))
            {
                // �n�߂Ă�Interceptor�̏ꍇ��string�̃��X�g������������
                methodNames.Add(interceptor, new List<string>());
            }

            // ���\�b�h����ǉ�����
            methodNames[interceptor].Add(methodName);
        }

        /// <summary>
        /// Aspect��������C���^�[�Z�v�^�[���擾����
        /// </summary>
        /// <param name="aspectAttr">Aspect����</param>
        /// <returns>�C���^�[�Z�v�^�[</returns>
        protected virtual IMethodInterceptor GetMethodInterceptor(
            AspectAttribute aspectAttr)
        {
            if (aspectAttr.InterceptorType != null)
            {
                // interceptorType���w�肳��Ă���ꍇ��
                // Quill����Type���w�肵�ăC���^�[�Z�v�^�[���擾����
                return GetMethodInterceptor(aspectAttr.InterceptorType);
            }
            else if (aspectAttr.ComponentName != null)
            {
                // �R���|�[�l���g�����w�肳��Ă���ꍇ��
                // S2Container����R���|�[�l���g�����w�肵�ăC���^�[�Z�v�^�[���擾����
                return GetMethodInterceptor(aspectAttr.ComponentName);
            }
            else
            {
                // Aspect������interceptorType��componentName�̂ǂ���̎w���
                // ����Ă��Ȃ��ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0013");
            }
        }

        /// <summary>
        /// Quill����Type���w�肵�ăC���^�[�Z�v�^�[���擾����
        /// </summary>
        /// <param name="interceptorType">�C���^�[�Z�v�^�[��Type</param>
        /// <returns>�C���^�[�Z�v�^�[</returns>
        protected virtual IMethodInterceptor GetMethodInterceptor(
            Type interceptorType)
        {
            // Interceptor�̃R���|�[�l���g���擾����
            QuillComponent component =
                container.GetComponent(interceptorType);

            if (typeof(IMethodInterceptor).IsAssignableFrom(component.ComponentType))
            {
                // IMethodInterceptor�ɑ�����ł���ꍇ��Interceptor��Ԃ�
                return (IMethodInterceptor)component.GetComponentObject(interceptorType);
            }
            else
            {
                // IMethodInterceptor�ɑ���ł��Ȃ��ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0012",
                    new object[] { component.ComponentType.FullName });
            }
        }

        /// <summary>
        /// S2Container����R���|�[�l���g�����w�肵�ăC���^�[�Z�v�^�[���擾����
        /// </summary>
        /// <param name="componentName">�R���|�[�l���g��</param>
        /// <returns>�C���^�[�Z�v�^�[</returns>
        protected virtual IMethodInterceptor GetMethodInterceptor(
            string componentName)
        {
            // S2Container����R���|�[�l���g�̃I�u�W�F�N�g���擾����
            object interceptor =
                SingletonS2ContainerConnector.GetComponent(componentName);

            // �C���^�[�Z�v�^�[��Type���擾����
            Type type = TypeUtil.GetType(interceptor);

            if (typeof(IMethodInterceptor).IsAssignableFrom(type))
            {
                // IMethodInterceptor�ɑ�����ł���ꍇ��Interceptor��Ԃ�
                return (IMethodInterceptor)interceptor;
            }
            else
            {
                // IMethodInterceptor�ɑ���ł��Ȃ��ꍇ�͗�O���X���[����
                throw new QuillApplicationException("EQLL0012",
                    new object[] { type.FullName });
            }
        }

        /// <summary>
        /// Transaction��������C���^�[�Z�v�^�[���擾����
        /// </summary>
        /// <param name="txAttr">Transaction����</param>
        /// <returns>�C���^�[�Z�v�^�[</returns>
        protected virtual IMethodInterceptor GetMethodInterceptor(
            TransactionAttribute txAttr)
        {
            Type settingType = txAttr.TransactionSettingType;
            if (settingType == null)
            {
                // �g�����U�N�V�����ݒ肪�w�肳��Ă��Ȃ��ꍇ��
                // ��O���X���[����
                throw new QuillApplicationException("EQLL0013");
            }

            ITransactionSetting txSetting =
                (ITransactionSetting)ComponentUtil.GetComponent(container, settingType);
            if (txSetting.IsNeedSetup())
            {
                //  DataSource�̎擾
                IDataSource dataSource = (IDataSource)ComponentUtil.GetComponent(
                    container, typeof(SelectableDataSourceProxyWithDictionary));
                //  �g�����U�N�V�����֌W�̐ݒ�
                txSetting.Setup(dataSource);
            }

            if (txSetting.TransactionInterceptor == null)
            {
                //  Interceptor������Ă��Ȃ��ꍇ�͗�O�Ƃ���
                throw new QuillApplicationException("EQLL0024");
            }
            return txSetting.TransactionInterceptor;
        }

        /// <summary>
        /// S2Dao��������C���^�[�Z�v�^�[���擾����
        /// </summary>
        /// <param name="daoAttr">S2Dao����</param>
        /// <returns>�C���^�[�Z�v�^�[</returns>
        protected virtual IMethodInterceptor GetMethodInterceptor(
            S2DaoAttribute daoAttr)
        {
            Type settingType = daoAttr.DaoSettingType;
            if (settingType == null)
            {
                // �g�p����Handler���w�肳��Ă��Ȃ��ꍇ��
                // ��O���X���[����
                throw new QuillApplicationException("EQLL0013");
            }

            IDaoSetting daoSetting = (IDaoSetting)ComponentUtil.GetComponent(
                container, settingType);

            if (daoSetting.IsNeedSetup())
            {
                //  DataSource�̎擾
                IDataSource dataSource = (IDataSource)ComponentUtil.GetComponent(
                    container, typeof(SelectableDataSourceProxyWithDictionary));
                //  S2DaoInterceptor���̐ݒ�
                daoSetting.Setup(dataSource);
            }

            if (daoSetting.DaoInterceptor == null)
            {
                //  Interceptor������Ă��Ȃ��ꍇ�͗�O�Ƃ���
                throw new QuillApplicationException("EQLL0023");
            }
            return daoSetting.DaoInterceptor;
        }
    }
}
