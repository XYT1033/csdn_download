#ifndef  __INCLUDE_SQL_LITE_HELPER_H__
#define  __INCLUDE_SQL_LITE_HELPER_H__


#include "sqlite3.h"

#pragma comment(lib, "sqlite3.lib")


class SQLiteHelper
{
public:
	SQLiteHelper() { }
	virtual ~SQLiteHelper() { CloseDB(); }

	// 打开数据库
	int OpenDB(const char *path);
	// 关闭数据库
	int CloseDB();
	// 创建一张表
	int CreateTable(const char *create_table_state);
	// 删除一张表
	int DropTable(const char *table_name);
	// 查询操作
	int Select(const char *select_state);
	// 插入操作
	int Insert(const char *insert_state);
	// 删除操作
	int Delete(const char *delete_state);
	// 更新操作
	int Update(const char *update_state);
	
private:
	sqlite3 *sqlite_db_;// 数据库的指针
	char* err_msg_;		// 错误信息
	bool is_close_;		// 关闭数据的标识

	// 主要用在selece操作中的显示数据元素
	static int CallBackFunc(void *NotUsed, int argc, char **argv, char **azColName);
	// 执行sql语句
	int SqlStateExec(const char *sql_state);
};


#endif