#region using directives

using System;
using System.Text;
using System.Reflection;
using System.Collections;

using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Interceptors;

using Castle.DynamicProxy;

#endregion

namespace Seasar.Framework.Aop.Impl
{
    /// <summary>
    /// ������Advice(Interceptor)�ɂ��`�F�[���𒊏ۉ������C���^�t�F�[�X�̎����N���X�ł�
    /// </summary>
    /// <author>Kazz</author>
    /// <version>1.3 2006/05/23</version>
    ///
    public class DynamicProxyMethodInvocation : IS2MethodInvocation
    {
        #region fields

        private Object target;
        private Type targetType;
        private IInvocation invocation;
        private IMethodInterceptor[] interceptors;
        private int interceptorsIndex = 1;
        private Object[] arguments;
        private Hashtable parameters;

        #endregion

        #region constructors

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="target">�Ώۂ̃I�u�W�F�N�g���Z�b�g</param>
        /// <param name="targetType">�Ώۂ̌^���Z�b�g</param>
        /// <param name="invocation">IInvocation�C���^�t�F�[�X���Z�b�g</param>
        /// <param name="interceptors">�C���^�[�Z�v�^�̔z����Z�b�g</param>
        public DynamicProxyMethodInvocation(object target
                                            , Type targetType
                                            , IInvocation invocation
                                            , object[] arguments
                                            , IMethodInterceptor[] interceptors
                                            , Hashtable parameters)
        {
            if (target == null) throw new NullReferenceException("target");
            if (targetType == null) throw new NullReferenceException("target");
            if (invocation == null) throw new NullReferenceException("invocation");
            if (interceptors == null) throw new NullReferenceException("interceptors");
            this.target = target;
            this.targetType = targetType;
            this.invocation = invocation;
            this.arguments = arguments;
            this.interceptors = interceptors;
            this.parameters = parameters;
        }

        #endregion

        #region IMethodInvocation member

        public MethodBase Method
        {
            get { return this.invocation.Method; }
        }

        public Object Target
        {
            get { return this.target; }
        }

        public Object[] Arguments
        {
            get { return this.arguments; }
        }

        public Object Proceed()
        {
            while (interceptorsIndex < interceptors.Length)
            {
                return interceptors[interceptorsIndex++].Invoke(this);
            }
            return this.invocation.Proceed(arguments);
        }

        #endregion

        #region IS2MethodInvocation �����o

        public Type TargetType
        {
            get { return this.targetType; }
        }

        public object GetParameter(string name)
        {
            return this.parameters[name];
        }

        #endregion
    }
}
