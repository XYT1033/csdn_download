#ifndef  __INCLUDE_SQL_LITE_HELPER_H__
#define  __INCLUDE_SQL_LITE_HELPER_H__


#include "sqlite3.h"

#pragma comment(lib, "sqlite3.lib")


class SQLiteHelper
{
public:
	SQLiteHelper() { }
	virtual ~SQLiteHelper() { CloseDB(); }

	// �����ݿ�
	int OpenDB(const char *path);
	// �ر����ݿ�
	int CloseDB();
	// ����һ�ű�
	int CreateTable(const char *create_table_state);
	// ɾ��һ�ű�
	int DropTable(const char *table_name);
	// ��ѯ����
	int Select(const char *select_state);
	// �������
	int Insert(const char *insert_state);
	// ɾ������
	int Delete(const char *delete_state);
	// ���²���
	int Update(const char *update_state);
	
private:
	sqlite3 *sqlite_db_;// ���ݿ��ָ��
	char* err_msg_;		// ������Ϣ
	bool is_close_;		// �ر����ݵı�ʶ

	// ��Ҫ����selece�����е���ʾ����Ԫ��
	static int CallBackFunc(void *NotUsed, int argc, char **argv, char **azColName);
	// ִ��sql���
	int SqlStateExec(const char *sql_state);
};


#endif