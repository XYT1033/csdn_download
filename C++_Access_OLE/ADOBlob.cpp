// ADOBlob.cpp : Defines the class behaviors for the application.
// Download by http://www.NewXing.com

#include "stdafx.h"
#include "ADOBlob.h"
#include "ADOBlobDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CADOBlobApp

BEGIN_MESSAGE_MAP(CADOBlobApp, CWinApp)
	//{{AFX_MSG_MAP(CADOBlobApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CADOBlobApp construction

CADOBlobApp::CADOBlobApp()
{
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CADOBlobApp object

CADOBlobApp theApp;
/////////////////////////////////////////////////////////////////////////////
// CADOBlobApp initialization

BOOL CADOBlobApp::InitInstance()
{
	AfxEnableControlContainer();
	AfxOleInit();
	m_pConnection.CreateInstance("ADODB.Connection");
	/******************连接数据库********************/
	try
	{
		m_pConnection->ConnectionTimeout = 8;
		//连接SQL SERVER
		//m_pConnection->Open("Driver=SQL Server;Database=test;Server=127.0.0.1;UID=sa;PWD=;","","",adModeUnknown);
		//连接ACCESS2000
		m_pConnection->Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=userinfo.mdb","","",adModeUnknown);
	}
	catch(_com_error e)///捕捉异常
	{
		AfxMessageBox("数据库连接失败!");
		return FALSE;
	} 
	/**********************************************/

#ifdef _AFXDLL
	Enable3dControls();			// Call this when using MFC in a shared DLL
#else
	Enable3dControlsStatic();	// Call this when linking to MFC statically
#endif

	CADOBlobDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
	}
	else if (nResponse == IDCANCEL)
	{
	}

	return FALSE;
}

int CADOBlobApp::ExitInstance() 
{
	if(m_pConnection->State)
		m_pConnection->Close();
	m_pConnection.Release();
	return CWinApp::ExitInstance();
}
