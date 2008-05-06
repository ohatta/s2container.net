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
Imports log4net.Config
Imports Microsoft.VisualBasic.ApplicationServices
Imports log4net
Imports System.Reflection
Imports log4net.Util

Namespace My
    ' ���̃C�x���g�� MyApplication �ɑ΂��ė��p�ł��܂�:
    ' 
    ' Startup: �A�v���P�[�V�������J�n���ꂽ�Ƃ��A�X�^�[�g�A�b�v �t�H�[�����쐬�����O�ɔ������܂��B
    ' Shutdown: �A�v���P�[�V���� �t�H�[�������ׂĕ���ꂽ��ɔ������܂��B���̃C�x���g�́A�ʏ�̏I���ȊO�̕��@�ŃA�v���P�[�V�������I�����ꂽ�Ƃ��ɂ͔������܂���B
    ' UnhandledException: �n���h������Ă��Ȃ���O���A�v���P�[�V�����Ŕ��������Ƃ��ɔ�������C�x���g�ł��B
    ' StartupNextInstance: �P��C���X�^���X �A�v���P�[�V�������N������A���ꂪ���ɃA�N�e�B�u�ł���Ƃ��ɔ������܂��B 
    ' NetworkAvailabilityChanged: �l�b�g���[�N�ڑ����ڑ����ꂽ�Ƃ��A�܂��͐ؒf���ꂽ�Ƃ��ɔ������܂��B
    Partial Friend Class MyApplication
        Private Shared ReadOnly logger As ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

        ''' <summary>
        ''' �A�v���P�[�V���� �t�H�[�������ׂĕ���ꂽ��ɔ�������C�x���g
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shutdown
            logger.Info("�I��")
        End Sub

        ''' <summary>
        ''' �A�v���P�[�V�������J�n���ꂽ�Ƃ��A�X�^�[�g�A�b�v �t�H�[�����쐬�����O�ɔ�������C�x���g
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>�X�v���b�V���X�N���[�����N�����Ă���</remarks>
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As StartupEventArgs) Handles Me.Startup

            Try
                Dim info As FileInfo = _
                        New FileInfo(Format(SystemInfo.AssemblyShortName(Assembly.GetExecutingAssembly()), _
                                              "{0}.exe.config"))
                XmlConfigurator.Configure(LogManager.GetRepository(), info)
                logger.Info("�N��")

            Catch ex As Exception
                MessageBox.Show(ex.Message, "�N����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End Sub
    End Class
End Namespace

