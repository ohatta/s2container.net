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
using System.Reflection;

namespace Seasar.Dao.Pager
{
    public sealed class PagerConditionUtil
    {
        private PagerConditionUtil()
        {
        }

        /// <summary>
        /// ���\�b�h�̈�����PagerCondition���܂܂�Ă��邩�ǂ����𔻒肵�܂��B
        /// �������A<seealso cref="IPagerCondition.Limit"/> ��
        /// <seealso cref="PagerConditionConstants.NONE_LIMIT"/>�̏ꍇ��false��Ԃ��܂��B
        /// </summary>
        /// <param name="args">����</param>
        /// <returns>true/false</returns>
        public static bool IsPagerDto(object[] args)
        {
            var condition = GetPagerDto(args);
            if (condition == null)
            {
                return false;
            }
            if (condition.Limit == PagerConditionConstants.NONE_LIMIT && condition.Offset == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ���\�b�h��������<seealso cref="IPagerCondition"/>���擾���܂��B
        /// </summary>
        /// <param name="args">����</param>
        /// <returns>IPagerCondition</returns>
        public static IPagerCondition GetPagerDto(object[] args)
        {
            foreach (var arg in args)
            {
                if (arg is IPagerCondition)
                {
                    return arg as IPagerCondition;
                }
            }
            return null;
        }

        /// <summary>
        /// ���\�b�h��`����y�[�W���O�p���\�b�h��`���擾���܂��B�D�揇�ʂ͉��L�ɂȂ�܂��B
        /// 1. ���\�b�h��`��NonPager����(Pagear�����̎擾���~)
        /// 2. ���\�b�h��`��Pager����
        /// 3. �N���X��`��Pager����
        /// </summary>
        /// <returns>PagerAttribute</returns>
        public static PagerAttribute GetPagerAttribute(MethodInfo mi)
        {
            var nonPager = Attribute.GetCustomAttribute(mi, typeof(NonPagerAttribute));
            if (nonPager != null)
             {
                return null;
            }

            var pager = Attribute.GetCustomAttribute(mi, typeof(PagerAttribute)) as PagerAttribute;
            if (pager == null)
            {
                if (mi.DeclaringType != null)
                    pager = Attribute.GetCustomAttribute(mi.DeclaringType, typeof(PagerAttribute)) as PagerAttribute;
            }

            return pager;
        }

        internal static void SetCount(MethodInfo mi, object[] args, int count)
        {
            var pager = GetPagerAttribute(mi);
            if (pager == null)
            {
                return;
            }

            // Pager�����l���y�[�W���O�p�����̃C���f�b�N�X���擾
            var parameters = mi.GetParameters();
            var ci = _FindParameterIndex(pager.CountParameter, parameters);

            if (ci == -1)
            {
                return;
            }

            args[ci] = count;
        }

        /// <summary>
        /// ���\�b�h��`����y�[�W���O�p���\�b�h��`���쐬�B�D�揇�ʂ͉��L�̒ʂ�B
        /// 1. ���\�b�h��`��NonPager����(Pager�����̎擾���~)
        /// 2. ���\�b�h��`��Pager����
        /// 3. �N���X��`��Pager����
        /// </summary>
        public static IPagerCondition CreatePagerDefinition(MethodInfo mi, object[] args)
        {
            // Pager�������擾�ł��Ȃ����null��ԋp
            var pager = GetPagerAttribute(mi);
            if (pager == null)
            {
                return null;
            }

            // Pager�����l���y�[�W���O�p�����̃C���f�b�N�X���擾
            var parameters = mi.GetParameters();
            var li = _FindParameterIndex(pager.LimitParameter, parameters);
            var oi = _FindParameterIndex(pager.OffsetParameter, parameters);
            var ci = _FindParameterIndex(pager.CountParameter, parameters);

            // �y�[�W���O�p�������擾�ł��Ȃ������ꍇ�A��O�𔭐�
            if (li == -1)
            {
                throw new PagingParameterDefinitionException(pager.LimitParameter);
            }
            if (parameters[li].ParameterType != typeof(int))
            {
                throw new PagingParameterDefinitionException(pager.LimitParameter);
            }

            if (oi == -1)
            {
                throw new PagingParameterDefinitionException(pager.OffsetParameter);
            }
            if (parameters[oi].ParameterType != typeof(int))
            {
                throw new PagingParameterDefinitionException(pager.OffsetParameter);
            }

            if (!(parameters[ci].ParameterType.Name == "Int32&" || parameters[ci].IsOut || parameters[ci].ParameterType.IsByRef))
            {
                throw new PagingParameterDefinitionException(pager.CountParameter);
            }

            return new DefaultPagerCondition(
                Convert.ToInt32(args[li]),
                Convert.ToInt32(args[oi])
                );
        }

        private static int _FindParameterIndex(string parameterName, ParameterInfo[] parameters)
        {
            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameterName == parameters[i].Name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
