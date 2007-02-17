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

namespace Seasar.Framework.Container
{
	/// <summary>
	/// �R���|�[�l���g�̒�`�����܂�
	/// </summary>
	public interface IComponentDef : IArgDefAware, IPropertyDefAware, IInitMethodDefAware, 
		IDestroyMethodDefAware, IAspectDefAware,IMetaDefAware
	{
		/// <summary>
		/// �R���|�[�l���g���擾���܂�
		/// </summary>
		///	<returns>�R���|�[�l���g</returns>
		object GetComponent();

		/// <summary>
		/// �󂯎��^���w�肵�ăR���|�[�l���g���擾���܂�
		/// </summary>
		/// <param name="receiveType">�󂯎��^</param>
		/// <returns>�R���|�[�l���g</returns>
		object GetComponent(Type receiveType);

		/// <summary>
		/// �O���R���|�[�l���g�Ƀv���p�e�B�E�C���W�F�N�V�����A
		/// ���\�b�h�E�C���W�F�N�V���������s���܂��B
		/// �O���R���|�[�l���g�ƌ݊����̂���R���|�[�l���g��`�𗘗p���܂��B
		/// instance���[�h��"outer"�ƒ�`���ꂽ�R���|�[�l���g�̂ݗL���ł��B
		/// </summary>
		/// <param name="outerComponent">�O���R���|�[�l���g</param>
		void InjectDependency(Object outerComponent);

		/// <summary>
		/// S2Container
		/// </summary>
		IS2Container Container{set;get;}

		/// <summary>
		/// �R���|�[�l���g��Type
		/// </summary>
		Type ComponentType{get;}

		/// <summary>
		/// �R���|�[�l���g�̖��O
		/// </summary>
		string ComponentName{get;}

		/// <summary>
		/// �����o�C���f�B���O���[�h
		/// </summary>
		string AutoBindingMode{set;get;}

		/// <summary>
		/// �C���X�^���X���[�h
		/// </summary>
		string InstanceMode{set;get;}

		/// <summary>
		/// Expression
		/// </summary>
		string Expression{set;get;}

		/// <summary>
		/// �R���|�[�l���g�̒�`�����������܂��B
		/// </summary>
		void Init();

		/// <summary>
		/// �v���L�V���擾���܂��B
		/// </summary>
		object GetProxy(Type proxyType);

		void AddProxy(Type proxyType, object proxy);

		void Destroy();

	}
}
