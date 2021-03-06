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
Imports Seasar.S2FormExample.Logics.Dto
Imports Seasar.Dao.Attrs
Imports Seasar.Quill.Attrs

Namespace Dao
    ''' <summary>
    ''' 性別用DAO
    ''' </summary>
    ''' <remarks></remarks>
    <Implementation()> _
        <S2Dao()> _
        <Bean(GetType(GenderDto))> _
    Public Interface IGenderDao
        ''' <summary>
        ''' 性別を一覧で取得する
        ''' </summary>
        ''' <returns>性別一覧</returns>
        ''' <remarks></remarks>
        Function GetAll() As IList(Of GenderDto)
    End Interface
End Namespace