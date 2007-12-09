''
'' Copyright 2005-2007 the Seasar Foundation and the Others.
''
'' Licensed under the Apache License, Version 2.0 (the "License");
'' you may not use this file except in compliance with the License.
'' You may obtain a copy of the License at
''
''     http://www.apache.org/licenses/LICENSE-2.0
''
'' Unless required by applicable law or agreed to in writing, software
'' distributed under the License is distributed on an "AS IS" BASIS,
'' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
'' either express or implied. See the License for the specific language
'' governing permissions and limitations under the License.
''
Imports Seasar.Windows.Attr
Imports Seasar.Quill.Attrs

''' <summary>
''' ��ʑJ�ڒ�`FormDispatcher�C���^�[�t�F�C�X
''' </summary>
''' <remarks></remarks>
<Implementation()> _
    <Aspect("FormInterceptorr")> _
Public Interface IFormDispatcher
    ''' <summary>
    ''' �Ј��ꗗ�t�H�[����\������
    ''' </summary>
    ''' <returns>�_�C�A���O����</returns>
    ''' <remarks></remarks>
    <TargetForm(GetType(FrmEmployeeList), ModalType.Modal)> _
    Function ShowDataList() As DialogResult

    ''' <summary>
    ''' �Ј��ҏW�t�H�[����\������
    ''' </summary>
    ''' <param name="id">�Ј�ID</param>
    ''' <returns>�_�C�A���O����</returns>
    ''' <remarks></remarks>
    <TargetForm(GetType(FrmEmployeeEdit), ModalType.Modal)> _
    Function ShowDataEdit(ByVal id As Nullable(Of Integer)) As DialogResult

    ''' <summary>
    ''' ����ꗗ�t�H�[����\������
    ''' </summary>
    ''' <returns>�_�C�A���O����</returns>
    ''' <remarks></remarks>
    <TargetForm(GetType(FrmDepartmentList), ModalType.Modal)> _
    Function ShowMasterList() As DialogResult

    ''' <summary>
    ''' ����ҏW�t�H�[����\������
    ''' </summary>
    ''' <param name="id">����ID</param>
    ''' <returns>�_�C�A���O����</returns>
    ''' <remarks></remarks>
    <TargetForm(GetType(FrmDepartmentEdit), ModalType.Modal)> _
    Function ShowMasterEdit(ByVal id As Nullable(Of Integer)) As DialogResult
End Interface