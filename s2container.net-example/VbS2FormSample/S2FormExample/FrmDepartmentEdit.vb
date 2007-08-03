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
Imports System.Text.RegularExpressions
Imports Seasar.S2FormExample.Logics.Page
Imports Seasar.Windows.Attr
Imports Seasar.S2FormExample.Logics.Service

''' <summary>
''' ����o�^���
''' </summary>
''' <remarks></remarks>
<ControlModifier("Txt", "")> _
Public Class FrmDepartmentEdit
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
    ''' ����ID
    ''' </summary>
    ''' <remarks></remarks>
    Private _id As Nullable(Of Integer)

    ''' <summary>
    ''' ����o�^�T�[�r�X
    ''' </summary>
    ''' <remarks></remarks>
    Protected service As IDepartmentEditService

    ''' <summary>
    ''' ����ID
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Id() As Nullable(Of Integer)
        Get
            Return _id
        End Get
        Set(ByVal value As Nullable(Of Integer))
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' �o�^�{�^�����������Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnUpdate.Click
        Try
            If MessageBox.Show("�{���ɓo�^���܂����H", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) _
               = DialogResult.No Then
                Return
            End If

            If _SetInputData() = False Then
                Return
            End If

            Dim data As DepartmentEditPage = CType(Me.DataSource, DepartmentEditPage)
            data.Id = _id
            If service.ExecUpdate(data) > 0 Then
                _InitializeControls()
                MessageBox.Show("�o�^���܂���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Throw New ApplicationException("�o�^�Ɏ��s���܂���")
            End If
        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT, ex.Message), Text, _
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' �폜�{�^�����������Ƃ��̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnDelete.Click
        Try
            If MessageBox.Show("�{���ɍ폜���܂����H", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) _
               = DialogResult.No Then
                Return
            End If

            If _id.HasValue = True Then
                If service.ExecDelete(_id.Value) > 0 Then
                    MessageBox.Show("�폜���܂���", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Else
                    Throw New ApplicationException("�폜�Ɏ��s���܂���")
                End If
            Else
                MessageBox.Show("�폜�Ώۂ�I��ł�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT, ex.Message), Text, _
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
    Private Sub FrmDepartmentEdit_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        logger.InfoFormat("{0}�����[�h����܂���", Name)
        Try
            _InitializeControls()

            If _id.HasValue = True Then
                Dim data As DepartmentEditPage = service.GetData(_id.Value)
                If data Is Nothing = False Then
                    Me.DataSource = data
                    BtnDelete.Enabled = True
                Else
                    Me.DataSource = Nothing
                    Throw New ApplicationException("����f�[�^��������܂���ł���")
                End If
            Else
                Me.DataSource = New DepartmentEditPage()
            End If

        Catch ex As Exception
            logger.ErrorFormat(EXCEPTION_MSG_FORMAT, ex.Message)
            MessageBox.Show(String.Format(EXCEPTION_MSG_FORMAT, ex.Message), Text, _
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' �R���g���[��������������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub _InitializeControls()
        TxtCode.Text = ""
        TxtName.Text = ""
        TxtOrder.Text = "0"

        BtnDelete.Enabled = False
    End Sub

    ''' <summary>
    ''' ���̓f�[�^���`�F�b�N����
    ''' </summary>
    ''' <returns>�o�^�̉�</returns>
    ''' <remarks></remarks>
    Private Function _SetInputData() As Boolean
        Dim ret As Boolean = True

        '' �R���g���[������DataSource�Ńo�C���h�����I�u�W�F�N�g�֔��f�BControlAttribute�ł��\�B
        Validate()

        '' ����R�[�h
        If String.IsNullOrEmpty(TxtCode.Text) = False Then
            If Regex.IsMatch(TxtCode.Text, "^\d{4}") = False Then
                MessageBox.Show("�R�[�h�ɐ����ȊO�̕���������܂�", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ret = False
            End If
        Else
            MessageBox.Show("�R�[�h����͂��Ă�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ret = False
        End If

        '' ���喼
        If String.IsNullOrEmpty(TxtName.Text) = True Then
            MessageBox.Show("���O����͂��Ă�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ret = False
        End If

        '' �\������
        If String.IsNullOrEmpty(TxtOrder.Text) = False Then
            If Regex.IsMatch(TxtOrder.Text, "^\d{1,4}") = False Then
                MessageBox.Show("�\�����Ԃɐ����ȊO�̕���������܂�", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ret = False
            End If
        Else
            MessageBox.Show("�\�����Ԃ���͂��Ă�������", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ret = False
        End If

        Return ret
    End Function
End Class
