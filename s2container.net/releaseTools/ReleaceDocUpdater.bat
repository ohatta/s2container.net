rem ------------------------------------------------------------------
rem cd XXX�̕����ɂ�Seasar.NET��www�t�H���_������p�X���w�肵�ĉ������B
rem ------------------------------------------------------------------
set path=%~dp0.

rem ------------------------------------------------------------------------------
rem ReleaceDocUpdater.exe Seasar�̃z�[���y�[�W��̃����[�X����
rem �@�@�@�@�@�@�@�@�@�@�@�o�[�W�����ԍ����X�V
rem USAGE:
rem    1:���݂̃o�[�W�����ԍ�
rem    2:�V�����o�[�W�����ԍ�
rem    3�`:�X�V�ΏۂƂȂ�t�@�C���p�X�i�����w�肷��ꍇ�͋󔒋�؂�Ńp�X���L�q�j
rem -----------------------------------------------------------------------------

rem s2conariner.net ja
cd C:\MyPrograms\Seasar\www\ja
ReleaceDocUpdater.exe 1.3.13 1.3.14 index.html seasarnet.html download.html

rem s2conariner.net en
cd C:\MyPrograms\Seasar\www\en
ReleaceDocUpdater.exe 1.3.13 1.3.14 index.html releases.html

rem s2dao.net
cd C:\MyPrograms\Seasar\s2dao_www
ReleaceDocUpdater.exe 1.3.13 1.3.14 ja\index.html en\index.html