::Notes
::1.Must put in folder to clone/update sources
::2.Must have permission to view folder C:\Windows\System32\inetsrv\config
::3.Start w3svc Service: enable the IIS Hostable Web Core.
::4.Error the w3svc service is not available: Run file batch as Administrator
::5.Must config SSH for pulling code: http://learnaholic.me/2012/10/12/make-powershell-and-git-suck-less-on-windows/
::6.Add correct host key in /c/users/louis.nguyen/.ssh/know_hosts (before that, remove old host key)
::7.Auto close batch, put exit in bottom of main code	

:: Configurations 
@cd /d "%~dp0"
@set currentFolder=%cd%
@set microSiteSources=%currentFolder%

@set isCloneCode=0
@set isBuildCode=0
@set isArrangeIIS=1


@REM Can use List=(value1 value2) but hard to get value by index: List[index]
:: Source codes from gitlab
@set sourceCodes[0]=git@gitlab.nexdev.net:rome/alpha/CN88-ShareConfig.git
@set sourceCodes[1]=git@gitlab.nexdev.net:rome/alpha/CN88-Assets.git
@set sourceCodes[2]=git@gitlab.nexdev.net:rome/alpha/Alpha.CN88.git
@set sourceCodes[3]=git@gitlab.nexdev.net:rome/alpha/CN88-Features.git
@set sourceCodes[4]=git@gitlab.nexdev.net:rome/alpha/CN88-ViewLogs.git
@set sourceCodes[5]=git@gitlab.nexdev.net:rome/alpha/CN88-TotalBets.git
@set sourceCodes[6]=git@gitlab.nexdev.net:rome/alpha/CN88-Messages.git
@set sourceCodes[7]=git@gitlab.nexdev.net:rome/alpha/CN88-Results.git
@set sourceCodes[8]=git@gitlab.nexdev.net:rome/alpha/CN88-BetLists.git
@set sourceCodes[9]=git@gitlab.nexdev.net:rome/alpha/CN88-Reports.git
@set sourceCodes[10]=git@gitlab.nexdev.net:rome/alpha/CN88-BeforeLogin.git


:: Folder contains solution file/build file
:: If same structure, can loop through sub folder in microSiteSources to build
@set solutionFolders[0]=CN88-ShareConfig
@set solutionFolders[1]=CN88-Assets
@set solutionFolders[2]=Alpha.CN88
@set solutionFolders[3]=CN88-Features
@set solutionFolders[4]=CN88-ViewLogs
@set solutionFolders[5]=CN88-TotalBets
@set solutionFolders[6]=CN88-Messages
@set solutionFolders[7]=CN88-Results
@set solutionFolders[8]=CN88-BetLists
@set solutionFolders[9]=CN88-Reports
@set solutionFolders[10]=CN88-BeforeLogin


@set siteNameBefore=CN88-Before
@set portBefore=9119


@set siteName=CN88-Microsites3
@set port=9118

:: Virtual Directories
@set virtualDirectories[0]=assets
@set virtualDirectoryFolders[0]=CN88-Assets\assets

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

@set webApplicationFolders[0]=Alpha.CN88\Alpha.CN88\Alpha.CN88.Site
@set webApplicationFolders[1]=CN88-Features\src\CN88.Features.Site
@set webApplicationFolders[2]=CN88-ViewLogs\Alpha.ViewLogs.Site
@set webApplicationFolders[3]=CN88-TotalBets\src\CN88.TotalBets.Site
@set webApplicationFolders[4]=CN88-Messages\Alpha.Messages\Alpha.Messages.Site
@set webApplicationFolders[5]=CN88-Results\Alpha.Results
@set webApplicationFolders[6]=CN88-BetLists
@set webApplicationFolders[7]=CN88-Reports

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

@set PATH=%PATH%;C:\Program Files (x86)\Git\bin


@echo off
@setlocal EnableDelayedExpansion


@if %isCloneCode%==1 (
	@if not exist %microSiteSources% mkdir %microSiteSources%
	@call :CloneSources
)

@if %isBuildCode%==1 (
	@call :BuildCode
)

@if %isArrangeIIS%==1 (
	@call :ArrangeIIS
)

@pause
@exit /b

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:CloneSources
@setlocal enableDelayedExpansion

@set sourceCodeFolder=%microSiteSources%
@cd !sourceCodeFolder!

@set maxIndex=-1
@for /F "tokens=2 delims==" %%s in ('set sourceCodes[') do (
	@set /a maxIndex=maxIndex+1
)

@for /L %%i in (0,1,!maxIndex!) do (
	@set sourceCode=!sourceCodes[%%i]!
	@git clone !sourceCode!
)	

@exit /b



:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:BuildCode
@setlocal enableDelayedExpansion

@set sourceCodeFolder=%microSiteSources%

@set builCodeTasks[0]=build.cmd

@cd !sourceCodeFolder!

@set maxIndex=-1
@for /F "tokens=2 delims==" %%s in ('set solutionFolders[') do (
	@set /a maxIndex=maxIndex+1
)

