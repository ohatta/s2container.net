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
    ''' ����pDAO
    ''' </summary>
    ''' <remarks></remarks>
    <Implementation()> _
        <Aspect("DaoInterceptor")> _
        <Bean(GetType(DepartmentDto))> _
    Public Interface IDepartmentDao
        ''' <summary>
        ''' ����ꗗ���擾����
        ''' </summary>
        ''' <returns>���僊�X�g</returns>
        ''' <remarks></remarks>
        <Query("order by n_show_order")> _
        Function GetAll() As IList(Of DepartmentDto)

        ''' <summary>
        ''' ����f�[�^���擾����
        ''' </summary>
        ''' <param name="id">����ID</param>
        ''' <returns>����f�[�^</returns>
        ''' <remarks></remarks>
        <Query("n_id = /*id*/1")> _
        Function GetData(ByVal id As Integer) As DepartmentDto

        ''' <summary>
        ''' ����ID���擾����
        ''' </summary>
        ''' <param name="code">����R�[�h</param>
        ''' <returns>����ID</returns>
        ''' <remarks></remarks>
        <Sql("select n_id from t_dept where s_code = /*code*/'0002'")> _
        Function GetId(ByVal code As String) As Integer

        ''' <summary>
        ''' �����}������
        ''' </summary>
        ''' <param name="dto">�}������f�[�^</param>
        ''' <returns>�}������</returns>
        ''' <remarks></remarks>
        <NoPersistentProps("Id")> _
        Function InsertData(ByVal dto As DepartmentDto) As Integer

        ''' <summary>
        ''' ������X�V����
        ''' </summary>
        ''' <param name="dto">�X�V�f�[�^</param>
        ''' <returns>�X�V����</returns>
        ''' <remarks></remarks>
        Function UpdateData(ByVal dto As DepartmentDto) As Integer

        ''' <summary>
        ''' ������폜����
        ''' </summary>
        ''' <param name="dto">�폜�f�[�^</param>
        ''' <returns>�폜����</returns>
        ''' <remarks></remarks>
        Function DeleteData(ByVal dto As DepartmentDto) As Integer

    End Interface
End Namespace