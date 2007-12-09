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
''' �Ј��ꗗ���
''' </summary>
''' <remarks></remarks>
<ControlModifier("Txt", "")> _
    <Control("gridList", "DataSource", "List")> _
    <Control("lblGenderName", "Text", "GenderName")> _
    <Control("txtGenderId", "Text", "GenderId", DataSourceUpdateMode.OnPropertyChanged)> _
Public Class FrmEmployeeList
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
    ''' �Ј��ꗗ�T�[�r�X
    ''' </summary>
    ''' <remarks></remarks>
    Protected service As IEmployeeListService

    ''' <summary>
    ''' �V�K�{�^�����������Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNew.Click
        Try
            dispatcher.ShowDataEdit(Nothing)

            Me.DataSource = service.GetAll
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' �o�̓{�^�����������Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnOutput_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnOutput.Click
        Try
            MessageBox.Show("�ۑ�����w�肵�Ă�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)

            _InitializeSaveDialog()

            If dlgSave.ShowDialog(Me) = DialogResult.OK Then
                If service.OutputCSV(dlgSave.FileName) > 0 Then
                    MessageBox.Show("�o�͂��܂���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("�o�͂���f�[�^������܂���ł���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

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
    ''' �O���b�h���_�u���N���b�N�����Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridList_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) _
        Handles GridList.CellDoubleClick
        Try
            Dim index As Integer = GridList.CurrentRow.Index
            Dim id As Nullable(Of Integer) = (CType(Me.DataSource, EmployeeListPage)).List(index).Id
            dispatcher.ShowDataEdit(id)

            Me.DataSource = service.GetAll
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' �t�H�[�������[�h�����Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEmployeeList_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            logger.InfoFormat("{0}�����[�h����܂���", Name)

            Me.DataSource = service.GetAll()
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' �ۑ��_�C�A���O������������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub _InitializeSaveDialog()
        dlgSave.DefaultExt = "*.csv"
        dlgSave.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        dlgSave.Title = "CSV�o��"
        dlgSave.Filter = "CSV�t�@�C�� (*.csv)|*.csv|���ׂẴt�@�C�� (*.*)|*.*"
        dlgSave.AddExtension = True
        dlgSave.OverwritePrompt = True
        dlgSave.FileName = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\�Ј��ꗗ.csv"
        dlgSave.RestoreDirectory = True
    End Sub

    ''' <summary>
    ''' ����ID����t�H�[�J�X���O�ꂽ�Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TxtGenderId_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles TxtGenderId.Leave
        Try
            Dim page As EmployeeListPage = CType(Me.DataSource, EmployeeListPage)
            If String.IsNullOrEmpty(page.GenderId) = True Then
                MessageBox.Show("���ʂ���͂��Ă�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            Else
                If page.GenderId <> "01" And page.GenderId <> "02" And page.GenderId <> "99" Then
                    MessageBox.Show("���ʂ𐳂������͂��Ă�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If
            End If

            Me.DataSource = service.Find(page)
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class