@for /L %%i in (0,1,!maxIndex!) do (
	@set solutionFolder=!solutionFolders[%%i]!
	
	@if exist !sourceCodeFolder!\!solutionFolder! (
		@cd !sourceCodeFolder!\!solutionFolder!
	)
	
	@for /L %%i in (0,1,0) do (
		@set buildTaskPath=!sourceCodeFolder!\!solutionFolder!\!builCodeTasks[%%i]!
		
		@if exist !buildTaskPath! (	
			@call start !buildTaskPath!		
		)	
	)
)	

@exit /b

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:ArrangeIIS
@setlocal enableDelayedExpansion

@set appCMD=%systemroot%/system32/inetsrv/APPCMD
@set netFrameworkVersion="v4.0"

@cd %currentFolder%



:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@REM CN88-Before
@set poolNameBefore=!siteNameBefore!
@call %appCMD% list apppool /name:"!poolNameBefore!"
@if "%ERRORLEVEL%"=="1" (
	@echo %appCMD%\!poolNameBefore!
	@call %appCMD% add apppool /name:"!poolNameBefore!"
	@call %appCMD% set apppool /apppool.name:"!poolNameBefore!" /managedRuntimeVersion:"%netFrameworkVersion%"
)


@set micrositeHostBefore=%microSiteSources%\IISHost
@if not exist !micrositeHostBefore! mkdir !micrositeHostBefore!
@call %appCMD% list site /name:"!siteNameBefore!"
@if "%ERRORLEVEL%"=="1" (
	@call %appCMD% add site /name:"!siteNameBefore!" /physicalPath:"!micrositeHostBefore!" /bindings:http/*:%portBefore%:
	@call %appCMD% migrate config "!siteNameBefore!/"
	@call %appCMD% set site /site.name:"!siteNameBefore!" /[path='/'].applicationPool:"!poolNameBefore!"
	@call %appCMD% set site /site.name:"!siteNameBefore!" /[path='/'].preloadEnabled:"True"
	@call %appCMD% start site "!siteNameBefore!"
)

@set webApplicationBefore=cn88-before
@set webApplicationFolderBefore=!microSiteSources!\CN88-BeforeLogin\Fanex.Alpha.Before.Web
@call %appCMD% list app /site.name:"%siteNameBefore%" /path:"/!webApplicationBefore!"
@if "!ERRORLEVEL!"=="1" (
	@call %appCMD% add app /apppool.name:"%poolNameBefore%" /site.name:"%siteNameBefore%" /path:"/!webApplicationBefore!" /physicalPath:"!webApplicationFolderBefore!" /preloadEnabled:"True"
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@REM Create Application Pool
@set poolName=!siteName!
@call %appCMD% list apppool /name:"!poolName!"
@if "%ERRORLEVEL%"=="1" (
	@echo %appCMD%\!poolName!
	@call %appCMD% add apppool /name:"!poolName!"
	@call %appCMD% set apppool /apppool.name:"!poolName!" /managedRuntimeVersion:"%netFrameworkVersion%"
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@REM Create Site
@set micrositeHost=%microSiteSources%\CN88-ShareConfig\Alpha.ConfigurationSharing
@if not exist !micrositeHost! mkdir !micrositeHost!
@call %appCMD% list site /name:"!siteName!"
@if "%ERRORLEVEL%"=="1" (
	@call %appCMD% add site /name:"!siteName!" /physicalPath:"!micrositeHost!" /bindings:http/*:%port%:
	@call %appCMD% migrate config "!siteName!/"
	@call %appCMD% set site /site.name:"!siteName!" /[path='/'].applicationPool:"!poolName!"
	@call %appCMD% set site /site.name:"!siteName!" /[path='/'].preloadEnabled:"True"
	@call %appCMD% start site "!siteName!"
)


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@REM Create Virtual Directories

@REM Get Virtual Directory Length
@REM for getting List=(value1 value2) length, use for /L %%l in %List%
@set maxIndex=-1
@for /F "tokens=2 delims==" %%s in ('set virtualDirectories[') do (
	@set /a maxIndex=maxIndex+1
)

@for /L %%i in (0,1,!maxIndex!) do (
	@set virtualDirectory=!virtualDirectories[%%i]!
	@set virtualDirectoryFolder=!microSiteSources!\!virtualDirectoryFolders[%%i]!
	
	@call %appCMD% list vdir /app.name:"%siteName%/" /path:"/!virtualDirectory!"
	@if "!ERRORLEVEL!"=="1" (
		@REM the / following %siteName% specifies: the virtual directory should be added to the root application of the site.
		@call %appCMD% add vdir /app.name:"%siteName%/" /path:"/!virtualDirectory!" /physicalPath:"!virtualDirectoryFolder!"
	)
)	


:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@REM Create Web Applications 

@REM Get Web Applications Length
@set maxIndex=-1
@for /F "tokens=2 delims==" %%s in ('set webApplications[') do (
	@set /a maxIndex=maxIndex+1
)

@for /L %%i in (0,1,!maxIndex!) do (
	@set webApplication=!webApplications[%%i]!
	@set webApplicationFolder=!microSiteSources!\!webApplicationFolders[%%i]!
	@call %appCMD% list app /site.name:"%siteName%" /path:"/!webApplication!"
	@if "!ERRORLEVEL!"=="1" (
		@call %appCMD% add app /apppool.name:"%poolName%" /site.name:"%siteName%" /path:"/!webApplication!" /physicalPath:"!webApplicationFolder!" /preloadEnabled:"True"
	)
)

@exit /b








