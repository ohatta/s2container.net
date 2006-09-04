#region Copyright
/*
 * Copyright 2005-2006 the Seasar Foundation and the Others.
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
using System.Web;
using System.Web.SessionState;

namespace Seasar.Framework.Container
{
	/// <summary>
	/// �R���|�[�l���g���Ǘ�����DI�R���e�i�̃C���^�[�t�F�[�X
	/// </summary>
	public interface IS2Container : IMetaDefAware
	{

		/// <summary>
		/// �L�[���w�肵�ăR���|�[�l���g���擾���܂��B
		/// �L�[�ƈ�v����R���|�[�l���g�������R���|�[�l���g���擾���܂��B
		/// </summary>
		/// <param name="componentKey">�R���|�[�l���g���擾���邽�߂̃L�[</param>
		/// <returns>�R���|�[�l���g</returns>
		Object GetComponent(object componentKey);

		/// <summary>
		/// �O���R���|�[�l���g�Ƀv���p�e�B�E�C���W�F�N�V�����A
		/// ���\�b�h�E�C���W�F�N�V���������s���܂��B
		/// �O���R���|�[�l���g�ƌ݊����̂���R���|�[�l���g��`�𗘗p���܂��B
		/// instance���[�h��"outer"�ƒ�`���ꂽ�R���|�[�l���g�̂ݗL���ł��B
		/// </summary>
		/// <param name="outerComponent">�O���R���|�[�l���g</param>
		void InjectDependency(Object outerComponent);

		/// <summary>
		/// �O���R���|�[�l���g�Ƀv���p�e�B�E�C���W�F�N�V�����A
		/// ���\�b�h�E�C���W�F�N�V���������s���܂��B
		/// �O���R���|�[�l���g��`�̃L�[�ƌ݊����̂���
		/// �R���|�[�l���g��`�𗘗p���܂��B
		/// instance���[�h��"outer"�ƒ�`���ꂽ�R���|�[�l���g�̂ݗL���ł��B
		/// </summary>
		/// <param name="outerComponent">�O���R���|�[�l���g</param>
		/// <param name="componentType">�O���R���|�[�l���g��`�̃L�[(Type)</param>
		void InjectDependency(Object outerComponent,Type componentType);

		/// <summary>
		/// �O���R���|�[�l���g�Ƀv���p�e�B�E�C���W�F�N�V�����A
		/// ���\�b�h�E�C���W�F�N�V���������s���܂��B
		/// �O���R���|�[�l���g��`�̃L�[�ƈ�v���閼�O�̃R���|�[�l���g��`��
		/// �擾���܂��B
		/// instance���[�h��"outer"�ƒ�`���ꂽ�R���|�[�l���g�̂ݗL���ł��B
		/// </summary>
		/// <param name="outerComponent">�O���R���|�[�l���g</param>
		/// <param name="componentName">�O���R���|�[�l���g��`�̃L�[�i���O�j</param>
		void InjectDependency(Object outerComponent,string componentName);

		/// <summary>
		/// �I�u�W�F�N�g���R���|�[�l���g�Ƃ��ēo�^���܂��B
		/// �L�[�̓I�u�W�F�N�g�̃N���X�ɂȂ�܂��B
		/// </summary>
		/// <param name="component">�R���|�[�l���g�Ƃ��ēo�^����I�u�W�F�N�g</param>
		void Register(Object component);

		/// <summary>
		/// �I�u�W�F�N�g�𖼑O�t���R���|�[�l���g�Ƃ��ēo�^���܂��B
		/// </summary>
		/// <param name="component">�R���|�[�l���g�Ƃ��ēo�^����I�u�W�F�N�g</param>
		/// <param name="componentName">�R���|�[�l���g��</param>
		void Register(Object component,string componentName);

		/// <summary>
		/// Type���R���|�[�l���g��`�Ƃ��ēo�^���܂�
		/// </summary>
		/// <param name="componentType">�R���|�[�l���g��Type</param>
		void Register(Type componentType);

		/// <summary>
		/// Type�𖼑O�t���R���|�[�l���g��`�Ƃ��ēo�^���܂��B
		/// </summary>
		/// <param name="componentType">�R���|�[�l���g��Type</param>
		/// <param name="componentName">�R���|�[�l���g��</param>
		void Register(Type componentType,string componentName);

		/// <summary>
		/// �R���|�[�l���g��`��o�^���܂��B
		/// </summary>
		/// <param name="componentDef">�o�^����R���|�[�l���g��`</param>
		void Register(IComponentDef componentDef);

		/// <summary>
		/// �R���|�[�l���g��`�̐����擾���܂��B
		/// </summary>
		int ComponentDefSize{get;}

		/// <summary>
		/// �ԍ����w�肵�ăR���|�[�l���g��`���擾���܂��B
		/// </summary>
		/// <param name="index">�ԍ�</param>
		/// <returns>�R���|�[�l���g��`</returns>
		IComponentDef GetComponentDef(int index);

		/// <summary>
		/// �w�肵���L�[����R���|�[�l���g��`���擾���܂��B
		/// </summary>
		/// <param name="componentName">�R���|�[�l���g�̃L�[</param>
		/// <returns>�R���|�[�l���g��`</returns>
		IComponentDef GetComponentDef(object key);

		/// <summary>
		/// �w�肵���L�[�̃R���|�[�l���g��`�������Ă��邩���肵�܂��B
		/// </summary>
		/// <param name="componentKey">�R���|�[�l���g�̃L�[</param>
		/// <returns>���݂���Ȃ�true</returns>
		bool HasComponentDef(object componentKey);

		/// <summary>
		/// root�̃R���e�i�ŁApath�ɑΉ�����R���e�i�����Ƀ��[�h�����
		/// ���邩��Ԃ��܂��B
		/// </summary>
		/// <param name="path">�p�X</param>
		/// <returns>���[�h����Ă�����true</returns>
		bool HasDescendant(string path);

		/// <summary>
		/// root�̃R���e�i�ŁA�w�肵���p�X�ɑΉ����郍�[�h�ς݂̃R���e�i��
		/// �擾���܂��B
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		IS2Container GetDescendant(string path);

		/// <summary>
		/// root�̃R���e�i�ɁA���[�h�ς݂̃R���e�i��o�^���܂��B
		/// </summary>
		/// <param name="descendant"></param>
		void RegisterDescendant(IS2Container descendant);

		/// <summary>
		/// �q�R���e�i��include���܂��B
		/// </summary>
		/// <param name="child">include����q�R���e�i</param>
		void Include(IS2Container child);

		/// <summary>
		/// �q�R���e�i�̐����擾���܂��B
		/// </summary>
		int ChildSize{get;}

		/// <summary>
		/// �ԍ����w�肵�Ďq�R���e�i���擾���܂�
		/// </summary>
		/// <param name="index">�q�R���e�i�̔ԍ�</param>
		/// <returns>�q�R���e�i</returns>
		IS2Container GetChild(int index);

		/// <summary>
		/// �R���e�i�����������܂��B
		/// �q�R���e�i�����ꍇ�A�q�R���e�i��S�ď�����������A���������������܂��B
		/// </summary>
		void Init();

		/// <summary>
		/// ���O���
		/// </summary>
		string Namespace{set;get;}

		/// <summary>
		/// �ݒ�t�@�C���̃p�X
		/// </summary>
		string Path{set;get;}

		/// <summary>
		/// ���[�g�̃R���e�i
		/// </summary>
		IS2Container Root{set;get;}

		/// <summary>
		/// �R���e�i�̏I���������s���܂��B
		/// �q�R���e�i�����ꍇ�A�����̏I�����������s������A
		/// �q�R���e�i�S�Ă̏I���������s���܂��B
		/// </summary>
		void Destroy();

		/// <summary>
		/// HTTP�̃��X�|���X
		/// </summary>
		HttpResponse Response { get; }

		/// <summary>
		/// HTTP�̃��N�G�X�g
		/// </summary>
		HttpRequest Request { get; }

		/// <summary>
		/// HTTP�̃Z�b�V����
		/// </summary>
		HttpSessionState Session { get; }

		/// <summary>
		/// �A�v���P�[�V�����̊�{�N���X
		/// </summary>
		HttpApplication HttpApplication { get; }

		/// <summary>
		/// HTTP �v���Ɋւ��� HTTP �ŗL�̂��ׂĂ̏��
		/// </summary>
		HttpContext HttpContext { set;get; }
	}
}
