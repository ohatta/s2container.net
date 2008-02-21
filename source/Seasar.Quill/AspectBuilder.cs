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
using Seasar.Quill.Database.DataSource.Impl;
using Seasar.Quill.Database.Tx;
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
    /// </remarks>
    public class AspectBuilder
    {
        // AspectBuilder���Ŏg�p����QuillContainer
        // (Interceptor���擾����ׂɎg�p����)
        protected QuillContainer container;

        /// <summary>
        /// DaoSetting�^���
        /// </summary>
        protected Type _daoSettingType = null;

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
        /// <param name="targetType"></param>
        /// <param name="methods"></param>
        /// <param name="aspectList"></param>
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
        /// <param name="targetType"></param>
        /// <param name="methods"></param>
        /// <param name="aspectList"></param>
        protected virtual void CreateFromTransactionAttribute(
            Type targetType, MethodInfo[] methods, List<IAspect> aspectList)
        {
            //  Type�Ɏw�肳�ꂽ�g�����U�N�V�������w�肷�鑮�����擾����
            TransactionAttribute txAttrByTypte = AttributeUtil.GetTransactionAttr(targetType);
            if (txAttrByTypte != null)
            {
                IAspect txAspect = CreateTxAspect(txAttrByTypte);
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
        /// <param name="targetType"></param>
        /// <param name="methods"></param>
        /// <param name="aspectList"></param>
        protected virtual void CreateFromS2DaoAttribute(
            Type targetType, MethodInfo[] methods, List<IAspect> aspectList)
        {
            //  Type�Ɏw�肳�ꂽDaoInterceptor���w�肷�鑮�����擾����
            S2DaoAttribute daoAttrByTypte = AttributeUtil.GetS2DaoAttr(targetType);
            if (daoAttrByTypte != null)
            {
                IAspect daoAspect = CreateS2DaoAspect(daoAttrByTypte);
                if (daoAspect != null)
                {
                    aspectList.Add(daoAspect);
                }
            }

            // Method�Ɏw�肳��Ă���Transaction�pAspect�����X�g�ɒǉ�����
            aspectList.AddRange(CreateS2DaoAspectList(methods));
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

            // ���\�b�h�̌������AAspect�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
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
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="methodName">���\�b�h��</param>
        /// <param name="aspectAttr">Aspect����</param>
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
        /// <param name="aspectAttr">Aspect��ݒ肷�鑮��</param>
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
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="methodName">���\�b�h��</param>
        /// <param name="aspectAttr">Aspect����</param>
        protected virtual IAspect[] CreateS2DaoAspectList(MethodInfo[] methods)
        {
            IDictionary<IMethodInterceptor, List<string>> methodNames =
                new Dictionary<IMethodInterceptor, List<string>>();
            foreach (MethodInfo method in methods)
            {
                S2DaoAttribute daoAttr = AttributeUtil.GetS2DaoAttrByMethod(method);
                if (daoAttr != null)
                {
                    AddMethodNamesForS2DaoPointcut(methodNames, method.Name, daoAttr);
                }
            }

            // Aspect�̃��X�g
            List<IAspect> daoList = new List<IAspect>();

            // Interceptor�̌������AAspect���쐬����
            foreach (IMethodInterceptor interceptor in methodNames.Keys)
            {
                // Interceptor�ƃ��\�b�h���̔z�񂩂�Aspect���쐬����
                IAspect aspect = CreateAspect(
                    interceptor, methodNames[interceptor].ToArray());

                // Aspect�̃��X�g�ɒǉ�����
                daoList.Add(aspect);
            }

            // Aspect�̃��X�g��Ԃ�
            return daoList.ToArray();
        }

        /// <summary>
        /// �S�Ẵ��\�b�h��Aspect���L���ƂȂ�Aspect��`���쐬����
        /// </summary>
        /// <param name="aspectAttr">Aspect��ݒ肷�鑮��</param>
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
        /// <param name="aspectAttr">Aspect����</param>
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
        /// Aspect�������m�F����Pointcut���쐬����ׂ̃��\�b�h����ǉ�����
        /// </summary>
        /// <param name="methodNames">
        /// Interceptor����pointcut�ƂȂ郁�\�b�h�����i�[�����R���N�V����
        /// </param>
        /// <param name="methodName">���\�b�h��</param>
        /// <param name="aspectAttr">Aspect����</param>
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
        /// Aspect��������C���^�[�Z�v�^�[���擾����
        /// </summary>
        /// <param name="aspectAttr">Aspect����</param>
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

            QuillComponent txSettingQComponent = container.GetComponent(settingType);
            ITransactionSetting txSetting = 
                (ITransactionSetting)txSettingQComponent.GetComponentObject(settingType);
            if (txSetting.IsNeedSetup())
            {
                //  DataSource�̎擾
                QuillComponent dsQComponent = container.GetComponent(
                    typeof(SelectableDataSourceProxyWithDictionary));
                SelectableDataSourceProxyWithDictionary dataSource =
                     (SelectableDataSourceProxyWithDictionary)dsQComponent.GetComponentObject(
                    typeof(SelectableDataSourceProxyWithDictionary));
                //  �g�����U�N�V�����̐ݒ�
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
        /// Aspect��������C���^�[�Z�v�^�[���擾����
        /// </summary>
        /// <param name="aspectAttr">Aspect����</param>
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

            QuillComponent daoSettingQComponent = container.GetComponent(settingType);
            IDaoSetting daoSetting = (IDaoSetting)daoSettingQComponent.GetComponentObject(
                settingType);

            if (daoSetting.IsNeedSetup())
            {
                //  DataSource�̎擾
                QuillComponent dsQComponent = container.GetComponent(
                    typeof(SelectableDataSourceProxyWithDictionary));
                IDataSource dataSource = (IDataSource)dsQComponent.GetComponentObject(
                    typeof(SelectableDataSourceProxyWithDictionary));
                //  DaoInterceptor���̐ݒ�
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
