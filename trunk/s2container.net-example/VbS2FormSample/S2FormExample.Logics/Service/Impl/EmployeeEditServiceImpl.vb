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
Imports Seasar.Quill.Attrs
Imports Seasar.S2FormExample.Logics.Dto
Imports Seasar.S2FormExample.Logics.Page
Imports Seasar.S2FormExample.Logics.Dao

Namespace Service.Impl
    ''' <summary>
    ''' �Ј��o�^�p�T�[�r�X�p�����N���X
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EmployeeEditServiceImpl
        Inherits BaseServiceImpl
        Implements IEmployeeEditService

        Protected dao As IEmployeeDao

        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            '
        End Sub

        ''' <summary>
        ''' �Ј�DAO
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DaoProperty() As IEmployeeDao
            Get
                Return dao
            End Get
            Set(ByVal value As IEmployeeDao)
                dao = value
            End Set
        End Property


        ''' <summary>
        ''' �Ј��f�[�^���폜����
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Aspect("LocalRequiredTx")> _
        Public Function ExecDelete(ByVal id As Integer) As Integer Implements IEmployeeEditService.ExecDelete
            Dim dto As New EmployeeDto
            dto.Id = id

            Return dao.DeleteData(dto)
        End Function

        ''' <summary>
        ''' �Ј��f�[�^��o�^����
        ''' </summary>
        ''' <param name="data"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Aspect("LocalRequiredTx")> _
        Public Function ExecUpdate(ByVal data As EmployeeEditPage) As Integer _
            Implements IEmployeeEditService.ExecUpdate
            If data Is Nothing = True Then
                Throw New ArgumentNullException("data")
            End If

            Dim dto As New EmployeeDto
            dto.Code = data.Code
            dto.DeptNo = data.Depart
            dto.EntryDay = data.Entry
            dto.Gender = data.Gender
            dto.Id = data.Id
            dto.Name = data.Name

            If dto.Id.HasValue = True Then
                Dim e1 As EmployeeDto = dao.GetData(dto.Id)
                If e1 Is Nothing = False Then
                    Return dao.UpdateData(dto)
                Else
                    Return dao.InsertData(dto)
                End If
            Else
                Return dao.InsertData(dto)
            End If

        End Function

        ''' <summary>
        ''' �Ј��f�[�^���擾����
        ''' </summary>
        ''' <param name="id">�Ј�ID</param>
        ''' <returns>�Ј��f�[�^</returns>
        ''' <remarks></remarks>
        Public Function GetData(ByVal id As Integer) As EmployeeEditPage Implements IEmployeeEditService.GetData
            Dim page As EmployeeEditPage = New EmployeeEditPage()
            Dim dto As EmployeeDto = dao.GetData(id)
            If dto Is Nothing = False Then
                page.Code = dto.Code
                page.Depart = dto.DeptNo
                page.Entry = dto.EntryDay
                page.Gender = dto.Gender
                page.Id = dto.Id
                page.Name = dto.Name
            Else
                page = Nothing
            End If

            Return page
        End Function
    End Class
End Namespace