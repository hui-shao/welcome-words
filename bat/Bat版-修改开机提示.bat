@echo off
title 修改开机提示语 By 浅蓝的灯
color 2f
mode con lines=33 cols=100
REM ________________________________________________________________

>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

if '%errorlevel%' NEQ '0' (

    echo 请求管理员权限...

    goto UACPrompt

) else ( goto gotAdmin )

:UACPrompt

    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"

    echo UAC.ShellExecute "%~s0", "", "", "runas", 1 >> "%temp%\getadmin.vbs"

    "%temp%\getadmin.vbs"
exit /B

:gotAdmin

    if exist "%temp%\getadmin.vbs" ( del "%temp%\getadmin.vbs" )
    pushd "%CD%"
    CD /D "%~dp0"
	goto A

REM ________________________________________________________________

:A
echo.
echo.
echo 使用说明:请按提示依次输入以下内容，输入完成后请 按回车键确认
ping 127.0.0.1 /n 3 >nul
echo.
echo ============By 浅蓝的灯==============
echo.
set /p caption=开机时显示的标题:
echo.
set /p contents=开机时显示的内容:
echo.
echo ----------------------------------------------
echo.
echo 正在修改注册表项……请等待
echo.
echo.
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "LegalNoticeCaption" /d %caption% /t REG_SZ /f
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "LegalNoticeText" /d %contents% /t REG_SZ /f
ping 127.0.0.1 /n 4 >nul 
echo.
echo 运行完毕，5秒后自动退出……
ping 127.0.0.1 /n 6 >nul 
exit