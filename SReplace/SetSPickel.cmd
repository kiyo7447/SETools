@prompt $S
@cls

@SET ConfigFile=%KIYO_BIN%\DirectShell\SPickel.config

@echo Y	O:\20090311_swg_FileTranｆer> %ConfigFile%
@echo Y	O:\20090311_swg_FileTranｆer
@echo I	D:\kiyo\Windows\Deveropment\46_ファイル内の文字列置換(SReplace)\SReplace\SReplace\bin\Debug>> %ConfigFile%
@echo I	D:\kiyo\Windows\Deveropment\46_ファイル内の文字列置換(SReplace)\SReplace\SReplace\bin\Debug
@echo U	\\10.85.250.3\d$\Share\UNITE.NET\RO\Update\UpdateFiles\V04.XX.XX_Server_システムバックアップ＆リストアツール\BackupTool>> %ConfigFile%
@echo U	\\10.85.250.3\d$\Share\UNITE.NET\RO\Update\UpdateFiles\V04.XX.XX_Server_システムバックアップ＆リストアツール\BackupTool

@echo 環境を切り替えました。
@pause
