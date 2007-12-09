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
Imports Seasar.Dao.Attrs
Imports Seasar.Quill.Attrs

Namespace Dao
    ''' <summary>
    ''' �Ј��pDAO
    ''' </summary>
    ''' <remarks></remarks>
    <Implementation()> _
    <Aspect("DaoInterceptor")> _
    <Bean(GetType(EmployeeDto))> _
    Public Interface IEmployeeDao
        ''' <summary>
        ''' �Ј��ꗗ���擾����
        ''' </summary>
        ''' <returns>�Ј��ꗗ</returns>
        ''' <remarks></remarks>
        <Query("order by t_emp.n_id")> _
        Function GetAll() As IList(Of EmployeeDto)

        ''' <summary>
        ''' �Ј��f�[�^���擾����
        ''' </summary>
        ''' <param name="id">�Ј�ID</param>
        ''' <returns>�Ј��f�[�^</returns>
        ''' <remarks></remarks>
        <Query("t_emp.n_id = /*id*/1")> _
        Function GetData(ByVal id As Integer) As EmployeeDto

        ''' <summary>
        ''' �Ј�ID���擾����
        ''' </summary>
        ''' <param name="code">�Ј��R�[�h</param>
        ''' <returns>�Ј�ID</returns>
        ''' <remarks></remarks>
        <Sql("select n_id from t_emp where s_code = /*code*/'000001'")> _
        Function GetId(ByVal code As String) As Integer

        ''' <summary>
        ''' ���ʂŌ�������
        ''' </summary>
        ''' <param name="gender">����ID</param>
        ''' <returns>�Ј��ꗗ</returns>
        ''' <remarks></remarks>
        <Query("n_gender = /*gender*/1")> _
        Function FindByGender(ByVal gender As Integer) As IList(Of EmployeeDto)

        ''' <summary>
        ''' �Ј��f�[�^��}������
        ''' </summary>
        ''' <param name="data">�}������f�[�^</param>
        ''' <returns>�}������</returns>
        ''' <remarks></remarks>
        <NoPersistentProps("Id")> _
        Function InsertData(ByVal data As EmployeeDto) As Integer

        ''' <summary>
        ''' �Ј��f�[�^���X�V����
        ''' </summary>
        ''' <param name="data">�X�V����f�[�^</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function UpdateData(ByVal data As EmployeeDto) As Integer

        ''' <summary>
        ''' �Ј��f�[�^���폜����
        ''' </summary>
        ''' <param name="data">�Ј��f�[�^</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteData(ByVal data As EmployeeDto) As Integer

    End Interface
End Namespace