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
Imports System.IO
Imports Seasar.S2FormExample.Logics.Page
Imports Seasar.S2FormExample.Logics
Imports Seasar.S2FormExample.Logics.Dto
Imports Seasar.S2FormExample.Logics.Dao
Imports log4net.Config
Imports log4net
Imports MbUnit.Framework
Imports System.Reflection
Imports Seasar.Extension.Unit
Imports log4net.Util
Imports Seasar.S2FormExample.Logics.Service
Imports Seasar.Quill.Unit

<TestFixture()> _
Public Class TestEmployee
    Inherits QuillTestCase

    Protected daoOfEmp As IEmployeeDao
    Protected daoOfOutput As IOutputCSVDao
    Protected daoOfCsv As IEmployeeCSVDao
    Protected editService As IEmployeeEditService
    Protected listService As IEmployeeListService

    ''' <summary>
    ''' �e�X�g�̃Z�b�g�A�b�v
    ''' </summary>
    ''' <remarks></remarks>
    <SetUp()> _
    Public Sub Setup()
        Dim info As FileInfo = _
                New FileInfo(Format(SystemInfo.AssemblyShortName(Assembly.GetExecutingAssembly()), _
                                      "{0}.dll.config"))
        XmlConfigurator.Configure(LogManager.GetRepository(), info)
    End Sub

    ''' <summary>
    ''' �����n�̃e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), Quill(Tx.Rollback)> _
    Public Sub TestSelectOfDao()

        ' �ꗗ�Ŏ擾����
        Dim list As IList(Of EmployeeDto) = daoOfEmp.GetAll
        Assert.AreEqual(5, list.Count, "Count")
        Dim i As Integer = 0
        For Each dto As EmployeeDto In list
            If i = 2 Then
                Assert.AreEqual(3, dto.Id.Value, "Id")
                Assert.AreEqual("�������q", dto.Name, "Name")
                Assert.AreEqual("010003", dto.Code, "Code")
                Dim targetDate As New DateTime(2001, 4, 1, 0, 0, 0)
                Assert.AreEqual(2, dto.Gender, "Gender")
                Assert.AreEqual(targetDate, dto.EntryDay.Value, "Entry")
                Assert.AreEqual(1, dto.DeptNo.Value, "DeptNo")
                Assert.AreEqual(1, dto.Department.Id.Value, "Dept.No")
                Assert.AreEqual("�c�ƕ�", dto.Department.Name, "Dept.Name")
                Assert.AreEqual("0001", dto.Department.Code, "Dept.Code")

            End If
            i += 1
        Next

        list = daoOfEmp.FindByGender(1)
        Assert.AreEqual(4, list.Count, "Count2")

        ' �ʂɎ擾����
        Dim data As EmployeeDto = daoOfEmp.GetData(3)
        Assert.AreEqual(3, data.Id.Value, "Id2")
        Assert.AreEqual("�������q", data.Name, "Name2")
        Assert.AreEqual("010003", data.Code, "Code2")
        Assert.AreEqual(2, data.Gender, "Gender2")
        Dim targetDate2 As New DateTime(2001, 4, 1, 0, 0, 0)
        Assert.AreEqual(targetDate2, data.EntryDay.Value, "Entry2")
        Assert.AreEqual(1, data.DeptNo.Value, "DeptN2o")
        Assert.AreEqual(1, data.Department.Id.Value, "Dept.No2")
        Assert.AreEqual("�c�ƕ�", data.Department.Name, "Dept.Name2")
        Assert.AreEqual("0001", data.Department.Code, "Dept.Code2")

        Assert.AreEqual(3, daoOfEmp.GetId("010003"), "GetId")

    End Sub

    ''' <summary>
    ''' �X�V�n�̃e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), Quill(Tx.Rollback)> _
    Public Sub TestInsertOfDao()

        ' �}���̃e�X�g
        Dim data As New EmployeeDto()
        data.Code = "060006"
        data.Name = "�㓡�Z�Y"
        data.EntryDay = New DateTime(2001, 4, 1, 0, 0, 0)
        data.Gender = 1
        data.DeptNo = 1

        Assert.AreEqual(1, daoOfEmp.InsertData(data), "Insert")

        ' �X�V�̃e�X�g
        Dim id As Integer = daoOfEmp.GetId("060006")
        data.Id = id
        data.Code = "060006"
        data.Name = "�ܓ��Z�Y"
        data.EntryDay = New DateTime(2001, 4, 1, 0, 0, 0)
        data.Gender = 1
        data.DeptNo = 2

        Assert.AreEqual(1, daoOfEmp.UpdateData(data), "Update")

        data = daoOfEmp.GetData(id)
        Assert.AreEqual(id, data.Id.Value, "Id")
        Assert.AreEqual("�ܓ��Z�Y", data.Name, "Name")
        Assert.AreEqual("060006", data.Code, "Code")
        Assert.AreEqual(1, data.Gender, "Gender2")
        Dim targetDate As New DateTime(2001, 4, 1, 0, 0, 0)
        Assert.AreEqual(targetDate, data.EntryDay.Value, "Entry")
        Assert.AreEqual(2, data.DeptNo.Value, "DeptNo")
        Assert.AreEqual(2, data.Department.Id.Value, "Dept.No2")
        Assert.AreEqual("�Z�p��", data.Department.Name, "Dept.Name2")
        Assert.AreEqual("0002", data.Department.Code, "Dept.Code2")

        ' �폜�̃e�X�g
        data = New EmployeeDto()
        data.Id = id

        Assert.AreEqual(1, daoOfEmp.DeleteData(data), "Delete")

        Dim list As IList(Of EmployeeDto) = daoOfEmp.GetAll()
        Assert.AreEqual(5, list.Count, "Count")

    End Sub

    ''' <summary>
    ''' CSV�p�e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), Quill(Tx.Rollback)> _
    Public Sub TestDaoOfCSV()

        Dim list As IList(Of EmployeeCsvDto) = daoOfCsv.GetAll
        Assert.AreEqual(5, list.Count, "Count")

        Dim i As Integer = 0
        For Each dto As EmployeeCsvDto In list
            If i = 2 Then
                Assert.AreEqual("�������q", dto.Name, "Name")
                Assert.AreEqual("010003", dto.Code, "Code")
                Assert.AreEqual(2, dto.Gender, "Gender")
                Assert.AreEqual("����", dto.GenderName, "GenderName")
                Dim targetDate As New DateTime(2001, 4, 1, 0, 0, 0)
                Assert.AreEqual(targetDate, dto.EntryDay.Value, "Entry")
                Assert.AreEqual("�c�ƕ�", dto.DeptName, "Dept.Name")
                Assert.AreEqual("0001", dto.DeptCode, "Dept.Code")
            End If
            i += 1
        Next

        ' �o�͂̃e�X�g
        Dim filepath As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\csvtest.csv"
        Assert.AreEqual(5, daoOfOutput.OutputEmployeeList(filepath, list))

    End Sub

    ''' <summary>
    ''' �Ј����X�g�T�[�r�X�e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), Quill(Tx.NotSupported)> _
    Public Sub TestListService()

        Dim page As EmployeeListPage = listService.GetAll()
        Assert.AreEqual(5, page.List.Count, "Count")
        Dim i As Integer = 0
        For Each dto As EmployeeDto In page.List
            If i = 2 Then
                Assert.AreEqual(3, dto.Id.Value, "Id")
                Assert.AreEqual("�������q", dto.Name, "Name")
                Assert.AreEqual("010003", dto.Code, "Code")
                Assert.AreEqual(2, dto.Gender, "Gender")
                Dim targetDate As Date = New Date(2001, 4, 1, 0, 0, 0)
                Assert.AreEqual(targetDate, dto.EntryDay.Value, "Entry")
                Assert.AreEqual(1, dto.DeptNo.Value, "DeptNo")
                Assert.AreEqual(1, dto.Department.Id.Value, "Dept.No")
                Assert.AreEqual("�c�ƕ�", dto.Department.Name, "Dept.Name")
                Assert.AreEqual("0001", dto.Department.Code, "Dept.Code")
                Assert.AreEqual("�c�ƕ�", dto.DeptName, "DeptName")
            End If
            i += 1
        Next
    End Sub

    ''' <summary>
    ''' �Ј��o�^�T�[�r�X�̃e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), Quill(Tx.NotSupported)> _
    Public Sub TestEditService()
        Dim data As EmployeeEditPage = editService.GetData(3)
        Assert.AreEqual(3, data.Id.Value, "Id")
        Assert.AreEqual("�������q", data.Name, "Name")
        Assert.AreEqual("010003", data.Code, "Code")
        Assert.AreEqual(2, data.Gender, "Gender")
        Dim targetDate As Date = New Date(2001, 4, 1, 0, 0, 0)
        Assert.AreEqual(targetDate, data.Entry, "Entry")
        Assert.AreEqual(1, data.Depart, "DeptNo")

        '' �}���̃e�X�g
        data = New EmployeeEditPage()
        data.Code = "060006"
        data.Name = "�㓡�Z�Y"
        data.Entry = New DateTime(2006, 4, 1, 0, 0, 0)
        data.Gender = 1
        data.Depart = 1

        Assert.AreEqual(1, editService.ExecUpdate(data), "Insert")

        '' �X�V�̃e�X�g
        data = New EmployeeEditPage()
        data.Id = 2
        data.Code = "999999"
        data.Name = "��ؓ�Y"
        data.Entry = New DateTime(1999, 5, 1, 0, 0, 0)
        data.Gender = 1
        data.Depart = 2

        Assert.AreEqual(1, editService.ExecUpdate(data), "Update")

        data = editService.GetData(2)
        Assert.AreEqual(2, data.Id.Value, "Id")
        Assert.AreEqual("��ؓ�Y", data.Name, "Name")
        Assert.AreEqual("999999", data.Code, "Code")
        Assert.AreEqual(1, data.Gender, "Gender")
        targetDate = New Date(1999, 5, 1, 0, 0, 0)
        Assert.AreEqual(targetDate, data.Entry, "Entry")
        Assert.AreEqual(2, data.Depart, "DeptNo")

    End Sub
End Class