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
Imports Seasar.S2FormExample.Logics.Dao

Namespace Service.Impl
    Public Class EmployeeListServiceImpl
        Inherits BaseServiceImpl
        Implements IEmployeeListService

        Protected daoOfCsv As IEmployeeCSVDao

        Protected daoOfEmployee As IEmployeeDao

        Protected daoOfOutput As IOutputCSVDao

        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            '
        End Sub

        ''' <summary>
        ''' CSV�p�Ј�DAO
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DaoOfCsvProperty() As IEmployeeCSVDao
            Get
                Return daoOfCsv
            End Get
            Set(ByVal value As IEmployeeCSVDao)
                daoOfCsv = value
            End Set
        End Property

        ''' <summary>
        ''' �Ј�DAO
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DaoOfEmployeeProperty() As IEmployeeDao
            Get
                Return daoOfEmployee
            End Get
            Set(ByVal value As IEmployeeDao)
                daoOfEmployee = value
            End Set
        End Property

        ''' <summary>
        ''' �o�͗pDAO
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DaoOfOutputProperty() As IOutputCSVDao
            Get
                Return daoOfOutput
            End Get
            Set(ByVal value As IOutputCSVDao)
                daoOfOutput = value
            End Set
        End Property


        ''' <summary>
        ''' �Ј��ꗗ����������
        ''' </summary>
        ''' <param name="condition">��������</param>
        ''' <returns>�Ј��ꗗ</returns>
        ''' <remarks></remarks>
        Public Function Find(ByVal condition As EmployeeListPage) As EmployeeListPage _
            Implements IEmployeeListService.Find

            Dim page As EmployeeListPage = New EmployeeListPage()
            page.GenderId = condition.GenderId
            Dim genderList As IList(Of GenderDto) = MyBase.GetGenderAll()
            For Each dto As GenderDto In genderList
                If dto.Id = Convert.ToInt32(condition.GenderId) Then
                    page.GenderName = dto.Name
                End If
            Next

            If condition.GenderId = "99" Then
                page.GenderName = "�S��"
            End If

            Dim list As IList(Of EmployeeDto)
            If page.GenderId <> "99" Then
                list = daoOfEmployee.FindByGender(Convert.ToInt32(condition.GenderId))
            Else
                list = daoOfEmployee.GetAll()
            End If

            If list Is Nothing = False Then
                page.List = list
            End If

            Return page
        End Function

        ''' <summary>
        ''' �Ј��ꗗ���擾����
        ''' </summary>
        ''' <returns>�Ј��ꗗ</returns>
        ''' <remarks></remarks>
        Public Function GetAll() As EmployeeListPage Implements IEmployeeListService.GetAll
            Dim page As EmployeeListPage = New EmployeeListPage()

            page.GenderId = "99"
            page.GenderName = "�S��"
            page.List = daoOfEmployee.GetAll()

            Return page

        End Function

        ''' <summary>
        ''' CSV�ŏo�͂���
        ''' </summary>
        ''' <param name="path">�o�͐�p�X</param>
        ''' <returns>�o�͌���</returns>
        ''' <remarks></remarks>
        Public Function OutputCSV(ByVal path As String) As Integer Implements IEmployeeListService.OutputCSV
            If String.IsNullOrEmpty(path) = True Then
                Throw New ArgumentNullException("path")
            End If

            Dim list As IList(Of EmployeeCsvDto) = daoOfCsv.GetAll()

            If list.Count = 0 Then
                Return 0
            End If

            Return daoOfOutput.OutputEmployeeList(path, list)

        End Function
    End Class
End Namespace