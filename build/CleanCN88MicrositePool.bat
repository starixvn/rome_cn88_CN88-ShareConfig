::Notes
::1.Must have permission to view folder C:\Windows\System32\inetsrv\config
::2.Error the w3svc service is not available: Run file batch as Administrator


:: Configurations
@set siteNameBefore=CN88-Before

@set siteName=CN88-Microsites3
@set poolName=%siteName%

:: Web Applications
@set appPrefix=site
@set webApplications[0]=%appPrefix%-cn88
@set webApplications[1]=%appPrefix%-features
@set webApplications[2]=%appPrefix%-viewlogs
@set webApplications[3]=%appPrefix%-totalbets
@set webApplications[4]=%appPrefix%-messages
@set webApplications[5]=%appPrefix%-results
@set webApplications[6]=%appPrefix%-betlists
@set webApplications[7]=%appPrefix%-reports


@set appCMD=%systemroot%/system32/inetsrv/APPCMD
@set isRemoveMicrositeSources=0

@echo off
@setlocal EnableDelayedExpansion

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@REM CN88-Before
@call %appCMD% list app /site.name:"%siteNameBefore%" /path:"/cn88-before"
@if "!ERRORLEVEL!"=="0" (
	@call %appCMD% delete app /app.name:"%siteNameBefore%/cn88-before"
)


@call %appCMD% list site /name:"!siteNameBefore!"
@if "%ERRORLEVEL%"=="0" (
	@call %appCMD% delete site /site.name:"%siteNameBefore%"
)

@call %appCMD% list apppool /name:"%siteNameBefore%"
@if "%ERRORLEVEL%"=="0" (
	@call %appCMD% delete apppool /apppool.name:"%siteNameBefore%"	
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Remove Web Applications 

@REM Get Web Applications Length
@set maxIndex=-1
@for /F "tokens=2 delims==" %%s in ('set webApplications[') do (
	@set /a maxIndex=maxIndex+1
)

@for /L %%i in (0,1,!maxIndex!) do (
	@set "webApplication=!webApplications[%%i]!"
	@call %appCMD% list app /site.name:"%siteName%" /path:"/!webApplication!"
	@if "!ERRORLEVEL!"=="0" (
		@call %appCMD% delete app /app.name:"%siteName%/!webApplication!"
	)
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Remove Virtual Directories
@set virtualDirectories[0]=contents
@set virtualDirectories[1]=assets

@set maxIndex=-1
@for /F "tokens=2 delims==" %%s in ('set virtualDirectories[') do (
	@set /a maxIndex=maxIndex+1
)

@for /L %%i in (0,1,!maxIndex!) do (
	@set virtualDirectory=!virtualDirectories[%%i]!
	@call %appCMD% list vdir /app.name:"%siteName%/" /path:"/!virtualDirectory!"
	@if "!ERRORLEVEL!"=="0" (
		@call %appCMD% delete vdir /vdir.name:"%siteName%/!virtualDirectory!"
	)
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Remove Site
@call %appCMD% list site /name:"!siteName!"
@if "%ERRORLEVEL%"=="0" (
	@call %appCMD% delete site /site.name:"%siteName%"
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Remove Application Pool
@call %appCMD% list apppool /name:"%poolName%"
@if "%ERRORLEVEL%"=="0" (
	@call %appCMD% delete apppool /apppool.name:"%poolName%"	
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Remove MicrositeHost
@set micrositeHost=D:\MicrositeHost\%siteName%
@if exist !micrositeHost! rd /s /q !micrositeHost!

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Remove MicrositeSources
@if %isRemoveMicrositeSources%==1 (
	@set micrositeSources=D:\MicrositeSources
	@if exist !micrositeSources! rd /s /q !micrositeSources!
)

@pause
@exit /b












































































