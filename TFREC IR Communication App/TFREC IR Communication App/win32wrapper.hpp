#ifndef WIN32WRAPPER_HPP
#define WIN32WRAPPER_HPP

#include <windows.h>
#include <shlobj.h>
#include <string>
#include "resource.h"
#include <fstream>
#include <ctime>
#include <cstdio>

using std::fstream;
using std::string;
using std::endl;

class win32wrapper
{
public:
	win32wrapper::win32wrapper(const char *g_szClassName, HINSTANCE hInstance, int nCmdShow, int width, int height);
	~win32wrapper();
	WNDCLASSEX getWindowClass();
	void mainLoop();
	MSG getMessage();

private:
	WNDCLASSEX wc;
	HWND hwnd;
	MSG msg;
};

typedef struct settings
{
	string fileDirectory;
}Settings;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
BOOL CALLBACK SettingsDlgProc(HWND hwnd, UINT Message, WPARAM wParam, LPARAM lParam);

bool fileExists(const string &fileName);
struct tm getTime();
string dateToString(struct tm currentTime);
void changeDirectory(string &fileDirectory);
void readSettings(fstream &infile, Settings &settings);
void setLogFile(fstream &flog, Settings &settings);


#endif