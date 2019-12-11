// test_use_sqlite.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include "iostream"
#include "sstream"
using namespace std; 

#include "sql_lite_helper.h"


SQLiteHelper sql_lite_helper;


// Test������һ�����ݿ��
int TestCreateTable()
{
	return sql_lite_helper.CreateTable("test_table (id int, name varchar, age int)");
}


// Test�����Բ�������
int TestInsert()
{
	for (int i= 1; i < 10; ++i)
	{
		std::stringstream str_sql;
		str_sql << "insert into test_table values(";
		str_sql << i << ","<< (i + 10) << "," << 23 << ");";
		std::string str = str_sql.str();
		sql_lite_helper.Insert(str.c_str());
	}

	return 0;
}


// Test������ɾ��ĳ��Ԫ��
int TestDelete()
{
	string str_sql= "delete from test_table where id=4;";

	return sql_lite_helper.Delete(str_sql.c_str());
}


// Test�����Ը���ĳ��Ԫ��
int TestUpdate()
{
	string str_sql= "update test_table set name='SQLite3' where name='17';";

	return sql_lite_helper.Update(str_sql.c_str());
}


// Test�����Բ�ѯ
int TestSelect()
{
	string str_sql= "select * from test_table;";

	return sql_lite_helper.Select(str_sql.c_str());
}


// Test������ɾ����
int TestDropTable()
{
	return sql_lite_helper.DropTable("test_table");
}


int main()
{
	int res = sql_lite_helper.OpenDB("./Test.db3");

	res = TestCreateTable();
	if (res != 0)
	{
		return 0;
	}

	res = TestInsert();
	if (res != 0)
	{
		return 0;
	}

	TestSelect();

	res = TestDelete();
	if (res != 0)
	{
		return 0;
	}

	res = TestUpdate();
	if (res != 0)
	{
		return 0;
	}

	TestSelect();

	TestDropTable();

	TestSelect();


	return 0;
}