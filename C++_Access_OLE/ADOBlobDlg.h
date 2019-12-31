//Download by http://www.NewXing.com
// ADOBlobDlg.h : header file
//

#if !defined(AFX_ADOBLOBDLG_H__F6761EC1_02B5_4F5F_B939_578FB5DDB9D2__INCLUDED_)
#define AFX_ADOBLOBDLG_H__F6761EC1_02B5_4F5F_B939_578FB5DDB9D2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CADOBlobDlg dialog

class CADOBlobDlg : public CDialog
{
// Construction
public:
	_RecordsetPtr	m_pRecordset;
	HBITMAP			m_hPhotoBitmap;
	BOOL			m_bModify;
	BOOL			m_bNewUser;

	void			ResetControls();
	BOOL			FirstRecord();
	BOOL			LastRecord();
	void			ReadData();
	DWORD			m_nFileLen;
	HBITMAP			BufferToHBITMAP();
	BOOL			LoadBMPFile(const char* pBMPPathname);
	char			*m_pBMPBuffer;
	void			DestroyPhoto();
	void			DrawUserPhoto(int x,int y,CDC* pDC);
	CADOBlobDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CADOBlobDlg)
	enum { IDD = IDD_ADOBLOB_DIALOG };
	CButton	m_buttonDeleteUser;
	CButton	m_buttonPreviousUser;
	CButton	m_buttonNextUser;
	CEdit	m_editOld;
	CEdit	m_editUserName;
	CButton	m_buttonSelectPhoto;
	CButton	m_buttonSaveInfo;
	CString	m_UserName;
	CString	m_Old;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CADOBlobDlg)
	public:
	virtual BOOL DestroyWindow();
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CADOBlobDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnAddUser();
	afx_msg void OnChangeUsername();
	afx_msg void OnChangeOld();
	afx_msg void OnSaveinfo();
	afx_msg void OnSelectphoto();
	afx_msg void OnNextUser();
	afx_msg void OnPreviousUser();
	afx_msg void OnDeleteUser();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ADOBLOBDLG_H__F6761EC1_02B5_4F5F_B939_578FB5DDB9D2__INCLUDED_)
