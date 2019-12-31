// ADOBlob.h : main header file for the ADOBLOB application
// Download by http://www.NewXing.com

#if !defined(AFX_ADOBLOB_H__7C4949C5_91D7_4590_AD59_ACD0BDC1066F__INCLUDED_)
#define AFX_ADOBLOB_H__7C4949C5_91D7_4590_AD59_ACD0BDC1066F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CADOBlobApp:
// See ADOBlob.cpp for the implementation of this class
//

class CADOBlobApp : public CWinApp
{
public:
	_ConnectionPtr m_pConnection;
	CADOBlobApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CADOBlobApp)
	public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CADOBlobApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ADOBLOB_H__7C4949C5_91D7_4590_AD59_ACD0BDC1066F__INCLUDED_)
