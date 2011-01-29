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
using System.Windows.Forms;
using Seasar.Windows.Attr;

namespace Seasar.WindowsExample.Forms
{
    /// <summary>
    /// ��ʑJ�ڒ�`FormDispatcher�C���^�[�t�F�C�X
    /// </summary>
    /// <remarks>
    /// ���[�_���X�ŕ\������t�H�[���́Adicon�t�@�C����instance=prototype�ɂ���
    /// <newpara>
    /// �t�H�[���ɒl��n���Ƃ��͕\�����\�b�h�Ɉ�����p�ӂ��A�����̃v���p�e�B��Ώۂ̃t�H�[���ɗp�ӂ���B
    /// </newpara>
    /// <newpara>
    /// ���\�b�h(�܂��͑Ώۃt�H�[��)�Ɩ��O��Ԃ�ύX���A�g���܂킵�Ă��悢�B
    /// </newpara>
    /// </remarks>
    public interface IFormDispatcher
    {
        /// <summary>
        /// �Ј��ꗗ�t�H�[����\������
        /// </summary>
        /// <returns>�_�C�A���O����</returns>
        [TargetForm(typeof (FrmEmployeeList), ModalType.Modal)]
        DialogResult ShowDataList();

        /// <summary>
        /// �Ј��ҏW�t�H�[����\������
        /// </summary>
        /// <param name="Id">�Ј�ID</param>
        /// <returns>�_�C�A���O����</returns>
        [TargetForm(typeof (FrmEmployeeEdit), ModalType.Modal)]
        DialogResult ShowDataEdit(Nullable<int> Id);

        /// <summary>
        /// ����ꗗ�t�H�[����\������
        /// </summary>
        /// <returns>�_�C�A���O����</returns>
        [TargetForm(typeof (FrmDepartmentList), ModalType.Modal)]
        DialogResult ShowMasterList();

        /// <summary>
        /// ����ҏW�t�H�[����\������
        /// </summary>
        /// <param name="Id">����ID</param>
        /// <returns>�_�C�A���O����</returns>
        [TargetForm(typeof (FrmDepartmentEdit), ModalType.Modal)]
        DialogResult ShowMasterEdit(Nullable<int> Id);
    }
}