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
Imports Seasar.S2FormExample.Logics.Service.Impl
Imports Seasar.Quill.Attrs
Imports Seasar.S2FormExample.Logics.Page

Namespace Service
    ''' <summary>
    ''' �Ј����X�g�T�[�r�X�p�C���^�[�t�F�C�X
    ''' </summary>
    ''' <remarks></remarks>
    <Implementation(GetType(EmployeeListServiceImpl))> _
    Public Interface IEmployeeListService
        ''' <summary>
        ''' �Ј��ꗗ���擾����
        ''' </summary>
        ''' <returns>�Ј��ꗗ</returns>
        ''' <remarks></remarks>
        Function GetAll() As EmployeeListPage

        ''' <summary>
        ''' �Ј��ꗗ����������
        ''' </summary>
        ''' <param name="condition">��������</param>
        ''' <returns>�Ј��ꗗ</returns>
        ''' <remarks></remarks>
        Function Find(ByVal condition As EmployeeListPage) As EmployeeListPage

        ''' <summary>
        ''' CSV�ŏo�͂���
        ''' </summary>
        ''' <param name="path">�o�͐�p�X</param>
        ''' <returns>�o�͌���</returns>
        ''' <remarks></remarks>
        Function OutputCSV(ByVal path As String) As Integer
    End Interface
End Namespace