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

namespace Seasar.Framework.Container
{
	/// <summary>
	/// IPropertyDef�̐ݒ肪�\�ɂȂ�܂��B
	/// </summary>
	public interface IPropertyDefAware
	{

		/// <summary>
		/// IPropertyDef��ǉ����܂��B
		/// </summary>
		/// <param name="propertyDef">IPropertyDef</param>
		void AddPropertyDef(IPropertyDef propertyDef);

		/// <summary>
		/// IPropertyDef�̐�
		/// </summary>
		int PropertyDefSize{get;}

		/// <summary>
		/// �ԍ����w�肵��IPropertyDef���擾���܂��B
		/// </summary>
		/// <param name="index">IPropertyDef�̔ԍ�</param>
		/// <returns>IPropertyDef</returns>
		IPropertyDef GetPropertyDef(int index);

		/// <summary>
		/// ���O���w�肵��IPropertyDef���擾���܂��B
		/// </summary>
		/// <param name="propertyName">IPropertyDef�̖��O</param>
		/// <returns>IPropertyDef</returns>
		IPropertyDef GetPropertyDef(string propertyName);

		/// <summary>
		/// �w�肵�����O��IPropertyDef�������Ă��邩���肵�܂��B
		/// </summary>
		/// <param name="propertyName">IPropertyDef�̖��O</param>
		/// <returns>���݂���Ȃ�true</returns>
		bool HasPropertyDef(string propertyName);
	}
}
