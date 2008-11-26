''
'' Copyright 2005-2008 the Seasar Foundation and the Others.
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
Imports Seasar.Quill.Attrs
Imports Seasar.S2FormExample.Logics.Service.Impl
Imports Seasar.S2FormExample.Logics.Page

Namespace Service
    ''' <summary>
    ''' �Ј��o�^�p�T�[�r�X�p�C���^�[�t�F�C�X
    ''' </summary>
    ''' <remarks></remarks>
    <Implementation(GetType(EmployeeEditServiceImpl))> _
    Public Interface IEmployeeEditService
        Inherits IBaseService

        ''' <summary>
        ''' �Ј��f�[�^���擾����
        ''' </summary>
        ''' <param name="id">�Ј�ID</param>
        ''' <returns>�Ј��f�[�^</returns>
        ''' <remarks></remarks>
        Function GetData(ByVal id As Integer) As EmployeeEditPage

        ''' <summary>
        ''' �Ј��f�[�^��o�^����
        ''' </summary>
        ''' <param name="data">�o�^�Ј��f�[�^</param>
        ''' <returns>�o�^����</returns>
        ''' <remarks></remarks>
        Function ExecUpdate(ByVal data As EmployeeEditPage) As Integer

        ''' <summary>
        ''' �Ј��f�[�^���폜����
        ''' </summary>
        ''' <param name="id">�폜�Ј�ID</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function ExecDelete(ByVal id As Integer) As Integer
    End Interface
End Namespace