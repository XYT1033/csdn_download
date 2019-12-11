// test_use_sqlite.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "iostream"
#include "sstream"
using namespace std; 

#include "sql_lite_helper.h"


SQLiteHelper sql_lite_helper;


// Test：创建一个数据库表
int TestCreateTable()
{
	return sql_lite_helper.CreateTable("test_table (id int, name varchar, age int)");
}


// Test：测试插入数据
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


// Test：测试删除某个元素
int TestDelete()
{
	string str_sql= "delete from test_table where id=4;";

	return sql_lite_helper.Delete(str_sql.c_str());
}


// Test：测试更新某个元素
int TestUpdate()
{
	string str_sql= "update test_table set name='SQLite3' where name='17';";

	return sql_lite_helper.Update(str_sql.c_str());
}


// Test：测试查询
int TestSelect()
{
	string str_sql= "select * from test_table;";

	return sql_lite_helper.Select(str_sql.c_str());
}


// Test：测试删除表
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