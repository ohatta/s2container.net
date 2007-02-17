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

namespace Seasar.Framework.Aop
{
	/// <summary>
	/// ���\�b�h�ɑ΂���Interceptor�̃C���^�[�t�F�C�X
	/// </summary>
	/// <remarks>
	/// ���̃C���^�[�t�F�C�X��AOP�A���C�A���X�����B
	/// </remarks>
	/// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
	public interface IMethodInterceptor
	{

		/// <summary>
		/// ���\�b�h��Intercept�����ꍇ�A���̃��\�b�h���Ăяo����܂�
		/// </summary>
		/// <param name="invocation">IMethodInvocation</param>
		/// <returns>Intercept����郁�\�b�h�̖߂�l</returns>
		object Invoke(IMethodInvocation invocation);

	}	// IMethodInterceptor
}
