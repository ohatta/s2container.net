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
Imports Seasar.S2FormExample.Logics.Dto
Imports Seasar.S2FormExample.Logics.Page
Imports Seasar.Quill.Attrs
Imports Seasar.S2FormExample.Logics.Dao

Namespace Service.Impl
    ''' <summary>
    ''' ����o�^�T�[�r�X�p�����N���X
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DepartmentEditServiceImpl
        Inherits BaseServiceImpl
        Implements IDepartmentEditService

        ''' <summary>
        ''' ����pDAO
        ''' </summary>
        ''' <remarks></remarks>
        Protected dao As IDepartmentDao

        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' ����pDAO
        ''' </summary>
        ''' <remarks>S2Unit�Ńe�X�g���邽�߂ɒǉ�(Injection�p)</remarks>
        Public Property DaoProperty() As IDepartmentDao
            Get
                Return dao
            End Get
            Set(ByVal value As IDepartmentDao)
                dao = value
            End Set
        End Property

        ''' <summary>
        ''' ������폜����
        ''' </summary>
        ''' <param name="id">����ID</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        <Aspect("LocalRequiredTx")> _
        Public Function ExecDelete(ByVal id As Integer) As Integer Implements IDepartmentEditService.ExecDelete
            Dim data As New DepartmentDto
            data.Id = id

            Return dao.DeleteData(data)
        End Function

        ''' <summary>
        ''' �����o�^����
        ''' </summary>
        ''' <param name="dto">�o�^����ҏWPage</param>
        ''' <returns>�o�^����</returns>
        ''' <remarks></remarks>
        <Aspect("LocalRequiredTx")> _
        Public Function ExecUpdate(ByVal dto As DepartmentEditPage) As Integer _
            Implements IDepartmentEditService.ExecUpdate

            If dto Is Nothing Then
                Throw New ArgumentNullException
            End If

            Dim data As New DepartmentDto
            data.Code = dto.Code
            data.Id = dto.Id
            data.Name = dto.Name
            data.ShowOrder = Convert.ToInt32(dto.Order)

            If data.Id.HasValue = True Then
                Dim departmentDto As DepartmentDto = dao.GetData(dto.Id.Value)
                If departmentDto Is Nothing = False Then
                    Return dao.UpdateData(data)
                Else
                    Return dao.InsertData(data)
                End If
            Else
                Return dao.InsertData(data)
            End If

        End Function

        ''' <summary>
        ''' ����ҏWPage���擾����
        ''' </summary>
        ''' <param name="id">����ID</param>
        ''' <returns>����ҏWPage</returns>
        ''' <remarks></remarks>
        Public Function GetData(ByVal id As Integer) As DepartmentEditPage Implements IDepartmentEditService.GetData
            Dim page As New DepartmentEditPage

            Dim dto As DepartmentDto = dao.GetData(id)
            If dto Is Nothing = False Then
                page.Code = dto.Code
                page.Id = dto.Id
                page.Name = dto.Name
                page.Order = Convert.ToString(dto.ShowOrder)
            Else
                page = Nothing
            End If

            Return page
        End Function
    End Class
End Namespace