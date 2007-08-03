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
Imports log4net
Imports System.Reflection
Imports Seasar.S2FormExample.Logics.Page
Imports Seasar.Windows.Attr
Imports Seasar.S2FormExample.Logics.Service

''' <summary>
''' ����ꗗ���
''' </summary>
''' <remarks></remarks>
<ControlModifier("Txt", "")> _
    <Control("GridList", "DataSource", "List")> _
Public Class FrmDepartmentList
    ''' <summary>
    ''' ��O�G���[���b�Z�[�W����
    ''' </summary>
    ''' <remarks></remarks>
    Private Const EXCEPTION_MSG_FORMAT As String = "�\���ł��Ȃ��G���[���������܂����B�ڍׂ��m�F���Ă��������B�i{0}�j"

    ''' <summary>
    ''' ���O(log4net)
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly logger As ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

    ''' <summary>
    ''' ��ʃf�B�X�p�b�`���[
    ''' </summary>
    ''' <remarks></remarks>
    Protected dispatcher As IFormDispatcher

    ''' <summary>
    ''' ����ꗗ�T�[�r�X
    ''' </summary>
    ''' <remarks></remarks>
    Protected service As IDepartmentListService

    ''' <summary>
    ''' �V�K�{�^�����������Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNew.Click
        Try
            dispatcher.ShowMasterEdit(Nothing)
            Me.DataSource = service.GetAll
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' ����{�^�����������Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnClose.Click
        logger.InfoFormat("{0}���I��", Name)
        Close()
    End Sub

    ''' <summary>
    ''' �t�H�[�������[�h�����Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDepartmentList_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            logger.InfoFormat("{0}�����[�h����܂���", Name)

            Me.DataSource = service.GetAll()
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' GridView������������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub _InitializeGridView()
        GridList.RowCount = 0
    End Sub

    ''' <summary>
    ''' �O���b�h���_�u���N���b�N�����Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridList_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) _
        Handles GridList.CellDoubleClick
        Try
            Dim index As Integer = GridList.CurrentRow.Index
            Dim id As Nullable(Of Integer) = (CType(Me.DataSource, DepartmentListPage)).List(index).Id
            dispatcher.ShowMasterEdit(id)

            Me.DataSource = service.GetAll
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class