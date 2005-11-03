#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
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

namespace Seasar.Framework.Container.Impl
{
	/// <summary>
	/// 1�̃L�[�ɕ����̃R���|�[�l���g���o�^���ꂽ�ꍇ�ɗ��p����܂��B
	/// </summary>
	public class TooManyRegistrationComponentDef : SimpleComponentDef
	{
		private object key_;
		private ArrayList componentTypes_ = new ArrayList();

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="key">�L�[</param>
		public TooManyRegistrationComponentDef(object key)
		{
			key_ = key;
		}

		/// <summary>
		/// �R���|�[�l���g��Type��ǉ����܂��B
		/// </summary>
		/// <param name="componentType">�R���|�[�l���g��Type</param>
		public void AddComponentType(Type componentType)
		{
			componentTypes_.Add(componentType);
		}


		public override object GetComponent()
		{
			throw new TooManyRegistrationRuntimeException(
				key_,(Type[]) componentTypes_.ToArray(typeof(Type)));
		}

		public override object GetComponent(Type receiveType)
		{
			throw new TooManyRegistrationRuntimeException(
				key_,(Type[]) componentTypes_.ToArray(typeof(Type)));
		}



	}
}
