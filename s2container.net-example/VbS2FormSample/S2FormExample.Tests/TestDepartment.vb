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
Imports System.IO
Imports Seasar.S2FormExample.Logics.Page
Imports Seasar.S2FormExample.Logics.Dto
Imports Seasar.S2FormExample.Logics.Dao
Imports log4net.Config
Imports log4net
Imports MbUnit.Framework
Imports System.Reflection
Imports Seasar.Extension.Unit
Imports log4net.Util
Imports Seasar.S2FormExample.Logics.Service

''' <summary>
''' ����p�e�X�g�P�[�X�N���X
''' </summary>
''' <remarks></remarks>
<TestFixture()> _
Public Class TestDepartment
    Inherits S2TestCase

    ''' <summary>
    ''' Logic�ݒ�t�@�C��
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PATH As String = "ExampleLogics.dicon"

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
    <Test(), S2()> _
    Public Sub TestSelectOfDao()
        Include(PATH)

        Dim dao As IDepartmentDao = CType(GetComponent(GetType(IDepartmentDao)), IDepartmentDao)
        Assert.IsNotNull(dao, "NotNull")

        ' �ꗗ�̃e�X�g
        Dim list As IList(Of DepartmentDto) = dao.GetAll()
        Assert.AreEqual(3, list.Count, "Count")
        Dim i As Integer = 0
        For Each dto As DepartmentDto In list
            If i = 1 Then
                Assert.AreEqual(2, dto.Id.Value, "ID")
                Assert.AreEqual("0002", dto.Code, "Code")
                Assert.AreEqual("�Z�p��", dto.Name, "Name")
                Assert.AreEqual(2, dto.ShowOrder, "Order")
            End If

            i += 1
        Next

        ' �ʎ擾�̃e�X�g
        Dim data As DepartmentDto = dao.GetData(2)
        Assert.AreEqual(2, data.Id.Value, "ID")
        Assert.AreEqual("0002", data.Code, "Code")
        Assert.AreEqual("�Z�p��", data.Name, "Name")
        Assert.AreEqual(2, data.ShowOrder, "Order")

        Assert.AreEqual(2, dao.GetId("0002"), "GetId")

    End Sub

    ''' <summary>
    ''' �X�V�n�̃e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), S2(Tx.Rollback)> _
    Public Sub TestInsertOfDao()
        Include(PATH)

        Dim dao As IDepartmentDao = CType(GetComponent(GetType(IDepartmentDao)), IDepartmentDao)
        Assert.IsNotNull(dao, "NotNull")

        ' �}���̃e�X�g
        Dim data As New DepartmentDto
        data.Code = "0102"
        data.Name = "�Ǘ���"
        data.ShowOrder = 4

        Assert.AreEqual(1, dao.InsertData(data), "Insert")

        ' �X�V�̃e�X�g
        Dim id As Integer = dao.GetId("0102")
        data = New DepartmentDto()
        data.Code = "0102"
        data.Id = id
        data.Name = "���ƊǗ���"
        data.ShowOrder = 4

        Assert.AreEqual(1, dao.UpdateData(data), "Update")

        data = dao.GetData(id)
        Assert.AreEqual(id, data.Id.Value, "ID")
        Assert.AreEqual("0102", data.Code, "Code")
        Assert.AreEqual("���ƊǗ���", data.Name, "Name")
        Assert.AreEqual(4, data.ShowOrder, "Order")

        ' �폜�̃e�X�g
        data = New DepartmentDto()
        data.Id = id
        Assert.AreEqual(1, dao.DeleteData(data), "Delete")

        Dim list As IList(Of DepartmentDto) = dao.GetAll()
        Assert.AreEqual(3, list.Count, "Count")

    End Sub

    ''' <summary>
    ''' ���僊�X�g�T�[�r�X�e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), S2()> _
    Public Sub TestListService()
        Include(PATH)

        Dim service As IDepartmentListService = CType(GetComponent(GetType(IDepartmentListService)), IDepartmentListService)
        Dim page As DepartmentListPage = service.GetAll

        Assert.AreEqual(3, page.List.Count, "Count")

    End Sub

    ''' <summary>
    ''' ����o�^�p�T�[�r�X�e�X�g
    ''' </summary>
    ''' <remarks></remarks>
    <Test(), S2(Tx.Rollback)> _
    Public Sub TestEditService()
        Include(PATH)

        Dim service As IDepartmentEditService = CType(GetComponent(GetType(IDepartmentEditService)), IDepartmentEditService)
        Assert.IsNotNull(service, "NotNull")

        Dim data As DepartmentEditPage = service.GetData(2)
        Assert.AreEqual(2, data.Id.Value, "ID")
        Assert.AreEqual("0002", data.Code, "Code")
        Assert.AreEqual("�Z�p��", data.Name, "Name")
        Assert.AreEqual("2", data.Order, "Order")

        ' �}���̃e�X�g
        data = New DepartmentEditPage()
        data.Code = "0102"
        data.Name = "�Ǘ���"
        data.Order = "4"

        Assert.AreEqual(1, service.ExecUpdate(data), "Insert")

        ' �X�V�̃e�X�g
        data = New DepartmentEditPage()
        data.Id = 2
        data.Code = "0020"
        data.Name = "�Z�p���ƕ�"
        data.Order = "5"

        Assert.AreEqual(1, service.ExecUpdate(data), "Update")

        data = service.GetData(2)
        Assert.AreEqual(2, data.Id.Value, "ID")
        Assert.AreEqual("0020", data.Code, "Code")
        Assert.AreEqual("�Z�p���ƕ�", data.Name, "Name")
        Assert.AreEqual("5", data.Order, "Order")

    End Sub
End Class